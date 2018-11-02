using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRayAnimation : MonoBehaviour {

	private Animator DeathRay;

	// Use this for initialization
	void Start () {

	}

	public void PlayBeamAnim() {
		DeathRay = gameObject.GetComponent<Animator>();
		//DeathRay.SetBool("Retract", false);
		DeathRay.Play("LaserShoot");
	}
	
	public void PlayRetractAnim() {
		DeathRay = gameObject.GetComponent<Animator>();
		//DeathRay.SetBool("Retract", true);
		DeathRay.Play("LaserRetract");
	}

}
