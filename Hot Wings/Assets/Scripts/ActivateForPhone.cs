using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateForPhone : MonoBehaviour {

	public Button InventoryButton;
	public GameObject GameplayButtons;
	
	// Use this for initialization
	void Start () {

		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			
			InventoryButton.enabled = true;
			GameplayButtons.SetActive(true);
		}
		else
		{
			InventoryButton.enabled = false;
			GameplayButtons.SetActive(false);
		}
	}
}
