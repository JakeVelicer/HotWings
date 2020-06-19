using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeSpriteButtonDown : MonoBehaviour
{
	public Sprite unselectedBox;
	public Sprite selectedBox;
	public Image inventoryBox2;
	private bool onMobile;

	private void Awake()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			onMobile = true;
		}
		else
		{
			onMobile = false;
		}
	}

	public void SelectSprite()
	{
		if (onMobile)
		inventoryBox2.sprite = selectedBox;
	}

	public void UnSelectSprite()
	{
		if (onMobile)
		inventoryBox2.sprite = unselectedBox;
	}

}
