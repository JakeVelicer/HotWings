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

		Rigidbody = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		Target = GameObject.FindGameObjectWithTag ("Player").transform;
		Movement();
		ChaseTarget();
		
	}

	void Movement () {

		if (CanChase == true) {

			if (ToTheRight == false) {
			Rigidbody.AddForce (MovementSpeed * new Vector2 (-1,0));
			}
			else if (ToTheRight == true) {
			Rigidbody.AddForce (MovementSpeed * new Vector2 (1,0));
			}

		}
	}

	void ChaseTarget () {

		if (Vector3.Distance(Target.position, transform.position) <= ChaseRange) {
			CanChase = true;
			ChaseDirection();
		}
		else CanChase = false;

	}

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