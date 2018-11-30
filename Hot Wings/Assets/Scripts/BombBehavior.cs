using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	public int Timer;
	private Collider2D Collider;
    private AudioSource bombSound;
    public AudioClip enemyBomb;

	// Use this for initialization
	void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        bombSound.clip = enemyBomb;
		ExplodeAnim = gameObject.GetComponent<Animator>();
		Collider = gameObject.GetComponent<Collider2D>();
		StartCoroutine(Countdown());
		
	}
	
	// Update is called once per frame
	IEnumerator Countdown () {
		yield return new WaitForSeconds(Timer);
		ExplodeAnim.SetTrigger("Boom");
		yield return new WaitForSeconds(1);
		Collider.enabled = true;
		GameObject.Find("Controller").GetComponent<ScreenShake>().BombGoesOff(0.2f);
		bombSound.Play();
		transform.parent.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(0.5f);
		Collider.enabled = false;
		Destroy(transform.parent.gameObject, 1);
	}
}
