using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonPositioner : MonoBehaviour {

	public float pcYPos;
	public float mobileYPos;
	public RectTransform buttonHolder;

	// Use this for initialization
	void Awake ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			buttonHolder.localPosition = new Vector3(0, mobileYPos, 1);
		}
		else
		{
			buttonHolder.localPosition = new Vector3(0, pcYPos, 1);
		}
	}
	
}
