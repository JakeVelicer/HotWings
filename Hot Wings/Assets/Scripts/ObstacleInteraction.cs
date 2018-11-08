using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInteraction : MonoBehaviour {
    public GameObject player;
    public Vector3 newPostion;
    public PolygonCollider2D checker;
    public float heightchecker;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
       checker = GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
       newPostion = player.transform.position;

        if (newPostion.y > heightchecker)
        {
            checker.enabled = true;


        }else{

            checker.enabled = false;
        }
    

    }
}
