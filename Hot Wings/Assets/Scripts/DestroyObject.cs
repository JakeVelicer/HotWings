using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	public int Timer;

	// Use this for initialization
	void Start () {

		Destroy(gameObject, Timer);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
