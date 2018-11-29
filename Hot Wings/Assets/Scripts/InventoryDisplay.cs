using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour {

	public Sprite[] PepperImages;
	private Image PepperDisplayImage;
	public Image CountdownImage;
	private playerControls Player;
	private int MaxHealthCounter;
	private int MaxBuffCounter;
	private float BarLength;
	private float MaxBarLength;

	// Use this for initialization
	void Start () {

		if (gameObject.name == "SlotA") {
			BarLength = 102;
			MaxBarLength = 102;
		}
		if (gameObject.name == "SlotB") {
			BarLength = 80;
			MaxBarLength = 80;
		}

		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControls>();
		PepperDisplayImage = this.gameObject.GetComponent<Image>();
		MaxHealthCounter = 5;
		MaxBuffCounter = 20;
		
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

		var theBarRectTransform = CountdownImage.transform as RectTransform;

		switch (Player.pepperIndexA) {
			case 0:
				PepperDisplayImage.sprite = PepperImages[0];
				CountdownImage.enabled = false;
				break;
			case 1:
				PepperDisplayImage.sprite = PepperImages[1];
				CountdownImage.enabled = false;
				break;
			case 2:
				PepperDisplayImage.sprite = PepperImages[2];
				CountdownImage.enabled = false;
				break;
			case 3:
				PepperDisplayImage.sprite = PepperImages[3];
				CountdownImage.enabled = false;
				break;
			case 4:
				PepperDisplayImage.sprite = PepperImages[4];
				CountdownImage.enabled = false;
				break;
			case 5:
				PepperDisplayImage.sprite = PepperImages[5];
				CountdownImage.enabled = false;
				break;
			case 6:
				PepperDisplayImage.sprite = PepperImages[6];
				CountdownImage.enabled = false;
				break;
			case 7:
				PepperDisplayImage.sprite = PepperImages[7];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,96,214,200);

				BarLength = MaxBarLength * Player.HealthTimer / MaxHealthCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				// Got from Ethan Ham
				break;
			case 8:
				PepperDisplayImage.sprite = PepperImages[8];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,0,20,200);

				BarLength = MaxBarLength * Player.BuffTimer / MaxBuffCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				// Got from Ethan Ham
				break;
			case 9:
				PepperDisplayImage.sprite = PepperImages[9];
				CountdownImage.enabled = false;
				break;
		}
	}

	private void PepperBArt() {

		var theBarRectTransform = CountdownImage.transform as RectTransform;

		switch (Player.pepperIndexB) {
			case 0:
				PepperDisplayImage.sprite = PepperImages[0];
				CountdownImage.enabled = false;
				break;
			case 1:
				PepperDisplayImage.sprite = PepperImages[1];
				CountdownImage.enabled = false;
				break;
			case 2:
				PepperDisplayImage.sprite = PepperImages[2];
				CountdownImage.enabled = false;
				break;
			case 3:
				PepperDisplayImage.sprite = PepperImages[3];
				CountdownImage.enabled = false;
				break;
			case 4:
				PepperDisplayImage.sprite = PepperImages[4];
				CountdownImage.enabled = false;
				break;
			case 5:
				PepperDisplayImage.sprite = PepperImages[5];
				CountdownImage.enabled = false;
				break;
			case 6:
				PepperDisplayImage.sprite = PepperImages[6];
				CountdownImage.enabled = false;
				break;
			case 7:
				PepperDisplayImage.sprite = PepperImages[7];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,96,214,200);

				BarLength = MaxBarLength * Player.HealthTimer / MaxHealthCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				// Got from Ethan Ham
				break;
			case 8:
				PepperDisplayImage.sprite = PepperImages[8];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,0,20,200);

				BarLength = MaxBarLength * Player.BuffTimer / MaxBuffCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				// Got from Ethan Ham
				break;
			case 9:
				PepperDisplayImage.sprite = PepperImages[9];
				CountdownImage.enabled = false;
				break;
		}
	}
}
