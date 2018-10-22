using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControls : MonoBehaviour
{

    private EnemyDamageValues DamageEffects;
    private Rigidbody2D PlayerRigidbody;
    public System.Action OnPunch;
    public int moveSpeed = 10;
    public int jumpForce = 300;
    public bool isJumping;
    public bool canShoot = true;
    public int shotSpeed = 1000;
    private bool Healing;
    private float ChargeTime = 1;
    private float DamageMultiplier;

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

    // Use this for initialization
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        healthDisplay = GameObject.Find("Health").GetComponent<Text>();
        healthDisplay.text = "Health: " + health;

        playerFireShot.SetActive(false);
        playerWaterShot.SetActive(false);

        playerSounds = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "Health: " + health;

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
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
                        playerSounds.clip = playerFire;
                        playerSounds.loop = true;
                        playerSounds.Play();
                        playerFireShot.SetActive(true);
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        playerSounds.Stop();
                    }
                    break;
                case 2: // Water Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        playerSounds.clip = playerWater;
                        playerSounds.loop = true;
                        playerSounds.Play();
                        playerWaterShot.SetActive(true);
                    }
                    if (Input.GetKeyUp(KeyCode.Space)) {
                        playerSounds.Stop();
                    }
                    break;
                case 3: // CALLS Ice Pepper Power Attack
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        playerSounds.clip = playerIce;
                        playerSounds.loop = false;
                        playerSounds.Play();
                        canShoot = false;
                        StartCoroutine(IceBurst());
                    }
                    break;
                case 4: // Electric Shock Pepper Power Attack
                    if (Input.GetKey(KeyCode.Space))
                    {
                        ChargeTime = ChargeTime + Time.deltaTime;
                        Debug.Log(ChargeTime);
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        canShoot = false;
                        if (ChargeTime >= 3) {
                            playerSounds.clip = playerShock4;
                            playerSounds.loop = false;
                            playerSounds.Play();
                            ElectricShotToUse = playerShockShot4;
                        }
                        else if (ChargeTime >= 2) {
                            playerSounds.clip = playerShock3;
                            playerSounds.loop = false;
                            playerSounds.Play();
                            ElectricShotToUse = playerShockShot3;
                        }
                        else if (ChargeTime >= 1) {
                            playerSounds.clip = playerShock2;
                            playerSounds.loop = false;
                            playerSounds.Play();
                            ElectricShotToUse = playerShockShot2;
                        }
                        else if (ChargeTime < 1) {
                            playerSounds.clip = playerShock1;
                            playerSounds.loop = false;
                            playerSounds.Play();
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
                        playerSounds.clip = playerEarth;
                        playerSounds.loop = false;
                        playerSounds.Play();
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
                        playerSounds.clip = playerWind;
                        playerSounds.loop = false;
                        playerSounds.Play();
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
                            playerSounds.clip = playerBuff;
                            playerSounds.loop = false;
                            playerSounds.Play();
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerFireShot.SetActive(false);
            playerWaterShot.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            int tempIndex = pepperIndexA;
            string tempPepper = pepperA;

            pepperIndexA = pepperIndexB;
            pepperIndexB = tempIndex;

            pepperA = pepperB;
            pepperB = tempPepper;

            Debug.Log("Pepper A is now " + pepperA);
            Debug.Log("Pepper B is now " + pepperB);
        }
    }

    void EggBombs()
    {
        GameObject shot;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            switch (pepperIndexA)
            {
                case 1:
                    shot = Instantiate(eggFire, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    pepperA = pepperB;
                    pepperIndexA = pepperIndexB;
                    pepperB = null;
                    pepperIndexB = 0;
                    break;
                case 2:
                    shot = Instantiate(eggWater, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    pepperA = pepperB;
                    pepperIndexA = pepperIndexB;
                    pepperB = null;
                    pepperIndexB = 0;
                    break;
                case 3:
                    shot = Instantiate(eggIce, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    pepperA = pepperB;
                    pepperIndexA = pepperIndexB;
                    pepperB = null;
                    pepperIndexB = 0;
                    break;
                case 4:
                    shot = Instantiate(eggShock, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    pepperA = pepperB;
                    pepperIndexA = pepperIndexB;
                    pepperB = null;
                    pepperIndexB = 0;
                    break;
                case 6:
                    shot = Instantiate(eggWind, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                    pepperA = pepperB;
                    pepperIndexA = pepperIndexB;
                    pepperB = null;
                    pepperIndexB = 0;
                    break;
                case 8:
                    shot = Instantiate(playerIceShot, transform.position + new Vector3(0, 0, 0), 
			            Quaternion.identity) as GameObject;
                        if (facingRight)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        else if (!facingRight)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                    pepperA = pepperB;
                    pepperIndexA = pepperIndexB;
                    pepperB = null;
                    pepperIndexB = 0;
                    break;
            }
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
                yield return new WaitForSeconds(0.1f);
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

    private IEnumerator shootWait()
    {
        //Debug.Log("Counting down...");
        if (pepperIndexA == 8) {
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
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Enemy")
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "firePepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 1;
                pepperA = "firePepper";
            }
            else
            {
                pepperIndexB = 1;
                pepperB = "firePepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "waterPepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 2;
                pepperA = "waterPepper";
            }
            else
            {
                pepperIndexB = 2;
                pepperB = "waterPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "icePepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 3;
                pepperA = "icePepper";
            }
            else
            {
                pepperIndexB = 3;
                pepperB = "icePepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "shockPepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 4;
                pepperA = "shockPepper";
            }
            else
            {
                pepperIndexB = 4;
                pepperB = "shockPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "earthPepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 5;
                pepperA = "earthPepper";
            }
            else
            {
                pepperIndexB = 5;
                pepperB = "earthPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "windPepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 6;
                pepperA = "windPepper";
            }
            else
            {
                pepperIndexB = 6;
                pepperB = "windPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "buffPepper")
        {
            BuffTimer = 20;
            if (pepperIndexA == 0)
            {
                pepperIndexA = 7;
                pepperA = "buffPepper";
            }
            else
            {
                pepperIndexB = 7;
                pepperB = "buffPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "speedPepper")
        {
            if (pepperIndexA == 0)
            {
                pepperIndexA = 8;
                pepperA = "speedPepper";
            }
            else
            {
                pepperIndexB = 8;
                pepperB = "speedPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "healthPepper")
        {
            HealthTimer = 5;
            if (pepperIndexA == 0)
            {
                pepperIndexA = 9;
                pepperA = "healthPepper";
            }
            else
            {
                pepperIndexB = 9;
                pepperB = "healthPepper";
            }
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "enemyFist")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 3;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT1")
        {
            if (!isImmune)
            {
                playerSounds.clip = playerHit;
                playerSounds.loop = false;
                playerSounds.Play();
                isImmune = true;
                health -= 5;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT2")
        {
            if (!isImmune)
            {
                playerSounds.clip = playerHit;
                playerSounds.loop = false;
                playerSounds.Play();
                isImmune = true;
                health -= 5;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT3")
        {
            if (!isImmune)
            {
                playerSounds.clip = playerHit;
                playerSounds.loop = false;
                playerSounds.Play();
                isImmune = true;
                health -= 20;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyExplosion")
        {
            if (!isImmune)
            {
                playerSounds.clip = playerHit;
                playerSounds.loop = false;
                playerSounds.Play();
                isImmune = true;
                health -= 10;
                StartCoroutine(iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyDeathRay")
        {
            if (!isImmune)
            {
                playerSounds.clip = playerHit;
                playerSounds.loop = false;
                playerSounds.Play();
                isImmune = true;
                health -= 30;
                StartCoroutine(iFrames());
            }
        }
    }
}