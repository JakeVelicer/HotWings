using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualController : MonoBehaviour {

	private PlayerControls playerScript;
	public GameObject joystickOutline;
	public GameObject joystickFinger;
	private Touch firstTouch;
	private Vector2 PointA;
	private Vector2 PointB;
	private Vector3 fingerImageStart;
	private bool Touching;
	private float Direction;
	public int joystickDeadspace;

	// Use this for initialization
	void Start ()
	{
		playerScript = GameObject.Find("Player").GetComponent<PlayerControls>();
		fingerImageStart = joystickFinger.transform.position;
		PointA = joystickOutline.transform.position;
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
					PointB = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
					joystickFinger.transform.position = PointB;
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
					joystickFinger.transform.position = fingerImageStart;
					break;
			}
		}
	}

	private void FixedUpdate()
	{	
		if (Touching)
		{
			Vector2 offset = PointB - PointA;
			offset /= joystickDeadspace;
			Direction = Mathf.Clamp(offset.x, -1, 1);
			playerScript.virtualHorizontalAxis = Direction;
		}
		else
		{
			playerScript.virtualHorizontalAxis = 0;
		}

	}

	public void AttackOnPress ()
	{
		playerScript.virtualAttackAxis = 1;
	}

	public void AttackOnRelease ()
	{
		playerScript.virtualAttackAxis = 0;
	}

}
