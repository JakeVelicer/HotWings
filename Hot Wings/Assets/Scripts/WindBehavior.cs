using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehavior : MonoBehaviour {

	private Animator WindGrow;
	private playerControls Player;

	// Use this for initialization
	void Start () {

		WindGrow = gameObject.GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControls>();

		if (Player.facingRight == true) {
			WindGrow.Play("WindBlowRight");
		}
		else if (Player.facingRight == false) {
			WindGrow.Play("WindBlowLeft");
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (WindGrow.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !WindGrow.IsInTransition(0)) {
			Destroy (gameObject);
		}
		
	}
}
