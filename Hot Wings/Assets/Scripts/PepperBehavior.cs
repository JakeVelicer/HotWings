using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperBehavior : MonoBehaviour {

	public int PepperType;
	private PlayerControls Player;
	private GameController Controller;

	// Use this for initialization
	void Start () {

		Player = GameObject.Find("Player").GetComponent<PlayerControls>();
		this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		Controller = GameObject.Find("Controller").GetComponent<GameController>();
		GameController.OnWaveIncremented += DestroyPepper;
		
	}

	void OnTriggerEnter2D(Collider2D collision) {

        if (Player.pepperIndexA == 0 || Player.pepperIndexB == 0) {

            if (collision.gameObject.tag == "Player") {
				
                if (Player.pepperIndexA != PepperType && Player.pepperIndexB != PepperType) {
					if (PepperType == 7) {
						Player.HealthTimer = 5;
					}
					else if (PepperType == 5) {
						Player.DashCount = 7;
					}
					else if (PepperType == 2) {
						Player.ChargeTime = 0;
					}
                    Player.PepperCollision(PepperType);
					GameController.OnWaveIncremented -= DestroyPepper;
                    Destroy(gameObject);
                }
            }
		}
		
	}

	private void DestroyPepper(int waveCount) {

		GameController.OnWaveIncremented -= DestroyPepper;
		Destroy(gameObject);

	}

}
