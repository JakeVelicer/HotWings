using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBehavior : MonoBehaviour {

	private Animator PunchAnim;
	BasicEnemyControls EnemyControls;

	// Use this for initialization
	void Start () {

		PunchAnim = gameObject.GetComponent<Animator>();
		EnemyControls = transform.parent.GetComponent<BasicEnemyControls>();
		EnemyControls.OnPunch += OnPunch;
		
	}
	
	// Update is called once per frame
	void OnPunch () {
		PunchAnim.SetTrigger("GoPunch");
	}
}
