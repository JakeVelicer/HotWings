using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSwitchShownInput : MonoBehaviour {

	public GameObject creditsImage;
	public GameObject ourStoryImage;
	public GameObject[] buttons;
	private bool onMobile;

	// Use this for initialization
	void Start ()
	{
		creditsImage.SetActive(true);
		ourStoryImage.SetActive(false);

		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			onMobile = true;
		}
		else
		{
			onMobile = false;
			foreach (GameObject mobileButtons in buttons)
			{
				mobileButtons.SetActive(false);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!onMobile)
		{
			if(Input.GetAxisRaw("Horizontal") < 0)
			{
				ourStoryImage.SetActive(false);
				creditsImage.SetActive(true);
			}
			else if(Input.GetAxisRaw("Horizontal") > 0)
			{
				ourStoryImage.SetActive(true);
				creditsImage.SetActive(false);
			}
		}
	}

	public void ChooseStoryImage()
	{
		ourStoryImage.SetActive(true);
		creditsImage.SetActive(false);
	}

	public void ChooseCreditsImage()
	{
		ourStoryImage.SetActive(false);
		creditsImage.SetActive(true);
	}
}
