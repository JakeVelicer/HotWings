using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFloor : MonoBehaviour {

	private Collider2D PlayerCollider;
	private Collider2D Collider;
	[HideInInspector] public float virtualVerticalAxis;
	private float localAxis;

	// Use this for initialization
	void Start () {

		PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
		Collider = gameObject.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			localAxis = virtualVerticalAxis;
		}
		else
		{
			localAxis = Input.GetAxis("Vertical");
		}

		if (localAxis < -0.5f && Collider.IsTouching(PlayerCollider))
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
