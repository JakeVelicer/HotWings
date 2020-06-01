using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShownInput : MonoBehaviour {

	public GameObject controlsPC;
	public GameObject controlsController;
	private bool onMobile;

	// Use this for initialization
	void Start () {

		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			onMobile = true;
			controlsController.SetActive(false);
			controlsPC.SetActive(false);
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
}
