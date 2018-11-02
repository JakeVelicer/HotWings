using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChecker : MonoBehaviour {
    Animator anim;
    int idleHash = Animator.StringToHash("HotwingsIDLE");
    int runStateHash = Animator.StringToHash("Base Layer.HotwingsRun1");


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", move);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.LeftArrow)   && stateInfo.fullPathHash == idleHash || Input.GetKeyDown(KeyCode.RightArrow) && stateInfo.fullPathHash == idleHash)
        {
            anim.SetTrigger(runStateHash);
        }
    }
}
