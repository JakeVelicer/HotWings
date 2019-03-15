using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockChargeVisual : MonoBehaviour {

	private playerControls PlayerScript;
	private Animator Anim;

	// Use this for initialization
	void Start () {
		
		PlayerScript = GetComponentInParent<playerControls>();
		Anim = GetComponentInParent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerScript.pepperIndexA == 2) {

			if (PlayerScript.ChargeTime >= 3) {
				Anim.Play("ChargeUp4");
			}
			else if (PlayerScript.ChargeTime >= 2) {
				Anim.Play("ChargeUp3");
			}
			else if (PlayerScript.ChargeTime >= 1) {
				Anim.Play("ChargeUp2");
			}
			else if (PlayerScript.ChargeTime < 1 && PlayerScript.ChargeTime > 0) {
				Anim.Play("ChargeUp1");
			}
			else if (PlayerScript.ChargeTime <= 0) {
				Anim.Play("ChargeUpIdle");
			}

		}
		
	}
}
