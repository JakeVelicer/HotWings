using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperBehavior : MonoBehaviour {

	private bool CanDestroy = false;
	private playerControls Player;

	// Use this for initialization
	void Start () {

		Player = GameObject.FindWithTag("Player").GetComponent<playerControls>();
		StartCoroutine(Activation());
		this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		
	}

	void OnTriggerStay2D(Collider2D collision) {

		if (collision.gameObject.tag == "firePepper" || collision.gameObject.tag == "waterPepper" ||
		collision.gameObject.tag == "icePepper" || collision.gameObject.tag == "speedPepper" ||
		collision.gameObject.tag == "shockPepper" || collision.gameObject.tag == "windPepper" ||
		collision.gameObject.tag == "earthPepper" || collision.gameObject.tag == "healhPepper" ||
		collision.gameObject.tag == "buffPepper") {

			if (CanDestroy == true) {
				Destroy(gameObject);
			}
		}
		
	}

	private IEnumerator Activation () {
		yield return new WaitForSeconds(1);
		CanDestroy = true;
	}

}
