using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPepper : MonoBehaviour {

	private bool CanDestroy = false;

	// Use this for initialization
	void Start () {

		StartCoroutine(Activation());
		
	}

	void OnTriggerStay2D(Collider2D collision) {

		if (collision.gameObject.tag == "firePepper" || collision.gameObject.tag == "waterPepper" ||
		collision.gameObject.tag == "icePepper" || collision.gameObject.tag == "speedPepper" ||
		collision.gameObject.tag == "shockPepper" || collision.gameObject.tag == "windPepper" ||
		collision.gameObject.tag == "earthPepper" || collision.gameObject.tag == "healhPepper" ||
		collision.gameObject.tag == "buffPepper" || collision.gameObject.tag == "Player") {

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
