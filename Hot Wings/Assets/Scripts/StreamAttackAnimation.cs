using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamAttackAnimation : MonoBehaviour {

	private Animator StreamAttack;

	// Use this for initialization
	void Start () {
		
	}
	
	public void PlayBeamAnimFlame() {
		StreamAttack = gameObject.GetComponent<Animator>();
		StreamAttack.Play("FireSpray");
	}

	public void PlayRetractAnimFlame() {
		StreamAttack = gameObject.GetComponent<Animator>();
		StreamAttack.Play("FireRetract");
	}

	public void PlayBeamAnimWater() {
		StreamAttack = gameObject.GetComponent<Animator>();
		StreamAttack.Play("WaterSpray");
	}

	public void PlayRetractAnimWater() {
		StreamAttack = gameObject.GetComponent<Animator>();
		StreamAttack.Play("WaterRetract");
	}
}
