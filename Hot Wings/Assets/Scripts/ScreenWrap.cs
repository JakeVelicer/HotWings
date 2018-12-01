using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    public GameObject mainCamera;

    private float rotationSpeed;
    private float thrustForce;
    private Rigidbody2D rb2D;
    public GameObject Player;
    //public float PlayerFall;
    private float horizontal;
    private float vertical;
    private Renderer[] renderers;

    private bool isWrappingX = false;
    private bool isWrappingY = false;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        rb2D = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<Renderer>();
        //PlayerFall = Player.GetComponent<playerControls>().PlayerRigidbody.velocity.y; 

	}
	
	// Update is called once per frame
	void LateUpdate () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //rb2D.angularVelocity = -horizontal * rotationSpeed;
        //rb2D.AddForce(transform.up * vertical * thrustForce);

        ScreenWrapping();

    } 

    void ScreenWrapping() {

        bool isVisible = CheckRenderers(); 

        if(isVisible ) {

            isWrappingX = false;
            isWrappingY = false;
            mainCamera.GetComponent<CameraFollow>().smoothTimeX = .5f;
            mainCamera.GetComponent<CameraFollow>().smoothTimeY = .5f;
            return; 
        }
       /* if (isVisible)
        {

            isWrappingX = false;
            isWrappingY = false;
            mainCamera.GetComponent<CameraFollow>().smoothTimeX = .2f;
            mainCamera.GetComponent<CameraFollow>().smoothTimeY = .2f;
            return;
        }*/
        if (isWrappingX && isWrappingY) {
            return;
        }

        Vector3 newPostion = transform.position;

        if (newPostion.x>1 || newPostion.x <0) {
            mainCamera.GetComponent<CameraFollow>().smoothTimeX = 0f;
            mainCamera.GetComponent<CameraFollow>().smoothTimeY = 0f;
            newPostion.x = -newPostion.x;
            isWrappingX = true;

        }
        if (newPostion.y > 1 || newPostion.y < 0) {
            mainCamera.GetComponent<CameraFollow>().smoothTimeX = 0f;
            mainCamera.GetComponent<CameraFollow>().smoothTimeY = 0f;
            newPostion.y = +newPostion.y;
            isWrappingY = true;

        } 
        transform.position=newPostion;

    }

    bool CheckRenderers() {

        foreach(Renderer renderer in renderers ) {

            if (Player.transform.position.x > -57 && Player.transform.position.x < 57) {

                return true;
            }
        }
        return false; 

    }
}