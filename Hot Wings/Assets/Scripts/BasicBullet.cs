using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour {

	private Rigidbody2D Rb;
	private BasicEnemyControls BasicEnemy;
	public float Speed;
	private bool ShootRight;

	void Awake () {

		BasicEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BasicEnemyControls>();

	}

	// Use this for initialization
	void Start () {

		Rb = GetComponent<Rigidbody2D>();

		if (BasicEnemy.ToTheRight == true) {
			ShootRight = true;
		}
		else if (BasicEnemy.ToTheRight == false) {
			ShootRight = false;
		}

	}
	
	// Update is called once per frame
	void Update () {

		//Rb.AddForce (Vector3.right * Speed);
		if (ShootRight == true) {
		transform.Translate (Vector3.right * Time.deltaTime * Speed);
		}
		else if (ShootRight == false) {
		transform.Translate (Vector3.left * Time.deltaTime * Speed);
		}
		
	}
}
