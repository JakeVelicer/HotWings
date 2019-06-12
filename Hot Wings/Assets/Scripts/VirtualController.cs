using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualController : MonoBehaviour {

	private playerControls playerScript;
	private GameObject joystickOutline;
	private GameObject joystickFinger;
	private Touch firstTouch;
	private Vector2 PointA;
	private Vector2 PointB;
	private Vector3 outlineImageStart;
	private Vector3 fingerImageStart;
	private bool Touching;
	private float Direction;
	public int joystickDeadspace;

	// Use this for initialization
	void Start () {

		joystickOutline = GameObject.Find("JoystickOutline");
		joystickFinger = GameObject.Find("JoystickFinger");
		outlineImageStart = joystickOutline.transform.position;
		fingerImageStart = joystickFinger.transform.position;
		playerScript = GameObject.Find("Player").GetComponent<playerControls>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {

			firstTouch = Input.GetTouch(0);
			//Debug.Log("Hor: " + Direction);
			
			// Handle finger movements based on TouchPhase
			switch (firstTouch.phase) {

				// When a touch has first been detected, change the message and record the starting position
				case TouchPhase.Began:

					// Record initial touch position.
					PointA = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
					joystickOutline.transform.position = PointA;
					break;

				//Determine if the touch is a moving touch
				case TouchPhase.Moved:

					// Determine direction by comparing the current touch position with the initial one
					Touching = true;
					PointB = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
					joystickFinger.transform.position = PointB;
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
			offset /= joystickDeadspace;
			Direction = Mathf.Clamp(offset.x, -1, 1);
			playerScript.virtualHorizontalAxis = Direction;
		}
		else {
			
			playerScript.virtualHorizontalAxis = 0;
		}

	}
}
