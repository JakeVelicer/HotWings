using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggDisplay : MonoBehaviour {

	public Sprite[] EggImages;
	private Image EggDisplayImageRenderer;
	private PlayerControls Player;
	private RectTransform rectTransform;
	public Vector2 defaultEggSize;
	public Vector2 specialEggSize;

	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find("Player").GetComponent<PlayerControls>();
		EggDisplayImageRenderer = this.gameObject.GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Player.pepperIndexA == 5 || Player.pepperIndexA >= 7)
		{
			rectTransform.sizeDelta = specialEggSize;
		}
		else
		{
			rectTransform.sizeDelta = defaultEggSize;
		}
		
		switch (Player.pepperIndexA)
		{
			case 0:
				EggDisplayImageRenderer.sprite = EggImages[0];
				break;
			case 1:
				EggDisplayImageRenderer.sprite = EggImages[1];
				break;
			case 2:
				EggDisplayImageRenderer.sprite = EggImages[2];
				break;
			case 3:
				EggDisplayImageRenderer.sprite = EggImages[3];
				break;
			case 4:
				EggDisplayImageRenderer.sprite = EggImages[4];
				break;
			case 5:
				EggDisplayImageRenderer.sprite = EggImages[5];
				break;
			case 6:
				EggDisplayImageRenderer.sprite = EggImages[6];
				break;
			case 7:
				EggDisplayImageRenderer.sprite = EggImages[7];
				break;
			case 8:
				EggDisplayImageRenderer.sprite = EggImages[8];
				break;
			case 9:
				EggDisplayImageRenderer.sprite = EggImages[9];
				break;
		}
	}
}
