using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControls : MonoBehaviour {

    public int moveSpeed = 10;
    public int jumpForce = 300;
    public bool isJumping;
    public float fireRate;
    public bool canShoot = true;
    public int shotSpeed = 1000;
    public bool isSingleShot = true;
    public bool isFiring = false;
    public string pepperA = null;
    public string pepperB = null;
    public int pepperIndexA = 1;
    public int pepperIndexB;
    public Text healthDisplay;

    public int health = 500;

    public bool isImmune = false;
    public bool facingRight = true;

    public GameObject playerFireShot;
    public GameObject playerWaterShot;
    public GameObject playerIceShot;
    public GameObject playerShockShot;
    public GameObject playerEarthShot;
    public GameObject playerWindShot;
    public GameObject playerBuffShot;

    // Use this for initialization
    void Start () {
       
        healthDisplay.text = "Health: " + health;
        playerFireShot.SetActive(false);
        playerWaterShot.SetActive(false);
        playerWindShot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "Health: " + health;
       
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
			facingRight = true;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
			facingRight = false;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        Attacks ();
    }

    void Attacks() {
        if (Input.GetKey(KeyCode.Space))
        {
            if (canShoot)
            {
                GameObject shot;

                switch (pepperIndexA) {
                    case 1:
                        playerFireShot.SetActive(true);
                        break;
                    case 2:
                        playerWaterShot.SetActive(true);
                        break;
                    case 3:
                        canShoot = false;
                        shot = Instantiate(playerIceShot, transform.position,
                        Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        if (facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                        else if (!facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        StartCoroutine(shootWait());
                        break;
                    case 4:
                        canShoot = false;
                        shot = Instantiate(playerShockShot, transform.position,
                        Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        if (facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                        else if (!facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        StartCoroutine(shootWait());
                        break;
                    case 5:
                        canShoot = false;
                        shot = Instantiate(playerEarthShot, transform.position,
                        Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        if (facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                        else if (!facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        StartCoroutine(shootWait());
                        break;
                    case 6:
                        playerWindShot.SetActive(true);
                        break;
                    case 7:
                        canShoot = false;
                        shot = Instantiate(playerBuffShot, transform.position,
                        Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        if (facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                        }
                        else if (!facingRight && pepperIndexA != 1 && pepperIndexA != 2 && pepperIndexA != 6)
                        {
                            shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                        }
                        StartCoroutine(shootWait());
                        break;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerFireShot.SetActive(false);
            playerWaterShot.SetActive(false);
            playerWindShot.SetActive(false);
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

    public IEnumerator shootWait()
    {
        Debug.Log("Counting down...");
        yield return new WaitForSeconds(1.0f);
        canShoot = true;
    }

    public IEnumerator iFrames()
    {
       yield return new WaitForSeconds(1.0f);
        isImmune = false;
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
        if (collider.gameObject.tag == "healthPepper")
        {
            health += 100;
            if (health > 500){
                health = 500;
                healthDisplay.text = "Health: " + health;

            }
            if (health < 500){
                healthDisplay.text = "Health: " + health;

            }
            Destroy(collider.gameObject);
        }
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
        if (collider.gameObject.tag == "enemyFist")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 3;
            }
            StartCoroutine(iFrames());

        }
        if (collider.gameObject.tag == "enemyShotT1")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 5;
            }
            StartCoroutine(iFrames());
        }
        if (collider.gameObject.tag == "enemyShotT2")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 5;
            }
            StartCoroutine(iFrames());
        }
        if (collider.gameObject.tag == "enemyShotT3")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 20;
            }
            StartCoroutine(iFrames());
        }
        if (collider.gameObject.tag == "enemyExplosion")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 10;
            }
            StartCoroutine(iFrames());
        }
        if (collider.gameObject.tag == "enemyDeathRay")
        {
            if (!isImmune)
            {
                isImmune = true;
                health -= 30;
            }
            StartCoroutine(iFrames());
        }
    }
}