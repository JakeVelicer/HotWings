using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int pepperIndexA;
    public int pepperIndexB;


    public bool facingRight;
    public GameObject playerFireShot;
    public GameObject playerWaterShot;
    public GameObject playerIceShot;
    public GameObject playerShockShot;
    public GameObject playerEarthShot;
    public GameObject playerWindShot;
    public GameObject playerBuffShot;

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            facingRight = true;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            facingRight = false;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (canShoot)
            {
                canShoot = false;
                GameObject shot = null;
                if (pepperIndexA == 1)
                {
                    shot = Instantiate(playerFireShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                else if (pepperIndexA == 2)
                {
                    shot = Instantiate(playerWaterShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                else if (pepperIndexA == 3)
                {
                    shot = Instantiate(playerIceShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                else if (pepperIndexA == 4)
                {
                    shot = Instantiate(playerShockShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                else if (pepperIndexA == 5)
                {
                    shot = Instantiate(playerEarthShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                else if (pepperIndexA == 6)
                {
                    shot = Instantiate(playerWindShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                else if (pepperIndexA == 7)
                {
                    shot = Instantiate(playerBuffShot, transform.position,
                    Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                }
                if (facingRight)
                {
                    shot.GetComponent<Rigidbody2D>().AddForce(Vector3.right * shotSpeed);
                }
                else
                {
                    shot.GetComponent<Rigidbody2D>().AddForce(Vector3.left * shotSpeed);
                }
                StartCoroutine(shootWait());
            }
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

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            isJumping = false;
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
        }
    }


}

