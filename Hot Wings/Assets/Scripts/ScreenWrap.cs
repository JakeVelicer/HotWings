using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour {

    public float rotationSpeed;
    public float thrustForce;

    public Rigidbody2D rb2D; 

    public float horizontal;
    public float vertical;

    public Renderer[] renderers;

    public bool isWrappingX = false; 

    public bool isWrappingY = false;


    



    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<Renderer>(); 
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        horizontal = Input.GetAxis("Horizontal");
        horizontal = Input.GetAxis("Vertical");

        rb2D.angularVelocity = -horizontal * rotationSpeed;
        rb2D.AddForce(transform.up * vertical * thrustForce);

        ScreenWrapping();


    } 

    void ScreenWrapping(){

        bool isVisible = CheckRenderers(); 

        if(isVisible){

            isWrappingX = false;
            isWrappingY = false;

            return; 

        }
        if(isWrappingX && isWrappingY){


            return; 

        }
        Vector3 newPostion = transform.position; 

        if(newPostion.x>1 || newPostion.x <0){
            newPostion.x = -newPostion.x;

            isWrappingX = true; 


        }

        if (newPostion.y > 1 || newPostion.y < 0)
        {
            newPostion.y = +newPostion.y;

            isWrappingY = true;


        } 
        transform.position=newPostion;



    } 

    bool CheckRenderers(){

        foreach(Renderer renderer in renderers){

            if (renderer.isVisible)
            {

                return true;
            }



            }
        return false; 

        }


    }

