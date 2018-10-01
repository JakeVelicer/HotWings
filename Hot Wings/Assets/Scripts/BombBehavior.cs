using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	public float Timer;
	private bool PlayAnim;

	// Use this for initialization
	void Start () {

		ExplodeAnim = gameObject.GetComponent<Animator>();
		PlayAnim = true;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer -= Time.deltaTime;
		if (Timer <= 0 && PlayAnim == true) {
			ExplodeAnim.SetTrigger("Boom");
			PlayAnim = false;
			Destroy(transform.parent.gameObject, 1.1f);
		}
		
	}
}
