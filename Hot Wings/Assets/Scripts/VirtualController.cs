using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualController : MonoBehaviour {

	private PlayerControls playerScript;
	public GameObject joystickOutline;
	public GameObject joystickFinger;
	public Sprite fingerActiveImage;
	public Sprite fingerInactiveImage;
	public Image fingerImageRenderer;
	private Touch firstJoystickTouch;
	private Vector2 PointA;
	private Vector2 PointB;
	private Vector2 ClampedPointB;
	private Vector3 fingerImageStart;
	private bool touching;
	private float Horizontal;
	private float Vertical;
	private float distance;
	private bool withinRange;
	public int JoystickFillSpace;
	public float JoystickDeadSpace;
	public float limitTouchSideScreen = 800;
	public float radius = 400;
	public DeactivateFloor[] deactivePlatformScripts;
	private int joystickTouchNumber;

	// Use this for initialization
	void Start ()
	{
		playerScript = GameObject.Find("Player").GetComponent<PlayerControls>();
		fingerImageStart = joystickFinger.transform.position;
		PointA = joystickOutline.transform.position;
		fingerImageRenderer.sprite = fingerInactiveImage;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.touchCount > 0)
		{
			// Only assigns a touch if the touch is on the joystick side
			for (int i = 0; i < Input.touchCount; i++)
			{
				if (Input.GetTouch(i).position.x < limitTouchSideScreen)
				{
					joystickTouchNumber = i;
				}
			}

			if (joystickTouchNumber < Input.touchCount)
			{
				firstJoystickTouch = Input.GetTouch(joystickTouchNumber);
			}

			// Handle finger movements based on TouchPhase
			switch (firstJoystickTouch.phase)
			{
				// When a touch has first been detected, change the message and record the starting position
				case TouchPhase.Began:

					// Record initial touch position.
					PointB = new Vector2(firstJoystickTouch.position.x, firstJoystickTouch.position.y);
					if (withinRange)
					{
						touching = true;
						fingerImageRenderer.sprite = fingerActiveImage;
					}
					break;

				//Determine if the touch is a moving touch
				case TouchPhase.Moved:

					// Determine direction by comparing the current touch position with the initial one
					PointB = new Vector2(firstJoystickTouch.position.x, firstJoystickTouch.position.y);
					if (withinRange)
					{
						touching = true;
						fingerImageRenderer.sprite = fingerActiveImage;
					}
					break;

				case TouchPhase.Ended:

					// Report that the touch has ended when it ends
					touching = false;
					withinRange = false;
					joystickFinger.transform.position = fingerImageStart;
					fingerImageRenderer.sprite = fingerInactiveImage;
					joystickTouchNumber = 20;
					break;

				case TouchPhase.Canceled:

					// Report that the touch has canceled
					touching = false;
					withinRange = false;
					joystickFinger.transform.position = fingerImageStart;
					fingerImageRenderer.sprite = fingerInactiveImage;
					joystickTouchNumber = 20;
					break;

			}
		}
		else
		{
			touching = false;
			withinRange = false;
			joystickFinger.transform.position = fingerImageStart;
			fingerImageRenderer.sprite = fingerInactiveImage;
		}

		if (PointB.x < limitTouchSideScreen)
		{
			withinRange = true;
		}
		else
		{
			withinRange = false;
		}

		// Controls placement of finger joystick image, clamps it's radius
		distance = Vector3.Distance(PointB, PointA);
		if (distance > radius)
		{
			Vector2 fromOriginToObject = PointB - PointA;
			fromOriginToObject *= radius / distance;
			ClampedPointB = PointA + fromOriginToObject;
		}
		else
		{
			ClampedPointB = PointB;
		}
		
		if (touching && withinRange)
		{
			joystickFinger.transform.position = ClampedPointB;
		}
	}

	private void FixedUpdate()
	{	
		if (touching && withinRange)
		{
			Vector2 offset = PointB - PointA;
			offset /= JoystickFillSpace;
			Horizontal = Mathf.Clamp(offset.x, -1, 1);
			Vertical = Mathf.Clamp(offset.y, -1, 1);

			if(Horizontal < JoystickDeadSpace && Horizontal > -JoystickDeadSpace)
			{
				playerScript.virtualHorizontalAxis = 0;
			}
			else
			{
				playerScript.virtualHorizontalAxis = Horizontal;
			}

			foreach (DeactivateFloor platform in deactivePlatformScripts)
			{
				platform.virtualVerticalAxis = Vertical;
			}
		}
		else
		{
			playerScript.virtualHorizontalAxis = 0;
			foreach (DeactivateFloor platform in deactivePlatformScripts)
			{
				platform.virtualVerticalAxis = 0;
			}
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
