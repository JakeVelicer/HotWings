using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 cameraPosition = Vector3.zero;
    public Transform target;

    private Vector3 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
    public GameObject player;  
    public bool bounds; 
    public Vector3 minCameraPosition; 
    public Vector3 maxCameraPosition;

    private AudioSource bgm;
    public AudioClip gameplaySong;
    
	// Use this for initialization
	void Start () {

        bgm = gameObject.GetComponent<AudioSource>();
        bgm.clip = gameplaySong;
        bgm.loop = true;
        bgm.Play();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

       



        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bounds){

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                                             Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                                             Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
        }
    }

  
}