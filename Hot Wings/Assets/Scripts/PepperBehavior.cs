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

		if (collision.gameObject.name == "FirePepper(Clone)" || collision.gameObject.name == "WaterPepper(Clone)" ||
			collision.gameObject.name == "IcePepper(Clone)" || collision.gameObject.name == "SpeedPepper(Clone)" ||
			collision.gameObject.name == "ShockPepper(Clone)" || collision.gameObject.name == "WindPepper(Clone)" ||
			collision.gameObject.name == "EarthPepper(Clone)" || collision.gameObject.name == "HealhPepper(Clone)" ||
			collision.gameObject.name == "BuffPepper(Clone)") {

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
