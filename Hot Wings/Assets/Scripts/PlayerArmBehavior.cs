using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmBehavior : MonoBehaviour {

	private Animator PunchAnim;
	private PolygonCollider2D Collider;
	private playerControls Player;

	// Use this for initialization
	void Start () {

		PunchAnim = gameObject.GetComponent<Animator>();
		Collider = gameObject.GetComponent<PolygonCollider2D>();
		Player = transform.parent.GetComponent<playerControls>();
		Player.OnPunch += OnPunch;
		
	}
	
	// Update is called once per frame
	void OnPunch () {
		PunchAnim.SetTrigger("PlayerPunches");
		//Collider.enabled = true;
	}

	void Update () {
		if (PunchAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch")) {
			Collider.enabled = true;
		}
		else if (PunchAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerArmsIdle")) {
			Collider.enabled = false;
		}
	}
}
