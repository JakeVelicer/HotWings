using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFloor : MonoBehaviour {

	private Collider2D PlayerCollider;
	private Collider2D Collider;
	[HideInInspector] public float virtualVerticalAxis;
	private float localAxis;
	private float mobileTriggerPoint = -0.7f;
	private float regTriggerPoint = -0.5f;
	private float triggerPoint;
	private bool onMobile;

	// Use this for initialization
	void Start () {

		PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
		Collider = gameObject.GetComponent<Collider2D>();
		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			onMobile = true;
			triggerPoint = mobileTriggerPoint;
		}
		else
		{
			onMobile = false;
			triggerPoint = regTriggerPoint;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (onMobile)
		{
			localAxis = virtualVerticalAxis;
		}
		else
		{
			localAxis = Input.GetAxis("Vertical");
		}

		if (localAxis < triggerPoint && Collider.IsTouching(PlayerCollider))
		{
			Collider.isTrigger = true;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Collider.isTrigger = false;
		}
	}
}
