using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour {

    public int moveSpeed = 10;
    public int jumpForce = 300;
    public bool isJumping;
    
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping || Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D (Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
            isJumping = false;
    }
}

