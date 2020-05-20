using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmBehavior : MonoBehaviour {

	private Animator PunchAnim;
	public Collider2D Collider;
	private PlayerControls Player;

	// Use this for initialization
	void Start ()
	{
		PunchAnim = gameObject.GetComponent<Animator>();
		Player = transform.parent.GetComponent<PlayerControls>();
	}
	
	public void OnPunch ()
	{
		PunchAnim.Play("PlayerPunch");
		Debug.Log("Punch");
	}

	void Update ()
	{
		if (PunchAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch"))
		{
			Collider.enabled = true;
		}
		else if (PunchAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerArmsIdle"))
		{
			Collider.enabled = false;
		}
	}
}
