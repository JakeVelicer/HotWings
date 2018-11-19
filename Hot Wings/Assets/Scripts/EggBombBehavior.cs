using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	public float Timer;
	private bool PlayAnim;
	private Collider2D Collider;
    private AudioSource bombSound;
    public AudioClip playerBomb;

	// Use this for initialization
	void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        bombSound.clip = playerBomb;
		ExplodeAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
		PlayAnim = true;
		Collider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
		//gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Player";
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer -= Time.deltaTime;
		if (Timer <= 0 && PlayAnim == true) {
			PlayAnim = false;
			ExplodeAnim.SetTrigger("Boom");
			Collider.enabled = true;
            bombSound.Play();
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, 0.6f);
		}
	}
}
