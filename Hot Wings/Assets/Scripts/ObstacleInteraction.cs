using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInteraction : MonoBehaviour {
    public GameObject player;
    public Vector3 newPostion;
    private Collider2D checker;
    public float heightchecker;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        checker = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
       newPostion = player.transform.position;  

        if(newPostion.y < heightchecker)
        {

            checker.enabled = false;


        } 

        if(newPostion.y > heightchecker)
        {
            checker.enabled = true;


        }


    }
}
