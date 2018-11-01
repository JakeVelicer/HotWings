using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

	private Rigidbody2D Rb;
	private BasicEnemyControls BasicEnemy;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}
	}
}
