using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour {
	
	public float speed;
	public Rigidbody2D rigidBody;
	public float restartPositionX;
	private float chosenSpeed;

	// Update is called once per frame
	void FixedUpdate()
	{
		rigidBody.velocity = Vector2.left * speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("CloudRestart"))
		{
			//chosenSpeed = Random.Range(speeds.x, speeds.y);
			transform.localPosition = new Vector3 (restartPositionX, transform.position.y, transform.position.z);
		}
	}
}
