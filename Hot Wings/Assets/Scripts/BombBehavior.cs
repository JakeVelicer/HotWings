using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	public float Timer;
	private bool PlayAnim;
	private Collider2D Collider;
    private AudioSource bombSound;
    public AudioClip enemyBomb;

	// Use this for initialization
	void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        bombSound.clip = enemyBomb;
		ExplodeAnim = gameObject.GetComponent<Animator>();
		PlayAnim = true;
		Collider = gameObject.GetComponent<Collider2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer -= Time.deltaTime;
		if (Timer <= 0 && PlayAnim == true) {
			ExplodeAnim.SetTrigger("Boom");
			Collider.enabled = true;
			PlayAnim = false;
            bombSound.PlayDelayed(1.0f);
			Destroy(transform.parent.gameObject, 2.0f);
		}
	}
}
