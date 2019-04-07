using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketFollow : MonoBehaviour {

	private GameObject mainCamera;

	// Use this for initialization
	void Start () {

		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3
		(mainCamera.transform.position.x, transform.position.y, transform.position.z);
		
	}
}
