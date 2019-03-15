using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperBehavior : MonoBehaviour {

	public int PepperType;
	private bool CanDestroy = false;
	private playerControls Player;

	// Use this for initialization
	void Start () {

		Player = GameObject.Find("Player").GetComponent<playerControls>();
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
        if (Player.pepperIndexA == 0 || Player.pepperIndexB == 0) {

            if (collision.gameObject.tag == "Player") {
                if (Player.pepperIndexA != PepperType && Player.pepperIndexB != PepperType) {
					if (PepperType == 7) {
						Player.HealthTimer = 5;
					}
					else if (PepperType == 8) {
						Player.anim.SetBool("isBuff", true);
						Player.BuffTimer = 20;
					}
					else if (PepperType == 2) {
						Player.ChargeTime = 0;
					}
                    Player.PepperCollision(PepperType);
                    Destroy(gameObject);
                }
            }
		}
		
	}

	private IEnumerator Activation () {
		yield return new WaitForSeconds(1);
		CanDestroy = true;
	}

}
