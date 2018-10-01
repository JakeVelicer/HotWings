﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	// Use this for initialization
	private Rigidbody2D Rb;
	private BasicEnemyControls BasicEnemy;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 5);

	}
	
	// Update is called once per frame
	void Update () {

		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
