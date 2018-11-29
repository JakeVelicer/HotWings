using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBehavior : MonoBehaviour {

	private Animator PunchAnim;
	BasicEnemyControls EnemyControls;
    private AudioSource punchSound;
    public AudioClip punch;

	// Use this for initialization
	void Start () {

        punchSound = GetComponentInParent<AudioSource>();
        punchSound.clip = punch;
		PunchAnim = gameObject.GetComponent<Animator>();
		EnemyControls = transform.parent.GetComponent<BasicEnemyControls>();
		EnemyControls.OnPunch += OnPunch;
		
	}
	
	// Update is called once per frame
	void OnPunch () { 

        punchSound.Play();
		PunchAnim.SetTrigger("GoPunch");
	}
}
