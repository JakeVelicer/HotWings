﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControls : MonoBehaviour {

	private Rigidbody2D Rigidbody;
	private Transform Target;
	private bool CanChase;

	public float MovementSpeed;
	public float ChaseRange;
	public float FireRange;
	public float ProjectileSpeed;
	public float ProjectileHeight;
	private float CoolDownTimer = 0;
	
	public bool ToTheRight;
	public GameObject BulletObject;
	public GameObject BombObject;
	public int AlientType;

	// Use this for initialization
	void Start () {

		// Gets the Rigidbody of the game object this script is on
		//Rigidbody = GetComponent<Rigidbody2D> ();
		//BasicAttack = GetComponent<BasicEnemyAttack> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		// Finds the Player's transform and stores it in target
		Target = GameObject.FindGameObjectWithTag ("Player").transform;

		Movement();
		ChaseTarget();
		
	}

	// Controls the actual movement of the object
	void Movement () {

		// Checks if it is allowed to chase the player
		if (CanChase == true) {

			// Pushes the enemy in a direction based upon which side the player is on
			if (ToTheRight == false) {
			transform.Translate (Vector3.left * Time.deltaTime * MovementSpeed);
			}
			else if (ToTheRight == true) {
			transform.Translate (Vector3.right * Time.deltaTime * MovementSpeed);
			}
			//Rigidbody.AddForce (MovementSpeed * new Vector2 (1,0));
		}
	}

	void ChaseTarget () {

		// Determines if the range of the player is close enough to be chased
		if (Vector3.Distance(Target.position, transform.position) <= ChaseRange &&
		Vector3.Distance(Target.position, transform.position) > FireRange) {
			CanChase = true;
			ChaseDirection();
		}
		// Tells the player to attack if close enough
		else if (Vector3.Distance(Target.position, transform.position) <= FireRange) {
			CanChase = false;
			switch (AlientType) {
				case 2: 
     				if (CoolDownTimer <= 0) {
						AttackPhase1();
						CoolDownTimer = 1.2f;
					}
					CoolDownTimer -= Time.deltaTime;
					ChaseDirection();
					break;
				case 3:
     				if (CoolDownTimer <= 0) {
						AttackPhase1();
						CoolDownTimer = 1.0f;
					}
					CoolDownTimer -= Time.deltaTime;
					ChaseDirection();
					break;
				case 4:
     				if (CoolDownTimer <= 0) {
						AttackPhase2();
						CoolDownTimer = 1.5f;
					}
					CoolDownTimer -= Time.deltaTime;
					ChaseDirection();
					break;
				case 5:
     				if (CoolDownTimer <= 0) {
						AttackPhase1();
						CoolDownTimer = 0.2f;
					}
					CoolDownTimer -= Time.deltaTime;
					ChaseDirection();
					break;
			}
		}
		// Does nothing if out of range of chasing and attacking, will roam eventually
		else {
			CanChase = false;
			CoolDownTimer = 0;
		}
	}

	// Determines the direction the object faces when chasing
	void ChaseDirection () {

		if (Target.position.x > transform.position.x + 1) {
			transform.localScale = new Vector3(-1, 1, 1);
			ToTheRight = true;
		}
		else if (Target.position.x < transform.position.x + 1) {
			transform.localScale = new Vector3(1, 1, 1);
			ToTheRight = false;
		}
	}

	// Instantiates a chosen projectile in the scene and propels it
	void AttackPhase1 () {

		if (ToTheRight == true) {
			GameObject Projectile = Instantiate (BulletObject, transform.position, 
			Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
		}
		else if (ToTheRight == false) {
			GameObject Projectile = Instantiate (BulletObject, transform.position, 
			Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}
	
	}

	void AttackPhase2 () {

		if (ToTheRight == true) {
			GameObject Projectile = Instantiate (BombObject, transform.position + new Vector3(0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ProjectileSpeed);
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
		}
		else if (ToTheRight == false) {
			GameObject Projectile = Instantiate (BombObject, transform.position + new Vector3(-0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ProjectileSpeed);
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}

	}
}