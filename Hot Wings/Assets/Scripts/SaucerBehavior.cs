using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerBehavior : MonoBehaviour {

	private Transform Target;
	private bool CanChase;
	private bool ToTheRight;
	private bool CanFireRay;
	public int MovementSpeed;

	// Use this for initialization
	void Start () {

		Target = GameObject.FindGameObjectWithTag ("Player").transform;
		
	}
	
	// Update is called once per frame
	void Update () {

		float dist = Vector3.Distance(Target.position, transform.position);

		if (dist <= 10 && dist > 0.8) {
			CanChase = true;
			ChaseDirection();
			if (CanFireRay == false) {
					//SaucerRay.SetActive(false);
					//StartCoroutine(RayTime());
			}
			Debug.Log(dist);
		}
		else if (dist > 20) {
			CanChase = false;
			ChaseDirection();
			Debug.Log(dist);
		}
		// Does nothing if out of range of chasing and attacking, will roam eventually
		else {
			CanChase = false;
		}
		Movement();
	}

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
		}
	}

	void ChaseDirection () {

		if (Target.position.x > transform.position.x + 0.5) {
			transform.localScale = new Vector3(-1, 1, 1);
			ToTheRight = true;
		}
		else if (Target.position.x < transform.position.x + 0.5) {
			transform.localScale = new Vector3(1, 1, 1);
			ToTheRight = false;
		}
	}
}
