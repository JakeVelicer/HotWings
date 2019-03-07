using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamAttackAnimationFire : MonoBehaviour {

	[HideInInspector] public Animator Anim;
	private playerControls PlayerScript;

	// Use this for initialization
	void Start () {

		Anim = gameObject.GetComponent<Animator>();
		PlayerScript = transform.GetComponentInParent<playerControls>();
		
	}

	void Update() {

		if (this.Anim.GetCurrentAnimatorStateInfo(0).IsName("Loop")) {
			gameObject.GetComponent<Collider2D>().enabled = true;
		}
		else {
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
		if (PlayerScript.pepperIndexA != 1 && !this.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			Idle();
		}
	}

	public void StartBeam() {
		Anim.Play("Start");
	}

	public void GoToIdle() {
		Anim.Play("Finish");
	}

	public void Idle() {
		Anim.Play("StreamIdle");
	}

}
