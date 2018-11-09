using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public class playerControls : MonoBehaviour
{

    Animator anim;

    private EnemyDamageValues DamageEffects;
    private Rigidbody2D PlayerRigidbody;
    public System.Action OnPunch;
    private StreamAttackAnimationFire StreamAnimFire;
    private StreamAttackAnimationWater StreamAnimWater;
    public int Speed;
    private int moveSpeed;
    public int jumpForce;
    public bool isJumping;
    public bool canShoot = true;
    public int shotSpeed;
    private bool Healing;
    private float ChargeTime = 1;

    public string pepperA = null;
    public string pepperB = null;
    public int pepperIndexA;
    public int pepperIndexB;

    public Text healthDisplay;
    public int health;
    private float BuffTimer;
    private int HealthTimer;
    private int DashDirection;

    public bool isImmune = false;
    public bool facingRight = true;

    public GameObject playerFireShot;
    public GameObject playerWaterShot;
    public GameObject playerIceShot;
    public GameObject playerShockShot1;
    public GameObject playerShockShot2;
    public GameObject playerShockShot3;
    public GameObject playerShockShot4;
    public GameObject playerEarthShot;
    public GameObject playerWindShot;
    public GameObject playerBuffShot;
    private GameObject ElectricShotToUse;

    public AudioSource playerSounds;

    public AudioClip playerFire;
    public AudioClip playerWater;
    public AudioClip playerIce;
    public AudioClip playerShock1;
    public AudioClip playerShock2;
    public AudioClip playerShock3;
    public AudioClip playerShock4;
    public AudioClip playerEarth;
    public AudioClip playerWind;
    public AudioClip playerBuff;

    public AudioClip playerHit;
    public AudioClip playerDeath;

    public GameObject eggFire;
    public GameObject eggWater;
    public GameObject eggIce;
    public GameObject eggShock;
    public GameObject eggEarth;
    public GameObject eggWind;
    public GameObject shitBrick;

    public Sprite[] IceSprites;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        healthDisplay = GameObject.Find("Health").GetComponent<Text>();
        healthDisplay.text = "Health: " + health;
        playerSounds = gameObject.GetComponent<AudioSource>();
        StreamAnimFire = this.gameObject.transform.GetChild(0).GetComponent<StreamAttackAnimationFire>();
        StreamAnimWater = this.gameObject.transform.GetChild(1).GetComponent<StreamAttackAnimationWater>();
        moveSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {


        //float move = Input.GetAxis("Horizontal");
        //anim.SetFloat("Speed", moveSpeed);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        healthDisplay.text = "Health: " + health;

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            PlayerRigidbody.AddForce(Vector2.up * jumpForce);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("Speed", 1);
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
            PlayerRigidbody.AddForce(Vector2.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("Speed", 1);
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
            PlayerRigidbody.AddForce(Vector2.left * moveSpeed);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)
        || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            // anim. = 0f;
            // anim.SetTrigger(idleHash);
            anim.SetInteger("Speed", 0);
        }
        if (pepperIndexA != 1) {
            playerFireShot.GetComponent<Collider2D>().enabled = false;
            //playerFireShot.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (pepperIndexA != 2) {
            playerWaterShot.GetComponent<Collider2D>().enabled = false;
            //playerWaterShot.GetComponent<SpriteRenderer>().enabled = false;
        }
        PepAttacks();
        EggBombs();
    }

    void PepAttacks()
    {
        if (canShoot)
        {
            GameObject shot;

            switch (pepperIndexA)
            {
                case 1: // Fire Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerFire);
                        playerFireShot.GetComponent<Collider2D>().enabled = true;
                        //playerFireShot.GetComponent<SpriteRenderer>().enabled = true;
                        StreamAnimFire.StartBeam();
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        canShoot = false;
                        StreamAnimFire.EndBeam();
                        StartCoroutine(shootWait());
                        playerSounds.Stop();
                    }
                    break;
                case 2: // Water Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerWater);
                        playerWaterShot.GetComponent<Collider2D>().enabled = true;
                        //playerWaterShot.GetComponent<SpriteRenderer>().enabled = true;
                        StreamAnimWater.StartBeam();
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        canShoot = false;
                        StreamAnimWater.EndBeam();
                        StartCoroutine(shootWait());
                        playerSounds.Stop();
                    }
                    break;
                case 3: // CALLS Ice Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerIce);
                        canShoot = false;
                        StartCoroutine(IceBurst());
                    }
                    break;
                case 4: // Electric Shock Pepper Power Attack
                    if (Input.GetKey(KeyCode.Space))
                    {
                        ChargeTime = ChargeTime + Time.deltaTime;
                        //Debug.Log(ChargeTime);
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        canShoot = false;
                        if (ChargeTime >= 3) {
                            SoundCall(playerShock4);
                            ElectricShotToUse = playerShockShot4;
                        }
                        else if (ChargeTime >= 2) {
                            SoundCall(playerShock3);
                            ElectricShotToUse = playerShockShot3;
                        }
                        else if (ChargeTime >= 1) {
                            SoundCall(playerShock2);
                            ElectricShotToUse = playerShockShot2;
                        }
                        else if (ChargeTime < 1) {
                            SoundCall(playerShock1);
                            ElectricShotToUse = playerShockShot1;
                        }
                        shot = Instantiate(ElectricShotToUse, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                        if (facingRight)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                        else if (!facingRight)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        StartCoroutine(shootWait());
                    }
                    break;
                case 5: // Earth Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SoundCall(playerEarth);
                        canShoot = false;
                        shot = Instantiate(playerEarthShot, transform.position + new Vector3(0, -1, 0), 
			            Quaternion.identity) as GameObject;
                        //shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        ConsumableOver();
                        StartCoroutine(shootWait());
                    }
                    break;
                case 6: // Wind Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SoundCall(playerWind);
                        canShoot = false;
                        shot = Instantiate(playerWindShot, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                        if (facingRight)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 600);
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
                        }
                        else if (!facingRight)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 600);
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
                        }
                        StartCoroutine(shootWait());
                    }
                    break;
                case 7: // Buff Arms Pepper Power Attack
                    playerBuffShot.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        //canShoot = false;
                        if (OnPunch != null) {
                            SoundCall(playerBuff);
                            OnPunch();
						}
                    }
                    BuffTimer = BuffTimer - 1 * Time.deltaTime;
                    if (BuffTimer <= 0) {
                        playerBuffShot.SetActive(false);
                        ConsumableOver();
                    }
                    break;
                case 8: // CALLS Speed Dash Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        canShoot = false;
                        if (DashDirection == 0) {
                            StartCoroutine(SpeedDash());
                        }
                    }
                    break;
                case 9: // CALLS Health Pepper Power heal
                    if (Input.GetKeyDown(KeyCode.Space) && Healing == false) {
                        Healing = true;
                        StartCoroutine(HealThePlayer());
                    }
                    if (HealthTimer <= 0) {
                        Healing = false;
                        ConsumableOver();
                    }
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {   
            if (pepperIndexA == 1 && StreamAnimFire.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                StreamAnimFire.EndBeam();
                Debug.Log("Called1");
            }
            else if (pepperIndexA == 2 && StreamAnimWater.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                StreamAnimWater.EndBeam();
                Debug.Log("Called2");
            }

            int tempIndex = pepperIndexA;
            string tempPepper = pepperA;

            pepperIndexA = pepperIndexB;
            pepperIndexB = tempIndex;

            pepperA = pepperB;
            pepperB = tempPepper;

        }
    }

    void EggBombs()
    {
        GameObject shot;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            switch (pepperIndexA)
            {
                case 1:
                    shot = Instantiate(eggFire, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 2:
                    shot = Instantiate(eggWater, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 3:
                    shot = Instantiate(eggIce, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 4:
                    shot = Instantiate(eggShock, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 6:
                    shot = Instantiate(eggWind, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 8:
                    shot = Instantiate(playerIceShot, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                        if (facingRight) {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        else if (!facingRight) {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                    break;
            }
            pepperA = pepperB;
            pepperIndexA = pepperIndexB;
            pepperB = null;
            pepperIndexB = 0;
        }
    }

    private IEnumerator SpeedDash()
    {
        if (facingRight) {
            DashDirection = 1;
        }
        else if (!facingRight) {
            DashDirection = 2;
        }
        for (float i = 0; i < 1; i += 0.1f) {
            if (i < 0.9f) {
                if (DashDirection == 1) {
                    PlayerRigidbody.velocity = Vector2.right * 60;
                }
                else if (DashDirection == 2) {
                    PlayerRigidbody.velocity = Vector2.left * 60;
                }
            }
            else if (i >= 0.9f) {
                yield return new WaitForSeconds(0.2f);
                PlayerRigidbody.velocity = Vector2.zero;
            }
        }
        StartCoroutine(shootWait());
    }

    private IEnumerator IceBurst()
    {
        for (int i = 0; i < 3; i++) {
            GameObject shot = Instantiate(playerIceShot, transform.position + new Vector3(0, 0, 0), 
			Quaternion.identity) as GameObject;
            shot.GetComponent<SpriteRenderer>().sprite = IceSprites[i];
            if (facingRight) {
                shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
            }
            else if (!facingRight) {
                shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
            }
            yield return new WaitForSeconds(0.3f);
        }
        StartCoroutine(shootWait());
    }

    void CollidingDeathRay () {
        if (!isImmune) {
            if (!playerSounds.isPlaying)
            {
                SoundCall(playerHit);
            }
            isImmune = true;
            health -= 10;
            StartCoroutine(iFrames());
        }
    }

    private IEnumerator shootWait()
    {
        //Debug.Log("Counting down...");
        if (pepperIndexA == 1) {
            //yield return new WaitForSeconds(0.5f);
        }
        else if (pepperIndexA == 2) {
            //yield return new WaitForSeconds(1f);
        }
        else if (pepperIndexA == 8) {
            yield return new WaitForSeconds(0.3f);
            DashDirection = 0;
        }
        else if (pepperIndexA == 6) {
            yield return new WaitForSeconds(2.0f);
        }
        else if (pepperIndexA == 4) {
            ChargeTime = 0;
            yield return new WaitForSeconds(0.4f);
        }
        else {
            yield return new WaitForSeconds(1.0f);
        }
        canShoot = true;
    }

    private IEnumerator iFrames()
    {
        yield return new WaitForSeconds(1.0f);
        isImmune = false;
    }

    private IEnumerator HealThePlayer()
    {
        for (int i = 0; i < 5; i++) {
            HealthTimer = HealthTimer - 1;
            health = health + 20;
            yield return new WaitForSeconds(1);
        }
    }

    private void ConsumableOver()
    {
        pepperA = null;
        GameObject shot = Instantiate(shitBrick, transform.position + new Vector3(0, 0, 0), 
		    Quaternion.identity) as GameObject;
        pepperIndexA = 0;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Enemy") {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (pepperIndexA == 0 || pepperIndexB == 0) {

            if (collider.gameObject.tag == "firePepper") {
                PepperCollision(1, "firePepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "waterPepper") {
                PepperCollision(2, "waterPepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "icePepper") {
                PepperCollision(3, "icePepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "shockPepper") {
                PepperCollision(4, "shockPepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "earthPepper") {
                PepperCollision(5, "earthPepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "windPepper") {
                PepperCollision(6, "windPepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "buffPepper") {
                BuffTimer = 20;
                PepperCollision(7, "buffPepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "speedPepper") {
                PepperCollision(8, "speedPepper");
                Destroy(collider.gameObject);
            }
            if (collider.gameObject.tag == "healthPepper") {
                HealthTimer = 5;
                PepperCollision(9, "healthPepper");
                Destroy(collider.gameObject);
            }
        }
        if (collider.gameObject.tag == "enemyFist") {
            if (!isImmune) {
                if (!playerSounds.isPlaying)
                {
                    playerSounds.clip = playerHit;
                    playerSounds.loop = false;
                    playerSounds.Play();
                }
                isImmune = true;
                health -= 3;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT1") {
            if (!isImmune) {
                if (!playerSounds.isPlaying)
                {
                    playerSounds.clip = playerHit;
                    playerSounds.loop = false;
                    playerSounds.Play();
                }
                isImmune = true;
                health -= 5;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT2") {
            if (!isImmune) {
                if (!playerSounds.isPlaying)
                {
                    playerSounds.clip = playerHit;
                    playerSounds.loop = false;
                    playerSounds.Play();
                }
                isImmune = true;
                health -= 5;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT3") {
            if (!isImmune) {
                if (!playerSounds.isPlaying)
                {
                    playerSounds.clip = playerHit;
                    playerSounds.loop = false;
                    playerSounds.Play();
                }
                isImmune = true;
                health -= 20;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyExplosion") {
            if (!isImmune) {
                if (!playerSounds.isPlaying)
                {
                    playerSounds.clip = playerHit;
                    playerSounds.loop = false;
                    playerSounds.Play();
                }
                isImmune = true;
                health -= 10;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyDeathRay") {
            //SaucerColliding = true;
            InvokeRepeating("CollidingDeathRay", 0, 0.4f);
        }
    }
    
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "enemyDeathRay") {
            //SaucerColliding = false;
            Debug.Log("Left Collider");
            CancelInvoke("CollidingDeathRay");
        }
    }

    void PepperCollision(int pepperNumber, string pepperName) {
        if (pepperIndexA == 0) {
            pepperIndexA = pepperNumber;
            pepperA = pepperName;
        }
        else if (pepperIndexB == 0) {
            pepperIndexB = pepperNumber;
            pepperB = pepperName;
        }
    }

    void SoundCall (AudioClip clip) {
        playerSounds.clip = clip;
        playerSounds.loop = false;
        playerSounds.Play();
    }

}