using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour {
	
	public float speed;
	public float waitToStart;
	public Rigidbody2D rigidBody;
	private Vector3 startPosition;
	private float chosenSpeed;
	private bool canMove;

	void Start()
	{
		//chosenSpeed = Random.Range(speeds.x, speeds.y);
		startPosition = transform.position;
		StartCoroutine(WaitToMove());

	}

	private IEnumerator WaitToMove()
	{
		yield return new WaitForSeconds(waitToStart);
		canMove = true;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (canMove)
		{
			rigidBody.velocity = Vector2.left * speed;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("colliding");
		if (other.gameObject.CompareTag("CloudRestart"))
		{
			//chosenSpeed = Random.Range(speeds.x, speeds.y);
			transform.position = startPosition;
		}
	}
}
