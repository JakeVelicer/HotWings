using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFloor : MonoBehaviour {

	private Collider2D PlayerCollider;
	private Collider2D Collider;

	// Use this for initialization
	void Start () {

		PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
		Collider = gameObject.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis("Vertical") < -0.5 && Collider.IsTouching(PlayerCollider)) {
			Collider.isTrigger = true;
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Collider.isTrigger = false;
		}
	}
}
