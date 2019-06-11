using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour {

	private bool Touching;
	private Vector2 PointA;
	private Vector2 PointB;
	private Touch firstTouch;
	private GameObject joystickOutline;
	private GameObject joystickFinger;
	private Vector3 outlineImageStart;
	private Vector3 fingerImageStart;

	// Use this for initialization
	void Start () {

		joystickOutline = GameObject.Find("JoystickOutline");
		joystickFinger = GameObject.Find("JoystickFinger");
		outlineImageStart = joystickOutline.transform.position;
		fingerImageStart = joystickFinger.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {

			firstTouch = Input.GetTouch(0);
			
			// Handle finger movements based on TouchPhase
			switch (firstTouch.phase)
			{
				// When a touch has first been detected, change the message and record the starting position
				case TouchPhase.Began:

					// Record initial touch position.
					joystickOutline.transform.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
					PointA = Camera.main.ScreenToWorldPoint(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
					break;

				//Determine if the touch is a moving touch
				case TouchPhase.Moved:

					// Determine direction by comparing the current touch position with the initial one
					Touching = true;
					joystickFinger.transform.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
					PointA = Camera.main.ScreenToWorldPoint(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
					break;

				case TouchPhase.Ended:

					// Report that the touch has ended when it ends
					Touching = false;
					joystickOutline.transform.position = outlineImageStart;
					joystickFinger.transform.position = fingerImageStart;
					break;
			}
		}
	}

	private void FixedUpdate() {
		
		if (Touching) {
			
			Vector2 offset = PointB - PointA;
			Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
			direction *= -1;
		}

	}
}
