using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControls : MonoBehaviour {

	private Rigidbody2D Rigidbody;
	private Transform Target;
	public GameController MainController;

	public float EnemyHealth;
	public float MovementSpeed;
	public float ChaseRange;
	public float FireRange;
	public float ProjectileSpeed;
	public float ProjectileHeight;
	public float CoolDown;
	private float CoolDownTimer = 0;

	private bool CanChase;
	[HideInInspector] public bool Punch;
	public bool ToTheRight;
	public GameObject BulletObject;
	public GameObject BombObject;
	public int AlientType;

	// Use this for initialization
	void Start () {

		//Rigidbody = GetComponent<Rigidbody2D> ();
		MainController = GameObject.Find ("Controller").GetComponent<GameController> ();
		MainController.EnemiesLeft++;
		
	}
	
	// Update is called once per frame
	void Update () {

		// Finds the Player's transform and stores it in target
		Target = GameObject.FindGameObjectWithTag ("Player").transform;

		Movement();
		ChaseTarget();

		if (EnemyHealth <= 0) {
			MainController.EnemiesLeft--;
			Destroy(gameObject);
		}
		
	}

	// Controls the actual movement of the object
	void Movement () {

		// Checks if it is allowed to chase the player
		if (CanChase == true) {

			// Pushes the enemy in a direction based upon which side the player is on
			if (ToTheRight == false) {
			transform.Translate (Vector3.left * Time.deltaTime * MovementSpeed);
			}
			else if (ToTheRight == true) {
			transform.Translate (Vector3.right * Time.deltaTime * MovementSpeed);
			}
			//Rigidbody.AddForce (MovementSpeed * new Vector2 (1,0));
		}
	}

	void ChaseTarget () {

		// Determines if the range of the player is close enough to be chased
		if (Vector3.Distance(Target.position, transform.position) <= ChaseRange &&
		Vector3.Distance(Target.position, transform.position) > FireRange) {
			CanChase = true;
			ChaseDirection();
		}
		// Tells the player to attack if close enough
		else if (Vector3.Distance(Target.position, transform.position) <= FireRange) {
			CanChase = false;
			ChaseDirection();

			/* The switch assigns the proper cooldown and attack phase for each enemy type.
			The switch here should probably only have cases for the 3 different attack types, but 
			I have not changed it yet in case a reason emerges to have them for each enemy type. */
			switch (AlientType) {
				case 1: 
     				if (CoolDownTimer <= 0) {
						Punch = true;
						CoolDownTimer = CoolDown;
					}
					else {
						//Punch = false;
						CoolDownTimer -= Time.deltaTime;
					}
					break;
				case 2: 
     				if (CoolDownTimer <= 0) {
						AttackPhase1();
						CoolDownTimer = CoolDown;
					}
					CoolDownTimer -= Time.deltaTime;
					break;
				case 3:
     				if (CoolDownTimer <= 0) {
						AttackPhase1();
						CoolDownTimer = CoolDown;
					}
					CoolDownTimer -= Time.deltaTime;
					break;
				case 4:
     				if (CoolDownTimer <= 0) {
						AttackPhase2();
						CoolDownTimer = CoolDown;
					}
					CoolDownTimer -= Time.deltaTime;
					break;
				case 5:
     				if (CoolDownTimer <= 0) {
						AttackPhase1();
						CoolDownTimer = CoolDown;
					}
					CoolDownTimer -= Time.deltaTime;
					break;
			}
		}
		// Does nothing if out of range of chasing and attacking, will roam eventually
		else {
			CanChase = false;
			CoolDownTimer = 0;
		}
	}

	// Determines the direction the object faces when chasing
	void ChaseDirection () {

		if (Target.position.x > transform.position.x + 0.5) {
			transform.localScale = new Vector3(-1, 1, 1);
			ToTheRight = true;
		}
		else if (Target.position.x < transform.position.x + 0.5) {
			transform.localScale = new Vector3(1, 1, 1);
			ToTheRight = false;
		}
	}

	// Instantiates a chosen projectile in the scene and propels it forward like a bullet
	void AttackPhase1 () {

		if (ToTheRight == true) {
			GameObject Projectile = Instantiate (BulletObject, transform.position + new Vector3(0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
		}
		else if (ToTheRight == false) {
			GameObject Projectile = Instantiate (BulletObject, transform.position + new Vector3(-0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}
	
	}

	// Instantiates a chosen projectile in the scene and propels it forward and up like a thrown bomb
	void AttackPhase2 () {

		if (ToTheRight == true) {
			GameObject Projectile = Instantiate (BombObject, transform.position + new Vector3(0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ProjectileSpeed);
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
		}
		else if (ToTheRight == false) {
			GameObject Projectile = Instantiate (BombObject, transform.position + new Vector3(-0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ProjectileSpeed);
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}

	}

	// Takes damage from stream attacks
	void OnTriggerStay2D (Collider2D collision) {

		if (collision.gameObject.tag == "Fire") {
			EnemyHealth -= 10 * Time.deltaTime;
		}
		else if (collision.gameObject.tag == "Water") {
			EnemyHealth -= 8 * Time.deltaTime;
		}
		else if (collision.gameObject.tag == "Wind") {
			EnemyHealth -= 5 * Time.deltaTime;
		}
	}

	// Takes damage from burst attacks
	void OnTriggerEnter2D (Collider2D collision) {

		if (collision.gameObject.tag == "Electric") {
			EnemyHealth -= 10;
		}
		else if (collision.gameObject.tag == "Ice") {
			EnemyHealth -= 12;
		}
		else if (collision.gameObject.tag == "Earth") {
			EnemyHealth -= 8;
		}
	}

}