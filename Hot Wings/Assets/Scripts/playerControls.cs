using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class playerControls : MonoBehaviour
{

    private Rigidbody2D PlayerRigidbody;
    private Vector2 velocity;
    public System.Action OnPunch;
    private StreamAttackAnimationFire StreamAnimFire;
    private StreamAttackAnimationWater StreamAnimWater;
    [HideInInspector] public Animator anim;
	public Material NormalMaterial;
	public Material HotFlash;

    public int moveSpeed;
    public int jumpForce;
    public bool isJumping;
    private bool Dashing;
    private bool isBuff;
    public bool canShoot = true;
    private bool isAxisInUse;
    [HideInInspector] public bool Dead;
    public int shotSpeed;
    public int DashSpeed;
    private bool Healing;
    private float horizontalInput;
    private float currentMoveSpeed;
    private float FireControls;
    [HideInInspector] public float ChargeTime;

    //Pepper references
    public int pepperIndexA;
    public int pepperIndexB;

    public int health;
    [HideInInspector] public int BuffTimer;
    [HideInInspector] public int HealthTimer;
    [HideInInspector] public int DashCount;
    [HideInInspector] public float FireCoolDown;
    [HideInInspector] public float WaterCoolDown;
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
    public AudioClip brick;
    private bool buffPunch1 = true;

    public AudioClip playerDash;
    public AudioClip playerHeal;

    public AudioClip pepperCollect;
    public AudioClip eggDrop;
    public AudioClip playerHit;
    public AudioClip playerDeath;
    public AudioClip shockLoop;
    private bool shockLoopPlaying = false;
    private bool waterLoopPlaying = false;
    private bool fireLoopPlaying = false;
    private bool buffSoundPlaying = false;

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
		gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }

    // Update is called once per frame, movement, animations, attacks called
    void Update() {

        PepAttacks();
        EggBombs();
        SlotBCleanup();

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        AnimChecker = anim.GetInteger("Speed");
        if (!Dead) {
            
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isIdle", false);
                isJumping = true;
                PlayerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            horizontalInput = Input.GetAxis("Horizontal"); //a,d, left, and right
            currentMoveSpeed = Mathf.Clamp (currentMoveSpeed, 0f, moveSpeed);
            
            if (horizontalInput > 0)
            {
                currentMoveSpeed += 30 * Time.fixedDeltaTime;
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
                transform.localScale = new Vector3(1, 1, 1);
                facingRight = true;
            }
            else if (horizontalInput < 0)
            {
                currentMoveSpeed += 30 * Time.fixedDeltaTime;
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
                transform.localScale = new Vector3(-1, 1, 1);
                facingRight = false;
            }
            else if (horizontalInput == 0)
            {
                if (currentMoveSpeed > 0) {
                    currentMoveSpeed = 0;
                }
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
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
            if (health <= 0) {
                anim.SetBool("isDead", true);
                StreamAnimFire.GetComponent<SpriteRenderer>().enabled = false;
                StreamAnimWater.GetComponent<SpriteRenderer>().enabled = false;
                PlayerRigidbody.velocity = Vector2.zero;
                Dead = true;
            }
            if (pepperIndexA != 1 && !fireLoopPlaying && FireCoolDown < 4) {
                FireCoolDown = (FireCoolDown + 0.03f) + Time.fixedDeltaTime;
            }
            if (pepperIndexA != 3 && !waterLoopPlaying && WaterCoolDown < 4) {
                WaterCoolDown = (WaterCoolDown + 0.03f) + Time.fixedDeltaTime;
            }
        }

    }

    void FixedUpdate() {

        // Movement
        if (!Dashing && !Dead) {
            velocity = PlayerRigidbody.velocity;
            velocity.y += Physics2D.gravity.y * 0.05f;
            velocity.x = horizontalInput * currentMoveSpeed;
            PlayerRigidbody.velocity = velocity;
        }

    }

    void PepAttacks() {

        FireControls = Input.GetAxisRaw("FireControls");
        bool ControllerConnected;

        if (Input.GetJoystickNames().Length >= 1)
            ControllerConnected = true;
        else 
            ControllerConnected = false;

        if (canShoot && !Dead)
        {
            switch (pepperIndexA)
            {
                case 1: // Fire Pepper Power Attack
                    if (FireControls >= 1) {
                        if (FireCoolDown > 0 && !isAxisInUse) {
                            isAxisInUse = true;
                            FireAttackStart();
                        }
                    }
                    if (FireControls <= 0 && isAxisInUse) {
                        isAxisInUse = false;
                        FireAttackStop();
                    }
                    if (FireCoolDown <= 0) {
                        FireAttackStop();
                    }
                    if (FireControls >= 1 && fireLoopPlaying) {
                        FireCoolDown = FireCoolDown - Time.fixedDeltaTime;
                    }
                    if (!fireLoopPlaying && FireCoolDown < 4 && FireControls != 1) {
                        FireCoolDown = (FireCoolDown + 0.03f) + Time.fixedDeltaTime;
                    }
                    break;
                case 2: // Electric Shock Pepper Power Attack
                    if (FireControls >= 1) {
                        isAxisInUse = true;
                        if (!shockLoopPlaying)
                        {
                            SoundCall(shockLoop, playerAmbient);
                            shockLoopPlaying = true;
                        }
                        ChargeTime = ChargeTime + Time.fixedDeltaTime;
                    }
                    if (FireControls <= 0 && isAxisInUse) {
                        canShoot = false;
                        isAxisInUse = false;
                        ShockAttack();
                    }
                    break;
                case 3: // Water Pepper Power
                    if (FireControls >= 1) {
                        if (WaterCoolDown > 0 && !isAxisInUse) {
                            isAxisInUse = true;
                            WaterAttackStart();
                        }
                    }
                    if (FireControls <= 0 && isAxisInUse) {
                        isAxisInUse = false;
                        WaterAttackStop();
                    }
                    if (WaterCoolDown <= 0) {
                        WaterAttackStop();
                    }
                    if (FireControls >= 1 && waterLoopPlaying) {
                        WaterCoolDown = WaterCoolDown - Time.fixedDeltaTime;
                    }
                    if (!waterLoopPlaying && WaterCoolDown < 4 && FireControls != 1) {
                        WaterCoolDown = (WaterCoolDown + 0.03f) + Time.fixedDeltaTime;
                    }
                    break;
                case 4: // CALLS Ice Pepper Power Attack
                    if (FireControls >= 1) {
                        canShoot = false;
                        StartCoroutine(IceBurst());
                    }
                    break;
                case 5: // CALLS Speed Dash Pepper Power Attack
                    if (FireControls >= 1) {
                        canShoot = false;
                        if (DashDirection == 0) {
                            StartCoroutine(SpeedDash());
                        }
                    }
                    if (DashCount <= 0) {
                        ConsumableOverA();
                    }
                    break;
                case 6: // CALLS Wind Pepper Power Attack
                    if (FireControls >= 1) {
                        canShoot = false;
                        WindAttack();
                    }
                    break;
                case 7: // CALLS Health Pepper Power heal
                    if (FireControls >= 1 && !Healing) {
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
                    if (FireControls >= 1) {
                        //canShoot = false;
                        if (OnPunch != null) {
                            anim.SetBool("isAttacking", true);
                            OnPunch();
                            BuffAudioHandler();
                        }
                    }
                    if (FireControls <= 0) {
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
                case 9: // CALLS Earth Pepper Power Attack
                    if (FireControls >= 1) {
                        canShoot = false;
                        EarthAttack();
                    }
                    break;
            }
        }
        if (Input.GetButtonUp("Switch") || Input.GetKeyUp(KeyCode.RightShift))
        {
            if (shockLoopPlaying) {
                shockLoopPlaying = false;
                playerAmbient.Stop();
            }
            if (fireLoopPlaying) {
                FireAttackStop();
            }
            if (waterLoopPlaying) {
                WaterAttackStop();
            }
            if (pepperIndexA != 8) {
                int tempIndex = pepperIndexA;
                pepperIndexA = pepperIndexB;
                pepperIndexB = tempIndex;
            }
        }
    }

    private IEnumerator BuffAudioHandler()
    {
        if (buffPunch1 == true & buffSoundPlaying == false)
        {
            buffSoundPlaying = true;
            SoundCall(playerBuff, playerSounds);
            buffSoundPlaying = false;
        }
        else if (buffPunch1 == false & buffSoundPlaying == false)
        {
            buffSoundPlaying = true;
            SoundCall(playerBuff2, playerSounds);
            buffSoundPlaying = false;
        }
        buffPunch1 = !buffPunch1;
        yield return new WaitForSeconds(0.5f);
    }
    void SlotBCleanup () {

        switch (pepperIndexB) {
            case 5: // Dash Pepper Clean Up
                if (DashCount <= 0) {
                    ConsumableOverB();
                }
                break;
            case 7: // Health Pepper Clean up
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

        if (Input.GetButtonDown("Drop Pepper"))
        {
            if (shockLoopPlaying == true)
            {
                shockLoopPlaying = false;
                playerAmbient.Stop();
            }
            if (fireLoopPlaying == true || waterLoopPlaying == true)
            {
                fireLoopPlaying = false;
                waterLoopPlaying = false;
                playerSounds.Stop();
            }
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
                /*
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
                */
                case 6:
                    SoundCall(eggDrop, playerSounds);
                    shot = Instantiate(eggWind, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    break;
            }
            if (pepperIndexA != 5 && pepperIndexA != 7 && pepperIndexA != 8 && pepperIndexA != 9) {
                pepperIndexA = pepperIndexB;
                pepperIndexB = 0;
            }
        }
    }

    private void FireAttackStart() {

        fireLoopPlaying = true;
        anim.SetBool("isAttacking", true);
        SoundCall(playerFire, playerSounds);
        StreamAnimFire.StartBeam();
    }

    private void FireAttackStop() {

        if (fireLoopPlaying) {
            fireLoopPlaying = false;
            anim.SetBool("isAttacking", false);
            StreamAnimFire.GoToIdle();
            playerSounds.Stop();
        }
    }

    private void WaterAttackStart() {

        waterLoopPlaying = true;
        anim.SetBool("isAttacking", true);
        SoundCall(playerWater, playerSounds);
        StreamAnimWater.StartBeam();
    }

    private void WaterAttackStop() {

        if (waterLoopPlaying) {
            waterLoopPlaying = false;
            anim.SetBool("isAttacking", false);
            StreamAnimWater.GoToIdle();
            playerSounds.Stop();
        }
    }

    private IEnumerator IceBurst() {

        anim.SetBool("isAttacking", true);
        SoundCall(playerIce, playerSounds);
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

    private void ShockAttack() {

        shockLoopPlaying = false;
        playerAmbient.Stop();
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
        GameObject shot = Instantiate(ElectricShotToUse, transform.position + new Vector3(0, 0, 0),
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

    private void WindAttack() {
        
        anim.SetBool("isWind", true);
        SoundCall(playerWind, playerSounds);
        GameObject shot;
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

    private IEnumerator SpeedDash() {

        Dashing = true;
        DashCount--;
        playerDashCollider.GetComponent<Collider2D>().enabled = true;
        SoundCall(playerDash, playerSounds);

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

    private void EarthAttack() {

        SoundCall(playerEarth, playerSounds);
        Instantiate(playerEarthShot, new Vector3 (transform.position.x, -2.55f, 0), Quaternion.identity);
        GameObject.Find("Controller").GetComponent<ScreenShake>().BombGoesOff(0.6f);
        ConsumableOverA();
        StartCoroutine(shootWait());
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

    public IEnumerator iFrames() {
        
        //for (int i = 0; i < 2; i++) {
        GetComponent<SpriteRenderer>().material = HotFlash;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isHit", false);
        GetComponent<SpriteRenderer>().material = NormalMaterial;
        yield return new WaitForSeconds(0.6f);
        //}
        isImmune = false;
    }

    private IEnumerator HealThePlayer() {

        SoundCall(playerHeal, playerAmbient);
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
        SoundCall(brick, playerAmbient);
    }

    private void ConsumableOverB()
    {
        pepperIndexB = 0;
        GameObject shot = Instantiate(shitBrick, transform.position + new Vector3(0, 0, 0), 
		    Quaternion.identity) as GameObject;
        SoundCall(brick, playerAmbient);
    }

    public void PepperCollision(int pepperNumber) {
        if (pepperIndexA == 0) {
            SoundCall(pepperCollect, playerSounds);
            pepperIndexA = pepperNumber;
        }
        else if (pepperIndexB == 0) {
            SoundCall(pepperCollect, playerSounds);
            pepperIndexB = pepperNumber;
        }
    }

    public void SoundCall (AudioClip clip, AudioSource source) {
        source.clip = clip;
        source.loop = false;
        source.loop |= (clip == playerFire || clip == playerWater || clip == shockLoop);
        source.Play();
    }

}
