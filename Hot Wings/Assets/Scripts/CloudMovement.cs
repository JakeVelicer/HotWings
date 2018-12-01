using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
    public float moveSpeed; 
    public Rigidbody2D CloudRigidbody;
	// Use this for initialization
	void Start () {
        CloudRigidbody =  GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        CloudRigidbody.velocity = new Vector3(moveSpeed, 0, 0); 

        if(this.gameObject.transform.position.x > 72){

            Destroy(this.gameObject);
        }

    }
}
