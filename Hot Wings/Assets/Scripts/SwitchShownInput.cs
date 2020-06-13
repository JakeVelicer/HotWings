using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchShownInput : MonoBehaviour {

	public GameObject controlsPC;
	public GameObject controlsController;
	public GameObject controlsMobile;
	public GameObject helpScreenMobile;
	public Button backBtn;
	private bool onMobile;

	// Use this for initialization
	void Start () {

		if (Application.platform != RuntimePlatform.IPhonePlayer
		&& Application.platform != RuntimePlatform.Android)
		{
			onMobile = false;
			controlsController.SetActive(false);
			controlsPC.SetActive(true);
			controlsMobile.SetActive(false);
			helpScreenMobile.SetActive(false);
			backBtn.Select();
		}
		else
		{
			onMobile = true;
			controlsController.SetActive(false);
			controlsPC.SetActive(false);
			controlsMobile.SetActive(true);
			helpScreenMobile.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!onMobile)
		{
			if(Input.GetAxisRaw("Horizontal") < 0)
			{
				controlsController.SetActive(true);
				controlsPC.SetActive(false);
			}
			else if(Input.GetAxisRaw("Horizontal") > 0)
			{
				controlsPC.SetActive(true);
				controlsController.SetActive(false);
			}
		}
	}

	public void ChooseMobileControlImage()
	{
		controlsMobile.SetActive(true);
		helpScreenMobile.SetActive(false);
	}

	public void ChooseMobileHelpImage()
	{
		controlsMobile.SetActive(false);
		helpScreenMobile.SetActive(true);
	}
}
