using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {
    public GameObject[] Clouds;
    //public Rigidbody[] Clouds2;
    public float waitTime; 
    public float moveSpeed;
    public Rigidbody Cloud;
    public Rigidbody Clone;  


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("cloudCreate", 2.0f, waitTime);
    }

    // Update is called once per frame
    void Update()
    { 
        //Instantiate(Clouds[Random.Range(0, Clouds.Length)]);
        //cloudCreate();
       // cloudWait();
    }
    void cloudCreate(){


        Instantiate (Clouds[Random.Range(0, Clouds.Length)], transform.position,transform.rotation);
        //Clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        StartCoroutine( CloudWait());
    }
    private IEnumerator CloudWait()
    {

        yield return new WaitForSeconds(waitTime);
        //cloudCreate();
        //return;

    }
}