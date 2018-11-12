using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

	public Animator Anim;

	// Use this for initialization
	void Start () {

		if (gameObject.name == "LightningBullet(Clone)") {
			Anim.Play("Shock1");
		}
		else if (gameObject.name == "LightningBullet2(Clone)") {
			Anim.Play("Shock2");
		}
		else if (gameObject.name == "LightningBullet3(Clone)") {
			Anim.Play("Shock3");
		}
		else if (gameObject.name == "LightningBullet4(Clone)") {
			Anim.Play("Shock4");
		}
		Destroy (gameObject, 1.3f);
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}
		if (gameObject.tag == "Ice" && collision.gameObject.tag == "IceBlock") {
			Destroy (gameObject);
		}
	}
}
