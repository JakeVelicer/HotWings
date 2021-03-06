﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class InventoryDisplay : MonoBehaviour {

	public Sprite[] PepperImages;
	public Vector2 defaultPepperSize;
	public Vector2 buffPepperSize;
	private Image PepperDisplayImage;
	public Image CountdownImage;
	public Text ShockChargeText;
	private PlayerControls Player;
	private readonly int MaxHealthCounter = 5;
	private int MaxBuffCounter = 20;
	private readonly int MaxDashCounter = 7;
	private readonly float MaxFireCapacity = 4;
	private readonly float MaxWaterCapacity = 4;
	private readonly float MaxShockCapacity = 3;
	private float BarLength;
	private float MaxBarLength;
	private RectTransform rectTransform;

	// Use this for initialization
	void Start ()
	{
		BarLength = CountdownImage.rectTransform.sizeDelta.y;
		MaxBarLength = CountdownImage.rectTransform.sizeDelta.y;
		Player = GameObject.Find("Player").GetComponent<PlayerControls>();
		PepperDisplayImage = this.gameObject.GetComponent<Image>();
		MaxBuffCounter = Player.MaxBuffTime;
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.name == "SlotA")
		{
			PepperAArt();
			if (Player.pepperIndexA == 8)
			{
				rectTransform.sizeDelta = buffPepperSize;
			}
			else
			{
				rectTransform.sizeDelta = defaultPepperSize;
			}
		}
		if (gameObject.name == "SlotB")
		{
			PepperBArt();
			if (Player.pepperIndexB == 8)
			{
				rectTransform.sizeDelta = buffPepperSize;
			}
			else
			{
				rectTransform.sizeDelta = defaultPepperSize;
			}
		}
	}

	private void PepperAArt() {

		var theBarRectTransform = CountdownImage.transform as RectTransform;

		switch (Player.pepperIndexA) {
			case 0:
				PepperDisplayImage.sprite = PepperImages[0];
				CountdownImage.enabled = false;
				ShockChargeText.enabled = false;
				break;
			case 1:
				PepperDisplayImage.sprite = PepperImages[1];
				CountdownImage.enabled = true;
				ShockChargeText.enabled = false;
				CountdownImage.color = new Color32(255,78,0,200);

				BarLength = MaxBarLength * Player.FireCoolDown / MaxFireCapacity;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 2:
				PepperDisplayImage.sprite = PepperImages[2];
				CountdownImage.enabled = true;
				ShockChargeText.enabled = true;
				CountdownImage.color = new Color32(255,246,0,200);

				if (Player.ChargeTime > 0) {
					ShockChargeText.text = (Math.Truncate((decimal)(Player.ChargeTime + 1))).ToString();
				}
				else {
					ShockChargeText.text = "";
				}
				BarLength = MaxBarLength * Player.ChargeTime / MaxShockCapacity;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 3:
				PepperDisplayImage.sprite = PepperImages[3];
				CountdownImage.enabled = true;
				ShockChargeText.enabled = false;
				CountdownImage.color = new Color32(0,114,255,200);

				BarLength = MaxBarLength * Player.WaterCoolDown / MaxWaterCapacity;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 4:
				PepperDisplayImage.sprite = PepperImages[4];
				CountdownImage.enabled = false;
				ShockChargeText.enabled = false;
				break;
			case 5:
				PepperDisplayImage.sprite = PepperImages[5];
				CountdownImage.enabled = true;
				ShockChargeText.enabled = false;
				CountdownImage.color = new Color32(152,42,143,200);

				BarLength = MaxBarLength * Player.DashCount / MaxDashCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 6:
				PepperDisplayImage.sprite = PepperImages[6];
				CountdownImage.enabled = false;
				ShockChargeText.enabled = false;
				break;
			case 7:
				PepperDisplayImage.sprite = PepperImages[7];
				CountdownImage.enabled = true;
				ShockChargeText.enabled = false;
				CountdownImage.color = new Color32(255,96,214,200);

				BarLength = MaxBarLength * Player.HealthTimer / MaxHealthCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 8:
				PepperDisplayImage.sprite = PepperImages[8];
				CountdownImage.enabled = true;
				ShockChargeText.enabled = false;
				CountdownImage.color = new Color32(255,0,20,200);

				BarLength = MaxBarLength * Player.BuffTimer / MaxBuffCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				// Got from Ethan Ham
				break;
			case 9:
				PepperDisplayImage.sprite = PepperImages[9];
				CountdownImage.enabled = false;
				ShockChargeText.enabled = false;
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
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,78,0,200);

				BarLength = MaxBarLength * Player.FireCoolDown / MaxFireCapacity;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 2:
				PepperDisplayImage.sprite = PepperImages[2];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,246,0,200);

				BarLength = MaxBarLength * Player.ChargeTime / MaxShockCapacity;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 3:
				PepperDisplayImage.sprite = PepperImages[3];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(0,114,255,200);

				BarLength = MaxBarLength * Player.WaterCoolDown / MaxWaterCapacity;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 4:
				PepperDisplayImage.sprite = PepperImages[4];
				CountdownImage.enabled = false;
				break;
			case 5:
				PepperDisplayImage.sprite = PepperImages[5];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(152,42,143,200);

				BarLength = MaxBarLength * Player.DashCount / MaxDashCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
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
				break;
			case 8:
				PepperDisplayImage.sprite = PepperImages[8];
				CountdownImage.enabled = true;
				CountdownImage.color = new Color32(255,0,20,200);

				BarLength = MaxBarLength * Player.BuffTimer / MaxBuffCounter;
				theBarRectTransform.sizeDelta = new Vector2 (theBarRectTransform.sizeDelta.x, BarLength);
				break;
			case 9:
				PepperDisplayImage.sprite = PepperImages[9];
				CountdownImage.enabled = false;
				break;
		}
	}
}
