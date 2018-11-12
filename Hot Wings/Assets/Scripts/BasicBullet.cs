using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1.3f);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}
		if (gameObject.tag == "Ice" && collision.gameObject.tag == "IceBlock") {
			Destroy (gameObject);
		}
	}
}
