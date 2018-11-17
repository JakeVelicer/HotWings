using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour {

	public Sprite[] PepperImages;
	private Image PepperDisplayImage;
	private playerControls Player;

	// Use this for initialization
	void Start () {

		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControls>();
		PepperDisplayImage = this.gameObject.GetComponent<Image>();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.name == "SlotA") {
			PepperAArt();
		}
		if (gameObject.name == "SlotB") {
			PepperBArt();
		}
		
	}

	private void PepperAArt() {

		switch (Player.pepperIndexA) {
			case 0:
				PepperDisplayImage.sprite = PepperImages[0];
				break;
			case 1:
				PepperDisplayImage.sprite = PepperImages[1];
				break;
			case 2:
				PepperDisplayImage.sprite = PepperImages[2];
				break;
			case 3:
				PepperDisplayImage.sprite = PepperImages[3];
				break;
			case 4:
				PepperDisplayImage.sprite = PepperImages[4];
				break;
			case 5:
				PepperDisplayImage.sprite = PepperImages[5];
				break;
			case 6:
				PepperDisplayImage.sprite = PepperImages[6];
				break;
			case 7:
				PepperDisplayImage.sprite = PepperImages[7];
				break;
			case 8:
				PepperDisplayImage.sprite = PepperImages[8];
				break;
			case 9:
				PepperDisplayImage.sprite = PepperImages[9];
				break;
		}
	}

	private void PepperBArt() {

		switch (Player.pepperIndexB) {
			case 0:
				PepperDisplayImage.sprite = PepperImages[0];
				break;
			case 1:
				PepperDisplayImage.sprite = PepperImages[1];
				break;
			case 2:
				PepperDisplayImage.sprite = PepperImages[2];
				break;
			case 3:
				PepperDisplayImage.sprite = PepperImages[3];
				break;
			case 4:
				PepperDisplayImage.sprite = PepperImages[4];
				break;
			case 5:
				PepperDisplayImage.sprite = PepperImages[5];
				break;
			case 6:
				PepperDisplayImage.sprite = PepperImages[6];
				break;
			case 7:
				PepperDisplayImage.sprite = PepperImages[7];
				break;
			case 8:
				PepperDisplayImage.sprite = PepperImages[8];
				break;
			case 9:
				PepperDisplayImage.sprite = PepperImages[9];
				break;
		}
	}
}
