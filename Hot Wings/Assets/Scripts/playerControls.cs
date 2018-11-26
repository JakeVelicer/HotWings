using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public class playerControls : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D PlayerRigidbody;
    private Vector2 velocity;
    public System.Action OnPunch;
    private StreamAttackAnimationFire StreamAnimFire;
    private StreamAttackAnimationWater StreamAnimWater;

    public int Speed;
    private int moveSpeed;
    public int jumpForce;
    public bool isJumping;
    private bool Dashing;
    public bool canShoot = true;
    public int shotSpeed;
    public int DashSpeed;
    private bool Healing;
    private float ChargeTime = 1;

    //Pepper references
    public string pepperA = null;
    public string pepperB = null;
    public int pepperIndexA;
    public int pepperIndexB;

    public int health;
    private float BuffTimer;
    private int HealthTimer;
    private int DashDirection;

    public bool isImmune = false;
    public bool facingRight = true;

    public GameObject playerDashCollider;
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
    public AudioClip playerDash;
    public AudioClip playerHeal;

    public AudioClip pepperCollect;
    public AudioClip eggDrop;
    public AudioClip playerHit;
    public AudioClip playerDeath;

    public GameObject eggFire;
    public GameObject eggWater;
    public GameObject eggIce;
    public GameObject eggShock;
    public GameObject eggSpeed;
    public GameObject eggWind;
    public GameObject shitBrick;
    public Sprite[] IceSprites;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        playerSounds = gameObject.GetComponent<AudioSource>();
        StreamAnimFire = playerFireShot.GetComponent<StreamAttackAnimationFire>();
        StreamAnimWater = playerWaterShot.GetComponent<StreamAttackAnimationWater>();
        moveSpeed = Speed;
    }

    // Update is called once per frame
    void Update() {

        PepAttacks();
        EggBombs();

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (pepperIndexA != 1) {
            playerFireShot.GetComponent<Collider2D>().enabled = false;
            //playerFireShot.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (pepperIndexA != 3) {
            playerWaterShot.GetComponent<Collider2D>().enabled = false;
            //playerWaterShot.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (health == 0) {
            anim.SetInteger("Speed", 9);
        }
    }

    // Movement
    void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            PlayerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        float horizontalInput = Input.GetAxis("Horizontal"); //a,d, left, and right
        if (horizontalInput > 0)
        {
            anim.SetInteger("Speed", 1);
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        else if (horizontalInput < 0)
        {
            anim.SetInteger("Speed", 1);
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        if (!Dashing) {
            velocity = PlayerRigidbody.velocity;
            velocity.y += Physics2D.gravity.y * 0.05f;
            velocity.x = horizontalInput * Speed;
            PlayerRigidbody.velocity = velocity;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)
        || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("Speed", 0);
        }

    }

    void PepAttacks() {

        if (canShoot)
        {
            GameObject shot;
            switch (pepperIndexA)
            {
                case 1: // Fire Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerFire);
                        playerFireShot.GetComponent<Collider2D>().enabled = true;
                        playerFireShot.GetComponent<SpriteRenderer>().enabled = true;
                        StreamAnimFire.StartBeam();
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        canShoot = false;
                        StreamAnimFire.GoToIdle();
                        StartCoroutine(shootWait());
                        playerSounds.Stop();
                    }
                    break;
                case 2: // Electric Shock Pepper Power Attack
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
                case 3: // Water Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerWater);
                        playerWaterShot.GetComponent<Collider2D>().enabled = true;
                        playerWaterShot.GetComponent<SpriteRenderer>().enabled = true;
                        StreamAnimWater.StartBeam();
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        canShoot = false;
                        StreamAnimWater.GoToIdle();
                        StartCoroutine(shootWait());
                        playerSounds.Stop();
                    }
                    break;
                case 4: // CALLS Ice Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerIce);
                        canShoot = false;
                        StartCoroutine(IceBurst());
                    }
                    break;
                case 5: // CALLS Speed Dash Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerDash);
                        canShoot = false;
                        if (DashDirection == 0) {
                            StartCoroutine(SpeedDash());
                        }
                    }
                    break;
                case 6: // Wind Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SoundCall(playerWind);
                        canShoot = false;
                        if (facingRight)
                        {
                            shot = Instantiate(playerWindShot, transform.position + new Vector3(1.5f, 0.32f, 0), 
			                Quaternion.identity) as GameObject;
                            shot.GetComponent<WindBehavior>().GoRight = true;
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 600);
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
                        }
                        else if (!facingRight)
                        {
                            shot = Instantiate(playerWindShot, transform.position + new Vector3(-1.5f, 0.32f, 0), 
			                Quaternion.identity) as GameObject;
                        	shot.GetComponent<WindBehavior>().GoRight = false;
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 600);
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
                        }
                        StartCoroutine(shootWait());
                    }
                    break;
                case 7: // CALLS Health Pepper Power heal
                    if (Input.GetKeyDown(KeyCode.Space) && Healing == false) {
                        SoundCall(playerHeal);
                        Healing = true;
                        StartCoroutine(HealThePlayer());
                    }
                    if (HealthTimer <= 0) {
                        Healing = false;
                        ConsumableOver();
                    }
                    break;
                case 8: // Buff Arms Pepper Power Attack
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
                case 9: // Earth Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SoundCall(playerEarth);
                        canShoot = false;
                        shot = Instantiate(playerEarthShot, transform.position + new Vector3(0, -1, 0), 
			            Quaternion.identity) as GameObject;
                        ConsumableOver();
                        StartCoroutine(shootWait());
                    }
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {   
            if (pepperIndexA == 1 && StreamAnimFire.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                StreamAnimFire.GoToIdle();
                StreamAnimWater.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (pepperIndexA == 3 && StreamAnimWater.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                StreamAnimWater.GoToIdle();
                StreamAnimFire.GetComponent<SpriteRenderer>().enabled = false;
            }

            int tempIndex = pepperIndexA;
            string tempPepper = pepperA;

            pepperIndexA = pepperIndexB;
            pepperIndexB = tempIndex;

            pepperA = pepperB;
            pepperB = tempPepper;

        }
    }

    void EggBombs() {

        GameObject shot;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            switch (pepperIndexA)
            {
                case 1:
                    SoundCall(eggDrop);
                    if (StreamAnimFire.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                        StreamAnimFire.GoToIdle();
                    }
                    shot = Instantiate(eggFire, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 2:
                    SoundCall(eggDrop);
                    shot = Instantiate(eggShock, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 3:
                    SoundCall(eggDrop);
                    if (StreamAnimWater.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                        StreamAnimWater.GoToIdle();
                    }
                    shot = Instantiate(eggWater, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 4:
                    SoundCall(eggDrop);
                    shot = Instantiate(eggIce, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 5:
                    SoundCall(eggDrop);
                    shot = Instantiate(eggSpeed, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                        if (facingRight) {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        else if (!facingRight) {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                    break;
                case 6:
                    SoundCall(eggDrop);
                    shot = Instantiate(eggWind, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
            }
            pepperA = pepperB;
            pepperIndexA = pepperIndexB;
            pepperB = null;
            pepperIndexB = 0;
        }
    }

    private IEnumerator SpeedDash() {

        Dashing = true;
        playerDashCollider.GetComponent<Collider2D>().enabled = true;
        if (facingRight) {
            DashDirection = 1;
        }
        else if (!facingRight) {
            DashDirection = 2;
        }
        for (float i = 0; i < 1; i += 0.1f) {
            if (i < 0.9f) {
                if (DashDirection == 1) {
                    PlayerRigidbody.velocity = Vector2.right * DashSpeed;
                }
                else if (DashDirection == 2) {
                    PlayerRigidbody.velocity = Vector2.left * DashSpeed;
                }
            }
            else if (i >= 0.9f) {
                yield return new WaitForSeconds(0.1f);
                PlayerRigidbody.velocity = Vector2.zero;
                playerDashCollider.GetComponent<Collider2D>().enabled = false;
                Dashing = false;
            }
        }
        StartCoroutine(shootWait());
    }

    private IEnumerator IceBurst() {

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

    private IEnumerator shootWait() {
        
        //Debug.Log("Counting down...");
        if (pepperIndexA == 1) {
            //yield return new WaitForSeconds(0.5f);
        }
        else if (pepperIndexA == 3) {
            //yield return new WaitForSeconds(1f);
        }
        else if (pepperIndexA == 5) {
            yield return new WaitForSeconds(0.4f);
            DashDirection = 0;
        }
        else if (pepperIndexA == 6) {
            yield return new WaitForSeconds(2.0f);
        }
        else if (pepperIndexA == 2) {
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

            if (collider.gameObject.name == "FirePepper(Clone)") {
                if (pepperIndexA != 1 && pepperIndexB != 1) {
                    PepperCollision(1, "firePepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "ShockPepper(Clone)") {
                if (pepperIndexA != 2 && pepperIndexB != 2) {
                    PepperCollision(2, "shockPepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "WaterPepper(Clone)") {
                if (pepperIndexA != 3 && pepperIndexB != 3) {
                    PepperCollision(3, "waterPepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "IcePepper(Clone)") {
                if (pepperIndexA != 4 && pepperIndexB != 4) {
                    PepperCollision(4, "icePepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "SpeedPepper(Clone)") {
                if (pepperIndexA != 5 && pepperIndexB != 5) {
                    PepperCollision(5, "speedPepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "WindPepper(Clone)") {
                if (pepperIndexA != 6 && pepperIndexB != 6) {
                    PepperCollision(6, "windPepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "HealthPepper(Clone)") {
                if (pepperIndexA != 7 && pepperIndexB != 7) {
                    HealthTimer = 5;
                    PepperCollision(7, "healthPepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "BuffPepper(Clone)") {
                if (pepperIndexA != 8 && pepperIndexB != 8) {
                    BuffTimer = 20;
                    PepperCollision(8, "buffPepper");
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "EarthPepper(Clone)") {
                if (pepperIndexA != 9 && pepperIndexB != 9) {
                    PepperCollision(9, "earthPepper");
                    Destroy(collider.gameObject);
                }
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
                health -= 10;
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
        if (collider.gameObject.tag == "enemyFist") {
            if (!isImmune) {
                if (!playerSounds.isPlaying)
                {
                    playerSounds.clip = playerHit;
                    playerSounds.loop = false;
                    playerSounds.Play();
                }
                isImmune = true;
                health -= 7;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyDeathRay") {
            //SaucerColliding = true;
            InvokeRepeating("CollidingDeathRay", 0, 0.4f);
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemyDeathRay") {
            //SaucerColliding = false;
            Debug.Log("Left Collider");
            CancelInvoke("CollidingDeathRay");
        }
    }

    void PepperCollision(int pepperNumber, string pepperName) {
        if (pepperIndexA == 0) {
            SoundCall(pepperCollect);
            pepperIndexA = pepperNumber;
            pepperA = pepperName;
        }
        else if (pepperIndexB == 0) {
            SoundCall(pepperCollect);
            pepperIndexB = pepperNumber;
            pepperB = pepperName;
        }
    }

    void SoundCall (AudioClip clip) {

        playerSounds.clip = clip;
        playerSounds.loop = false;
        playerSounds.loop |= (clip == playerFire || clip == playerWater);
        playerSounds.Play();
    }

}