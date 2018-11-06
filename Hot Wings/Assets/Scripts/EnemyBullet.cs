using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Destroy (gameObject, 2);
	}
	
	void OnBecameInvisible() {
        Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D collision) {
		
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
