using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    Vector3 originalCameraPosition;

    float shakeAmt = 0;

    public Camera mainCamera;

    public void BombGoesOff(float InputedAmount)
    {
        shakeAmt = InputedAmount;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
    }

    // Credit - (http://newbquest.com/2014/06/the-art-of-screenshake-with-unity-2d-script/)
    void CameraShake()
    {
        if(shakeAmt>0) 
        {
            float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y+= quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        //mainCamera.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
