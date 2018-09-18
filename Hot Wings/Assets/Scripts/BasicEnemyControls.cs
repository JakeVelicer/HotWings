using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControls : MonoBehaviour {

	private Rigidbody2D Rigidbody;
	private Transform Target;
	private bool CanChase;
	public float ChaseRange = 10;
	public float MovementSpeed = 0;
	public bool ToTheRight;

	// Use this for initialization
	void Start () {

		// Gets the Rigidbody of the game object this script is on
		Rigidbody = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		// Finds the Player's transform and stores it in target
		Target = GameObject.FindGameObjectWithTag ("Player").transform;

		Movement();
		ChaseTarget();
		
	}

	// Controls the actual moevment of the object
	void Movement () {

		// Checks if it is allowed to chase the player
		if (CanChase == true) {

			// Pushes the enemy in a direction based upon which side the player is on
			if (ToTheRight == false) {
			Rigidbody.AddForce (MovementSpeed * new Vector2 (-1,0));
			}
			else if (ToTheRight == true) {
			Rigidbody.AddForce (MovementSpeed * new Vector2 (1,0));
			}

		}
	}

	// Determines if the range of the player is close enough to be chased
	void ChaseTarget () {

		if (Vector3.Distance(Target.position, transform.position) <= ChaseRange) {
			CanChase = true;
			ChaseDirection();
		}
		else CanChase = false;

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
}