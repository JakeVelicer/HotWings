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
 		if (this.Anim.GetCurrentAnimatorStateInfo(0).IsName("Finish")) {
			gameObject.GetComponent<Collider2D>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void StartBeam() {
		Anim.Play("Start");
	}

	public void GoToIdle() {
		Anim.Play("Finish");
	}

}
