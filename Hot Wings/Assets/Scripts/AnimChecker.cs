using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChecker : MonoBehaviour {


    Animator anim;
    public bool isRunning;
    public bool isAttacking;
    public bool isJumping;  
    


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", move);

       
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||  Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)){

            isRunning = false;
        }
    }
}
