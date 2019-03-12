using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRayAnimation : MonoBehaviour {

	private Animator DeathRay;
	private Collider2D BeamCollider;

	// Use this for initialization
	void Start () {

		DeathRay = gameObject.GetComponent<Animator>();
		BeamCollider = gameObject.GetComponent<Collider2D>();
	}

	public void TurnOnBeam() {

		BeamCollider.enabled = true;
		DeathRay.Play("LaserShoot");
	}
	
	public void TurnOffBeam() {

		BeamCollider.enabled = false;
		DeathRay.Play("LaserRetract");
	}

}
