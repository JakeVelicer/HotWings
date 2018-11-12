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
    public AudioClip playerBomb;

	// Use this for initialization
	void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        if (gameObject.tag == "enemyExplosion") {
            bombSound.clip = enemyBomb;
        }
        else {
            bombSound.clip = playerBomb;
        }
		ExplodeAnim = gameObject.GetComponent<Animator>();
		PlayAnim = true;
		Collider = gameObject.GetComponent<Collider2D>();
		//gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Player";
		
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
