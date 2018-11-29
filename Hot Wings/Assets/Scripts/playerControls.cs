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
    private bool isBuff;
    public bool canShoot = true;
    public int shotSpeed;
    public int DashSpeed;
    private bool Healing;
    private float horizontalInput;
    private float ChargeTime = 1;

    //Pepper references
    public int pepperIndexA;
    public int pepperIndexB;

    public int health;
    [HideInInspector] public int BuffTimer;
    [HideInInspector] public int HealthTimer;
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
    public AudioSource playerVocals;
    public AudioSource playerAmbient;

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
    public AudioClip playerBuff2;
    private bool buffPunch1 = true;

    public AudioClip playerDash;
    public AudioClip playerHeal;

    public AudioClip pepperCollect;
    public AudioClip eggDrop;
    public AudioClip playerHit;
    public AudioClip playerDeath;
    public AudioClip shockLoop;
    private bool shockLoopPlaying = false;

    public GameObject eggFire;
    public GameObject eggWater;
    public GameObject eggIce;
    public GameObject eggShock;
    public GameObject eggSpeed;
    public GameObject eggWind;
    public GameObject shitBrick;
    public Sprite[] IceSprites;
    public int AnimChecker;
    // Use this for initialization
    void Start()
    { 

        anim = GetComponent<Animator>();
        anim.SetBool("isIdle", true);
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        StreamAnimFire = playerFireShot.GetComponent<StreamAttackAnimationFire>();
        StreamAnimWater = playerWaterShot.GetComponent<StreamAttackAnimationWater>();
        moveSpeed = Speed;
    }

    // Update is called once per frame, movement, animations, attacks called
    void Update() {

        PepAttacks();
        EggBombs();
        SlotBCleanup();

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        AnimChecker = anim.GetInteger("Speed");
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
            isJumping = true;
            PlayerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        horizontalInput = Input.GetAxis("Horizontal"); //a,d, left, and right
        if (horizontalInput > 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        else if (horizontalInput < 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (horizontalInput == 0)
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }

        if (pepperIndexA != 1) {
            playerFireShot.GetComponent<Collider2D>().enabled = false;
            //playerFireShot.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (pepperIndexA != 3) {
            playerWaterShot.GetComponent<Collider2D>().enabled = false;
            //playerWaterShot.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (PlayerRigidbody.velocity.y < -0.1)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }
        if (PlayerRigidbody.velocity.y > 0.1)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        if (health == 0) {
            anim.SetBool("isDead", true);
        } 

    }

    void FixedUpdate() {

        // Movement
        if (!Dashing) {
            velocity = PlayerRigidbody.velocity;
            velocity.y += Physics2D.gravity.y * 0.05f;
            velocity.x = horizontalInput * Speed;
            PlayerRigidbody.velocity = velocity;
        }

    }
    void AnimtionState(){




    }
    void PepAttacks() {

        if (canShoot)
        {
            GameObject shot;
            switch (pepperIndexA)
            {
                case 1: // Fire Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        anim.SetBool("isAttacking", true);
                        SoundCall(playerFire, playerSounds);
                        playerFireShot.GetComponent<Collider2D>().enabled = true;
                        playerFireShot.GetComponent<SpriteRenderer>().enabled = true;
                        StreamAnimFire.StartBeam();
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        anim.SetBool("isAttacking", false);
                        canShoot = false;
                        StreamAnimFire.GoToIdle();
                        StartCoroutine(shootWait());
                        playerSounds.Stop();
                    }
                    break;
                case 2: // Electric Shock Pepper Power Attack
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (!shockLoopPlaying)
                        {
                            SoundCall(shockLoop, playerAmbient);
                            shockLoopPlaying = true;
                        }
                        ChargeTime = ChargeTime + Time.deltaTime;
                        //Debug.Log(ChargeTime);
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        shockLoopPlaying = false;
                        playerAmbient.Stop();
                        canShoot = false;
                        if (ChargeTime >= 3) {
                            SoundCall(playerShock4, playerSounds);
                            ElectricShotToUse = playerShockShot4;

                        }
                        else if (ChargeTime >= 2) {
                            SoundCall(playerShock3, playerSounds);
                            ElectricShotToUse = playerShockShot3;
                        }
                        else if (ChargeTime >= 1) {
                            SoundCall(playerShock2, playerSounds);
                            ElectricShotToUse = playerShockShot2;
                           
                        }
                        else if (ChargeTime < 1) {
                            SoundCall(playerShock1, playerSounds);
                            ElectricShotToUse = playerShockShot1;
                           
                        }
                        anim.SetBool("isAttacking", true);
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
                        anim.SetBool("isAttacking", true);
                        SoundCall(playerWater, playerSounds);
                        playerWaterShot.GetComponent<Collider2D>().enabled = true;
                        playerWaterShot.GetComponent<SpriteRenderer>().enabled = true;
                        StreamAnimWater.StartBeam();
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        anim.SetBool("isAttacking", false);
                        canShoot = false;
                        StreamAnimWater.GoToIdle();
                        StartCoroutine(shootWait());
                        playerSounds.Stop();
                    }
                    break;
                case 4: // CALLS Ice Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        anim.SetBool("isAttacking", true);
                        SoundCall(playerIce, playerSounds);
                        canShoot = false;
                        StartCoroutine(IceBurst());
                    }
                    break;
                case 5: // CALLS Speed Dash Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        SoundCall(playerDash, playerSounds);
                        canShoot = false;
                        if (DashDirection == 0) {
                            StartCoroutine(SpeedDash());
                        }
                    }
                    break;
                case 6: // Wind Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        anim.SetBool("isWind", true);
                        SoundCall(playerWind, playerSounds);
                        canShoot = false;
                        if (facingRight)
                        {
                            anim.SetBool("isAttacking", true);
                            shot = Instantiate(playerWindShot, transform.position + new Vector3(1.5f, 0.32f, 0), 
			                Quaternion.identity) as GameObject;
                            shot.GetComponent<WindBehavior>().GoRight = true;
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 600);
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
                        }
                        else if (!facingRight)
                        {
                            anim.SetBool("isAttacking", true);
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
                    if (Input.GetKeyDown(KeyCode.Space) && !Healing) {
                        SoundCall(playerHeal, playerAmbient);
                        Healing = true;
                        StartCoroutine(HealThePlayer());
                    }
                    if (HealthTimer <= 0) {
                        Healing = false;
                        ConsumableOverA();
                    }
                    break;
                case 8: // Buff Arms Pepper Power Attack
                    playerBuffShot.SetActive(true);
                    anim.SetBool("isBuff", true);
                    if (!isBuff) {
                        isBuff = true;
                        StartCoroutine(BuffTime());
                    }
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        //canShoot = false;
                        if (OnPunch != null) {
                            anim.SetBool("isAttacking", true);
                            OnPunch();
                            if (buffPunch1)
                            {
                                SoundCall(playerBuff, playerSounds);
                            }
                            else
                            {
                                SoundCall(playerBuff2, playerSounds);
                            }
                            buffPunch1 = !buffPunch1;
						}
                    }
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        anim.SetBool("isAttacking", false);
                    }
                    if (BuffTimer <= 0) {
                        playerBuffShot.SetActive(false);
                        anim.SetBool("isBuff", false);
                        anim.SetBool("isAttacking", false);
                        isBuff = false;
                        ConsumableOverA();
                    }
                    break;
                case 9: // Earth Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SoundCall(playerEarth, playerSounds);
                        canShoot = false;
                        shot = Instantiate(playerEarthShot, transform.position + new Vector3(0, -1, 0), 
			            Quaternion.identity) as GameObject;
                        ConsumableOverA();
                        StartCoroutine(shootWait());
                    }
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {   
            if (pepperIndexA != 8) {
                if (pepperIndexA == 1 && StreamAnimFire.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                    StreamAnimFire.GoToIdle();
                    StreamAnimWater.GetComponent<SpriteRenderer>().enabled = false;
                }
                else if (pepperIndexA == 3 && StreamAnimWater.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                    StreamAnimWater.GoToIdle();
                    StreamAnimFire.GetComponent<SpriteRenderer>().enabled = false;
                }

                int tempIndex = pepperIndexA;
                pepperIndexA = pepperIndexB;
                pepperIndexB = tempIndex;
            }
        }
    }

    void SlotBCleanup () {

        switch (pepperIndexB) {
            case 7:
                if (HealthTimer <= 0) {
                    Healing = false;
                    ConsumableOverB();
                }
                break;
            case 8: // Buff Arms Pepper Power Attack
                if (BuffTimer <= 0) {
                    playerBuffShot.SetActive(false);
                    isBuff = false;
                    ConsumableOverB();
                }
                break;
        }
    }

    void EggBombs() {

        GameObject shot;

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            switch (pepperIndexA)
            {
                case 1:
                    SoundCall(eggDrop, playerSounds);
                    if (StreamAnimFire.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                        StreamAnimFire.GoToIdle();
                    }
                    shot = Instantiate(eggFire, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 2:
                    SoundCall(eggDrop, playerSounds);
                    shot = Instantiate(eggShock, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 3:
                    SoundCall(eggDrop, playerSounds);
                    if (StreamAnimWater.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
                        StreamAnimWater.GoToIdle();
                    }
                    shot = Instantiate(eggWater, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 4:
                    SoundCall(eggDrop, playerSounds);
                    shot = Instantiate(eggIce, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
                case 5:
                    SoundCall(eggDrop, playerSounds);
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
                    SoundCall(eggDrop, playerSounds);
                    shot = Instantiate(eggWind, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
            }
            pepperIndexA = pepperIndexB;
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
        anim.SetBool("isAttacking", false);
        StartCoroutine(shootWait());
    }

    void CollidingDeathRay () {
        if (!isImmune) {
            if (!playerSounds.isPlaying)
            {
                SoundCall(playerHit, playerVocals);
            }
            anim.SetBool("isHit", true);
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
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWind", false);
            yield return new WaitForSeconds(1.8f);
           

        }
        else if (pepperIndexA == 2) { 

            ChargeTime = 0;
            yield return new WaitForSeconds(0.4f);
            anim.SetBool("isAttacking", false);

        }
        else {
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("isAttacking", false);
            yield return new WaitForSeconds(0.5f);
        }
        canShoot = true;
    }

    private IEnumerator iFrames()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isHit", false);
        yield return new WaitForSeconds(0.9f);
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

    private IEnumerator BuffTime()
    {
        for (int i = 0; i < 20; i++) {
            BuffTimer = BuffTimer - 1;
            yield return new WaitForSeconds(1);
        }
    }

    private void ConsumableOverA()
    {
        pepperIndexA = pepperIndexB;
        pepperIndexB = 0;
        GameObject shot = Instantiate(shitBrick, transform.position + new Vector3(0, 0, 0), 
		    Quaternion.identity) as GameObject;
    }

    private void ConsumableOverB()
    {
        pepperIndexB = 0;
        GameObject shot = Instantiate(shitBrick, transform.position + new Vector3(0, 0, 0), 
		    Quaternion.identity) as GameObject;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Enemy") {
            isJumping = false;
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (pepperIndexA == 0 || pepperIndexB == 0) {

            if (collider.gameObject.name == "FirePepper(Clone)") {
                if (pepperIndexA != 1 && pepperIndexB != 1) {
                    PepperCollision(1);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "ShockPepper(Clone)") {
                if (pepperIndexA != 2 && pepperIndexB != 2) {
                    PepperCollision(2);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "WaterPepper(Clone)") {
                if (pepperIndexA != 3 && pepperIndexB != 3) {
                    PepperCollision(3);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "IcePepper(Clone)") {
                if (pepperIndexA != 4 && pepperIndexB != 4) {
                    PepperCollision(4);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "SpeedPepper(Clone)") {
                if (pepperIndexA != 5 && pepperIndexB != 5) {
                    PepperCollision(5);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "WindPepper(Clone)") {
                if (pepperIndexA != 6 && pepperIndexB != 6) {
                    PepperCollision(6);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "HealthPepper(Clone)") {
                if (pepperIndexA != 7 && pepperIndexB != 7) {
                    HealthTimer = 5;
                    PepperCollision(7);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "BuffPepper(Clone)") {
                if (pepperIndexA != 8 && pepperIndexB != 8) {
                    anim.SetBool("isBuff", true);
                    BuffTimer = 20;
                    PepperCollision(8);
                    Destroy(collider.gameObject);
                }
            }
            if (collider.gameObject.name == "EarthPepper(Clone)") {
                if (pepperIndexA != 9 && pepperIndexB != 9) {
                    PepperCollision(9);
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
                anim.SetBool("isHit", true);
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
                anim.SetBool("isHit", true);
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
                anim.SetBool("isHit", true);
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
                anim.SetBool("isHit", true);
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
            CancelInvoke("CollidingDeathRay");
        }
    }

    void PepperCollision(int pepperNumber) {
        if (pepperIndexA == 0) {
            SoundCall(pepperCollect, playerAmbient);
            pepperIndexA = pepperNumber;
        }
        else if (pepperIndexB == 0) {
            SoundCall(pepperCollect, playerAmbient);
            pepperIndexB = pepperNumber;
        }
    }

    void SoundCall (AudioClip clip, AudioSource source) {
        source.clip = clip;
        source.loop = false;
        source.loop |= (clip == playerFire || clip == playerWater || clip == shockLoop);
        source.Play();
    }

}
