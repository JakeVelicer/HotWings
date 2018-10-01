using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour {

    public int moveSpeed = 10;
    public int jumpForce = 300;
    public bool isJumping;
    public float fireRate;
    public bool canShoot = true;
    public int shotSpeed = 20;
    public bool isSingleShot = true;
    public bool isFiring = false;
    public string pepperA = "";
    public string pepperB = "";

    public bool facingRight;
    public GameObject playerSingleShot;

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
                GameObject shot = Instantiate(playerSingleShot, transform.position,
                Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
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
    }


}

