using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBlobBombExplosion : MonoBehaviour {

	private BombBehavior BombScript;

	// Use this for initialization
	void Start () {

		BombScript = gameObject.transform.GetChild(0).GetComponent<BombBehavior>();
		
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		
		if (!BombScript.Exploding) {
			if (collision.gameObject.name == "Player") {
				BombScript.Exploding = true;
			}
			if (collision.gameObject.tag == "Ground") {
				BombScript.Exploding = true;
			}
		}
	}
}
