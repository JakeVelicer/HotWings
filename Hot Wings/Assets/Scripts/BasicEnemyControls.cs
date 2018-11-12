using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControls : MonoBehaviour {

    Animator anim;

    private Rigidbody2D Rigidbody;
	private Transform Target;
	private GameController MainController;
	private EnemyDamageValues DamageValues;
    private GameObject gameController;
	private DeathRayAnimation BeamAnimation;
	private System.Action DestroyEnemySequence;

	public float EnemyHealth;
    public int enemyValue;
    public float MovementSpeed;
	public float ChaseRange;
	public float FireRange;
	public float ProjectileSpeed;
	public float ProjectileHeight;
	public float CoolDown;
	private float CoolDownTimer = 0;
	private int DashDirection;

	private bool CanRoam;
    private bool CanChase;
	public bool TouchStop;
	private bool CanAttack = true;
	private bool CanFireRay = true;
	public bool ToTheRight;

	private playerControls Player;
	public GameObject BulletObject;
	public GameObject BombObject;
	public GameObject IceBlock;
	public GameObject SaucerRay;
	private Collider2D AttackCollider;
	private Collider2D Collider;
	public int AlienType;
	public System.Action OnPunch;
	private System.Action ActivateDeathBeam;

    private AudioSource enemySounds;
    public AudioClip enemyPistol;
    public AudioClip enemyRapidFire;
    public AudioClip enemyLaser;
    public AudioClip enemyDeath1;
    public AudioClip enemyDeath2;
    public AudioClip enemyDeath3;
    public AudioClip enemyDeath4;
    public AudioClip enemyDeath5;

    public static System.Action<int> OnEnemyDeath;

    private bool soundPlaying = false;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        enemySounds = gameObject.GetComponent<AudioSource>();
        enemySounds.loop = false;
		Rigidbody = GetComponent<Rigidbody2D>();
		DamageValues = gameObject.GetComponent<EnemyDamageValues> ();
		Collider = gameObject.GetComponent<Collider2D> ();
		MainController = GameObject.Find ("Controller").GetComponent<GameController> ();
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControls>();
		InvokeRepeating ("Roam", 0, 2.0f);
		TouchStop = false;
		MainController.EnemiesLeft++;
		DestroyEnemySequence += EnemyDeathSequence;
		
		if (AlienType == 1 || AlienType == 3) {
			AttackCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
			AttackCollider.enabled = false;
		}
		if (AlienType == 5) {
			TouchStop = true;
			BeamAnimation = SaucerRay.GetComponent<DeathRayAnimation>();
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		// Finds the Player's transform and stores it in target
		Target = GameObject.FindGameObjectWithTag ("Player").transform;

        Movement();
		ChaseTarget();
		//TrackOtherEnemies();
		if (EnemyHealth <= 0) {
			if (DestroyEnemySequence != null) {
				DestroyEnemySequence();
			}
		}
	}

	// Controls the actual movement of the object
	void Movement () {

		// Checks if it is allowed to chase the player
		if (CanChase == true || CanRoam == true) {

			// Pushes the enemy in a direction based upon which side the player is on
			if (ToTheRight == false) {
     			if (TouchStop) {
                	transform.Translate (Vector3.left * Time.deltaTime * MovementSpeed);
				}
                if (anim.GetInteger("Near") != 0 && anim.GetInteger("Near") != 1)
                {

                }
                if ((anim.GetInteger("Near") == 1))
                {
                    anim.SetInteger("Near", 0);
                }
			}
			else if (ToTheRight == true) {
     			if (TouchStop) {
					transform.Translate (Vector3.right * Time.deltaTime * MovementSpeed);
				}
                if (anim.GetInteger("Near") != 0 && anim.GetInteger("Near") != 1)
                {

                }
                if(anim.GetInteger("Near") == 1)
                {
                    anim.SetInteger("Near", 0);
                }
            }
			//Rigidbody.AddForce (Vector3.right * MovementSpeed);
		}
	}

	void ChaseTarget () {

		float Dist = Vector3.Distance(Target.position, transform.position);
		float DistX = Mathf.Abs(Target.position.x - transform.position.x);

		// Determines if the range of the player is close enough to be chased
		if (Dist <= ChaseRange && Dist > FireRange && AlienType != 5) {
			CanChase = true;
			CanRoam = false;
			ChaseDirection();
		}
		// Tells the player to attack if close enough
		else if (Dist <= FireRange && AlienType != 5) {
			CanChase = false;
			CanRoam = false;
			ChaseDirection();

			/* The switch assigns the proper cooldown and attack phase for each enemy type.
			The switch here should probably only have cases for the 3 different attack types, but 
			I have not changed it yet in case a reason emerges to have them for each enemy type. */
			if (CanAttack) {
				if (TouchStop) {
					switch (AlienType) {
						// Roly Poly Alien
						case 1:
								CanAttack = false;
								StartCoroutine(DashAttack());
							break;
						// Blob Alien
						case 2:
							CanAttack = false;
							anim.SetInteger("Near", 1);
							BombAttack();
							StartCoroutine(shootWait());
							break;
						// Beefy Alien
						case 3: 

							CanAttack = false;
							anim.SetInteger("Near", 1);
							StartCoroutine(JumpSmashAttack());
							//if (OnPunch != null) {
								//anim.SetInteger("Near", 1);
								//OnPunch();
							//}
							break;
						// Armored Alien
						case 4:
							CanAttack = false;
							GunAttack();
							StartCoroutine(shootWait());
							break;
					}
				}
			}
		}
		// Saucer Attack Check
		else if (DistX <= FireRange && DistX > 0.5 && AlienType == 5) {
			CanChase = true;
			ChaseDirection();
			if (CanFireRay == true) {
				StartCoroutine(RayTime());
			}
		}
		// Saucer Stop Check
		else if (DistX > ChaseRange && AlienType == 5) {
			CanChase = false;
			SaucerRay.SetActive(false);
			ChaseDirection();
		}
		// Does nothing if out of range of chasing and attacking, will roam eventually
		else {
			CanChase = false;
			if (AlienType != 5) {
				CanRoam = true;
			}
			CoolDownTimer = 0;
            enemySounds.Stop();
            soundPlaying = false;
		}
	}

	// Determines the direction the object faces when chasing
	void ChaseDirection () {

		if (CanRoam == false) {
			if (Target.position.x > transform.position.x + 0.5) {
				transform.localScale = new Vector3(-1, 1, 1);
				ToTheRight = true;
			}
			else if (Target.position.x < transform.position.x + 0.5) {
				transform.localScale = new Vector3(1, 1, 1);
				ToTheRight = false;
			}
		}
		if (CanRoam == true) {
			if (ToTheRight == false) {
				transform.localScale = new Vector3(-1, 1, 1);
				ToTheRight = true;
			}
			else if (ToTheRight == true) {
				transform.localScale = new Vector3(1, 1, 1);
				ToTheRight = false;
			}
		}
	}

	void TrackOtherEnemies () {

		Vector2 vector = Vector2.right;
		if (ToTheRight == true) {
			vector = Vector2.right;
		}
		else if (ToTheRight == false) {
			vector = Vector2.left;
		}

		RaycastHit2D hit = Physics2D.Raycast(transform.position, vector , 5);

		if (hit.collider != null && hit.collider.tag == ("Enemy")) {
			TouchStop = false;
			Debug.Log("Called");
		}
		else {
			TouchStop = true;
		}

	}


	// Instantiates a chosen projectile in the scene and propels it forward like a bullet
	void GunAttack () {

        if (AlienType == 1) {
            enemySounds.clip = enemyPistol;
            enemySounds.loop = false;
        }
        if (AlienType == 4) {
            enemySounds.clip = enemyRapidFire;
            enemySounds.loop = true;
        }
        if (ToTheRight == true)
        {
            enemySounds.Play();
            GameObject Projectile = Instantiate(BulletObject, transform.position + new Vector3(0.86f, 0.24f, 0),
            Quaternion.identity) as GameObject;
            Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
        }
        else if (ToTheRight == false)
        {
            if (soundPlaying == false)
            {
                enemySounds.Play();
            }
            soundPlaying = true;
			GameObject Projectile = Instantiate (BulletObject, transform.position + new Vector3(-0.86f, 0.24f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}
	
	}

	// Instantiates a chosen projectile in the scene and propels it forward and up like a thrown bomb
	void BombAttack () {

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

	// Propels this enemy toward the player
	private IEnumerator JumpSmashAttack () {
        yield return new WaitForSeconds(.2f);
        // anim.SetInteger("Near", 2);
        gameObject.GetComponent<Rigidbody2D>().AddForce
		(new Vector3 (Target.position.x - transform.position.x, 0, 0) * 270);
		GetComponent<Rigidbody2D>().AddForce(Vector3.up * 2500);
        //anim.SetInteger("Near", 2); 
        //anim.SetInteger("Near", 2);
        yield return new WaitForSeconds(.5f);
        anim.SetInteger("Near", 2);
        yield return new WaitForSeconds(.3f);
        AttackCollider.enabled = true;
		Rigidbody.gravityScale = 12;

        yield return new WaitForSeconds(0.4f);
		Rigidbody.gravityScale = 2;
		AttackCollider.enabled = false;
       
        StartCoroutine(shootWait());


    }

	// Dash attack cycle
    private IEnumerator DashAttack()
    {
		AttackCollider.enabled = true;
        if (ToTheRight) {
            DashDirection = 1;
        }
        else if (!ToTheRight) {
            DashDirection = 2;
        }
        for (float i = 0; i < 1; i += 0.1f) {
            if (i < 0.9f) {
                if (DashDirection == 1) {
                    Rigidbody.velocity = Vector2.right * ProjectileSpeed;
                }
                else if (DashDirection == 2) {
                    Rigidbody.velocity = Vector2.left * ProjectileSpeed;
                }
            }
            else if (i >= 0.9f) {
                yield return new WaitForSeconds(0.6f);
                Rigidbody.velocity = Vector2.zero;
				AttackCollider.enabled = false;
            }
        }
		StartCoroutine(shootWait());
	}

	// Saucer attack cycle
	private IEnumerator RayTime () {
		CanFireRay = false;
		yield return new WaitForSeconds(3);
		SaucerRay.SetActive(true);
		BeamAnimation.PlayBeamAnim();
        enemySounds.clip = enemyLaser;
        enemySounds.loop = true;
        enemySounds.Play();
		yield return new WaitForSeconds(3);
		BeamAnimation.PlayRetractAnim();
		yield return new WaitForSeconds(0.2f);
        enemySounds.Stop();
        enemySounds.loop = false;
		SaucerRay.SetActive(false);
		CanFireRay = true;
	}

    private IEnumerator shootWait()
	{
       // anim.SetInteger("Near", 0);
        yield return new WaitForSeconds(CoolDown);
        CanAttack = true;
    }

	void EnemyDeathSequence () {

		DestroyEnemySequence = null;
		MainController.score += enemyValue;
		MainController.EnemiesLeft--;

		if (AlienType == 1)
		{
			enemySounds.clip = enemyDeath4;
			if (soundPlaying == false)
			{
				enemySounds.Play();
				soundPlaying = true;
			}
		}
        else if (AlienType == 2)
        {
            enemySounds.clip = enemyDeath5;
            if (soundPlaying == false)
            {
                enemySounds.Play();
                soundPlaying = true;
            }
        }
        else if (AlienType == 3)
        {
            enemySounds.clip = enemyDeath2;
            if (soundPlaying == false)
            {
                enemySounds.Play();
                soundPlaying = true;
            }
        }
        else if (AlienType == 4)
		{
			enemySounds.clip = enemyDeath1;
			if (soundPlaying == false)
			{
				enemySounds.Play();
				soundPlaying = true;
			}
		}
		else if (AlienType == 5)
		{
			enemySounds.clip = enemyDeath3;
			if (soundPlaying == false)
			{
				enemySounds.Play();
				soundPlaying = true;
			}
		}
		if (OnEnemyDeath != null)
		{
			OnEnemyDeath(AlienType);
		}

		if (AlienType == 5)
		{
			Destroy(gameObject, 1.0f);
		}
		else
		{
			Destroy(gameObject, 0.3f);
		}
	}

    void OnTriggerEnter2D(Collider2D collision) {

			// Takes damage from burst attacks
		if (collision.gameObject.name == "LightningBullet(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage;
		}
		if (collision.gameObject.name == "LightningBullet2(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage * 1.2f;
		}
		if (collision.gameObject.name == "LightningBullet3(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage * 1.5f;
		}
		if (collision.gameObject.name == "LightningBullet4(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage * 2.0f;
		}
		else if (collision.gameObject.tag == "Earth") {
			EnemyHealth -= DamageValues.EarthDamage;
		}
		else if (collision.gameObject.name == "AnchorArms") {
			EnemyHealth -= DamageValues.JackedDamage;
		}
			// Takes damage from stream attacks
		else if (collision.gameObject.tag == "Fire") {
			InvokeRepeating("TakeFireDamage", 0, 0.5f);
		}
		else if (collision.gameObject.tag == "Ice") {
			GameObject Projectile = Instantiate (IceBlock, transform.position + new Vector3(0, 0, 0), 
			Quaternion.identity) as GameObject;
		}
		else if (collision.gameObject.tag == "IceBlock") {
			InvokeRepeating("TakeIceDamage", 0, 0.5f);
			TouchStop = false;
		}
		else if (collision.gameObject.tag == "Water") {
			InvokeRepeating("TakeWaterDamage", 0, 0.5f);
		}
		else if (collision.gameObject.tag == "Wind") {
			InvokeRepeating("TakeWindDamage", 0, 0.5f);
			Rigidbody.AddForce(Vector3.up * 600);
			if (Player.facingRight) {
				Rigidbody.AddForce(Vector3.right * 600);
			}
			else if (!Player.facingRight) {
				Rigidbody.AddForce(Vector3.left * 600);
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Fire") {
			CancelInvoke("TakeFireDamage");
		}
		else if (collider.gameObject.tag == "Water") {
			CancelInvoke("TakeWaterDamage");
		}
		else if (collider.gameObject.tag == "Wind") {
			CancelInvoke("TakeWindDamage");
		}
		else if (collider.gameObject.tag == "IceBlock") {
			CancelInvoke("TakeIceDamage");
			TouchStop = true;
		}
	}

	void TakeFireDamage() {
		EnemyHealth -= DamageValues.FireDamage;
	}
	void TakeWaterDamage() {
		EnemyHealth -= DamageValues.WaterDamage;
	}
	void TakeWindDamage() {
		EnemyHealth -= DamageValues.WindDamage;
	}
	void TakeIceDamage() {
		EnemyHealth -= DamageValues.IceDamage;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			TouchStop = true;
		}
	}
	
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Ground") {
			TouchStop = false;
		}
	}

	private void Roam () {
		if (CanRoam) {
			ChaseDirection();
		}
	}

}