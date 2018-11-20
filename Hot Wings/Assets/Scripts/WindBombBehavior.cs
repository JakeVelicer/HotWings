using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBombBehavior : MonoBehaviour {

	public GameObject EggWind;
	private Animator ExplodeAnim;
	public float Timer;
	private bool PlayAnim;
    private AudioSource bombSound;
    public AudioClip playerBomb;

	// Use this for initialization
	void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        bombSound.clip = playerBomb;
		ExplodeAnim = gameObject.GetComponent<Animator>();
		PlayAnim = true;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer -= Time.deltaTime;
		if (Timer <= 0 && PlayAnim == true) {
			PlayAnim = false;
			bombSound.Play();
			GameObject shot1 = Instantiate(EggWind, transform.position + new Vector3(1.5f, 0.5f, 0), 
				Quaternion.identity) as GameObject;
				shot1.GetComponent<WindBehavior>().GoRight = true;
	        	shot1.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 600);
            	shot1.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
			GameObject shot2 = Instantiate(EggWind, transform.position + new Vector3(-1.5f, 0.5f, 0), 
				Quaternion.identity) as GameObject;
				shot2.GetComponent<WindBehavior>().GoRight = false;
				shot2.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 600);
				shot2.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 120);
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, 2);
		}
	}
}