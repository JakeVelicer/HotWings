using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	private Collider2D Collider;
    private AudioSource bombSound;
    public AudioClip enemyBomb;
	public bool Exploding;
	private bool Called;

	// Use this for initialization
	void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        bombSound.clip = enemyBomb;
		ExplodeAnim = gameObject.GetComponent<Animator>();
		Collider = gameObject.GetComponent<Collider2D>();
		StartCoroutine(Regardless());
		
	}

	void Update() {
		
		if (Exploding == true) {
			StartCoroutine(Countdown());
		}
	}

	private IEnumerator Regardless() {
		
		yield return new WaitForSeconds(5);
		Exploding = true;
	}

	public IEnumerator Countdown () {
		if (!Called) {
			Called = true;
			Exploding = false;
			yield return new WaitForSeconds(0.1f);
			ExplodeAnim.SetTrigger("Boom");
			transform.parent.GetComponent<SpriteRenderer>().enabled = false;
			Collider.enabled = true;
			GameObject.Find("Controller").GetComponent<ScreenShake>().BombGoesOff(0.2f);
			bombSound.Play();
			yield return new WaitForSeconds(0.5f);
			Collider.enabled = false;
			Destroy(transform.parent.gameObject, 1);
		}
	}
}
