using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyControls : MonoBehaviour {

	// Private Objects
    private Rigidbody2D Rigidbody;
	private Transform Target;
	private GameController MainController;
	private EnemyDamageValues DamageValues;
    private GameObject gameController;
	private DeathRayAnimation BeamAnimation;
    private Animator anim;
	private System.Action DestroyEnemySequence;
	public System.Action OnPunch;
	private System.Action ActivateDeathBeam;
    public static System.Action<int> OnEnemyDeath;

	// Number Elements
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

	// Boolean Elements
	private bool CanRoam;
    private bool CanChase;
	public bool TouchStop;
	private bool Freeze;
	private bool CanAttack = true;
	private bool CanFireRay = true;
	public bool ToTheRight;

	// Attack Objects and Elements
	private playerControls Player;
	public GameObject BulletObject;
	public GameObject BombObject;
	public GameObject IceBlock;
	public GameObject SaucerRay;
	public Material DefaultMaterial;
	public Material HotFlash;
	public GameObject[] OtherEnemies;
	private Collider2D AttackCollider;
	private Collider2D Collider;

	// The type of enemy this is
	public int AlienType;

	// Sound Elements
    public AudioSource enemyAttacks;
    public AudioSource enemyVocals;
    public AudioSource enemyDamage;
    public AudioSource enemyAmbient;

    public AudioClip enemyPistol;
    public AudioClip enemyRapidFire;
    public AudioClip enemyLaser;
    public AudioClip enemyDeath1;
    public AudioClip enemyDeath2;
    public AudioClip enemyDeath3;
    public AudioClip enemyDeath4;
    public AudioClip enemyDeath5;
    public AudioClip rolyPolyRoll;
    public AudioClip blobSpit;
    public AudioClip beefySmash;
    public AudioClip machineGunRev;
    public AudioClip laserCharge;

    public AudioClip hitDamage;
    public AudioClip criticalDamage;
    private bool soundPlaying = false;

    // Use this for initialization
    void Start () {

		// Assignment Calls
        anim = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
		DamageValues = gameObject.GetComponent<EnemyDamageValues> ();
		Collider = gameObject.GetComponent<Collider2D> ();
		MainController = GameObject.Find ("Controller").GetComponent<GameController> ();
		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControls>();

		// Setting elements to their proper states
		InvokeRepeating ("Roam", 0, 1.5f);
		TouchStop = false;
		GetComponent<SpriteRenderer>().material = DefaultMaterial;
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

		ChaseTarget();
		//TrackOtherEnemies();

		if (EnemyHealth <= 0) {
			if (DestroyEnemySequence != null) {
				DestroyEnemySequence();
			}
		}

		if (transform.position.y <= -1.5 && AlienType != 5) {
			TouchStop = true;
		}
		else if (transform.position.y > -1.5 && AlienType != 5) {
			TouchStop = false;
		}

	}

	// Controls the actual movement of the Enemy
	void FixedUpdate() {

		// Checks if it is allowed to chase the player
		if (CanChase || CanRoam && !Freeze) {

			// Pushes the enemy in a direction based upon which side the player is on
			if (ToTheRight == false) {
     			if (TouchStop && CanAttack) {
					Vector2 myVel = Rigidbody.velocity;
                	myVel.x = -MovementSpeed;
					Rigidbody.velocity = myVel;
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
     			if (TouchStop && CanAttack) {
					Vector2 myVel = Rigidbody.velocity;
                	myVel.x = MovementSpeed;
					Rigidbody.velocity = myVel;
				}
                if (anim.GetInteger("Near") != 0 && anim.GetInteger("Near") != 1)
                {

                }
                if(anim.GetInteger("Near") == 1)
                {
                    anim.SetInteger("Near", 0);
                }
            }
		}
	}

	void ChaseTarget () {

		float Dist = Vector3.Distance(Target.position, transform.position);
		float DistX = Mathf.Abs(Target.position.x - transform.position.x);

		// Determines if the range of the player is close enough to be chased
		if (Dist <= ChaseRange && Dist > FireRange && AlienType != 5 && !Freeze) {
			CanChase = true;
			CanRoam = false;
			ChaseDirection();
		}
		// Tells the player to attack if close enough
		else if (Dist <= FireRange && AlienType != 5 && !Freeze) {
			CanChase = false;
			CanRoam = false;
			ChaseDirection();

			// This switch assigns the proper cooldown and attack phase for each enemy type.
			if (CanAttack) {
				if (TouchStop) {
					switch (AlienType) {
						// Roly Poly Alien
						case 1:
							CanAttack = false;
							anim.SetInteger("Near", 1);
							StartCoroutine(DashAttack());
							break;
						// Blob Alien
						case 2:
							CanAttack = false;
							anim.SetInteger("Near", 1);
							StartCoroutine(BombAttack());
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
                            anim.SetInteger("Near", 1);
                            StartCoroutine(GunAttack());
							break;
					}
				}
			}
		}
		// Saucer Chase Check
		else if (DistX <= FireRange && DistX > 0.5 && AlienType == 5) {
			CanChase = true;
			CanRoam = false;
			ChaseDirection();
			if (CanFireRay == true) {
				SaucerRay.GetComponent<Collider2D>().enabled = true;
				StartCoroutine(RayTime());
			}
		}
		// Saucer Attack Check
		else if (DistX <= 0.5 && AlienType == 5) {
			CanChase = false;
			Rigidbody.velocity = Vector2.zero;
			ChaseDirection();
			if (CanFireRay == true) {
				SaucerRay.GetComponent<Collider2D>().enabled = true;
				StartCoroutine(RayTime());
			}
		}
		// Roams out of range of chasing and attacking
		else {
			CanChase = false;
			CanRoam = true;
			if (AlienType == 5) {
				SaucerRay.GetComponent<Collider2D>().enabled = false;
				SaucerRay.SetActive(false);
			}
			CoolDownTimer = 0;
            enemyAttacks.Stop();
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

		OtherEnemies = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < OtherEnemies.Length; i++) {

			Vector3 toTarget = (OtherEnemies[i].transform.position - transform.position);
			
			if (Vector3.Dot(toTarget, transform.right) < 0) {
				TouchStop = false;
			} else if (Vector3.Dot(toTarget, transform.right) > 0) {
				TouchStop = true;
			}

		}

	}

    // Instantiates a chosen projectile in the scene and propels it forward like a bullet
    private IEnumerator GunAttack () {

		Rigidbody.velocity = Vector2.zero;
        SoundCall(machineGunRev, enemyAmbient);
        yield return new WaitForSeconds(0.6f);

        if (AlienType == 4) {
            SoundCall(enemyRapidFire, enemyAttacks);
        }
        if (ToTheRight == true)
        {
            GameObject Projectile = Instantiate(BulletObject, transform.position + new Vector3(1.0f, .10f, 0),
            Quaternion.identity) as GameObject;
            Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
        }
        else if (ToTheRight == false)
        {
			GameObject Projectile = Instantiate (BulletObject, transform.position + new Vector3(-1.0f, .10f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}
		StartCoroutine(shootWait());
	
	}

	// Instantiates a chosen projectile in the scene and propels it forward and up like a thrown bomb
	private IEnumerator BombAttack () {
		
		Rigidbody.velocity = Vector2.zero;
		yield return new WaitForSeconds(0.2f);
        SoundCall(blobSpit, enemyAttacks);
        if (ToTheRight == true) {
			GameObject Projectile = Instantiate (BombObject, transform.position + new Vector3(0.5f, 0.5f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ProjectileSpeed);
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.right * ProjectileSpeed);
		}
		else if (ToTheRight == false) {
			GameObject Projectile = Instantiate (BombObject, transform.position + new Vector3(-0.5f, 0.5f, 0), 
			Quaternion.identity) as GameObject;
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * ProjectileSpeed);
			Projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.left * ProjectileSpeed);
		}
        StartCoroutine(shootWait());
	}

	// Propels this enemy toward the player
	private IEnumerator JumpSmashAttack () {

		Rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Rigidbody2D>().AddForce
			(new Vector3 (Target.position.x - transform.position.x, 0, 0) * 43);
		GetComponent<Rigidbody2D>().AddForce(Vector3.up * 750);
        yield return new WaitForSeconds(.5f);
        anim.SetInteger("Near", 2);
        yield return new WaitForSeconds(.2f);
        AttackCollider.enabled = true;
		Rigidbody.gravityScale = 12;
        yield return new WaitForSeconds(0.4f);
		Rigidbody.gravityScale = 2;
		AttackCollider.enabled = false;
        SoundCall(beefySmash, enemyAttacks);
        StartCoroutine(shootWait());

    }

	// Dash attack cycle
    private IEnumerator DashAttack()
    {
		Rigidbody.velocity = Vector2.zero;
        if (ToTheRight) {
            DashDirection = 1;
			anim.SetInteger("R_or_L", 1);
        }
        else if (!ToTheRight) {
            DashDirection = 2;
			anim.SetInteger("R_or_L", 2);
        }
		yield return new WaitForSeconds(0.6f);
		AttackCollider.enabled = true;
		SoundCall(rolyPolyRoll, enemyAttacks);
		if (!Freeze) {
			if (DashDirection == 1) {
				Rigidbody.AddForce(Vector2.right * ProjectileSpeed, ForceMode2D.Impulse);
			}
			else if (DashDirection == 2) {
				Rigidbody.AddForce(Vector2.left * ProjectileSpeed, ForceMode2D.Impulse);
			}
		}
		yield return new WaitForSeconds(0.6f);
		anim.SetInteger("Near", 2);
		Rigidbody.velocity = Vector2.zero;
		Rigidbody.angularVelocity = 0.0f;
		AttackCollider.enabled = false;
		StartCoroutine(shootWait());
	}

	// Saucer attack cycle
	private IEnumerator RayTime () {
		
		CanFireRay = false;
        SoundCall(laserCharge, enemyAmbient);
        yield return new WaitForSeconds(2.5f);
		SaucerRay.SetActive(true);
		BeamAnimation.PlayBeamAnim();
        SoundCall(enemyLaser, enemyAttacks);
        yield return new WaitForSeconds(3);
		BeamAnimation.PlayRetractAnim();
		yield return new WaitForSeconds(0.2f);
        enemyAttacks.Stop();
		SaucerRay.SetActive(false);
		CanFireRay = true;
	}

	// Cooldown before allowed to attack again
    private IEnumerator shootWait()
	{
    	// anim.SetInteger("Near", 0);
    	yield return new WaitForSeconds(CoolDown);
        CanAttack = true;
    }

	void EnemyDeathSequence () {

		DestroyEnemySequence = null;
		Rigidbody.velocity = Vector2.zero;
		Freeze = true;
		MainController.score += enemyValue;
		MainController.EnemiesLeft--;

		if (AlienType == 1)
		{
			if (soundPlaying == false)
			{
                SoundCall(enemyDeath4, enemyVocals);
                soundPlaying = true;
			}
		}
        else if (AlienType == 2)
        {
            if (soundPlaying == false)
            {
                SoundCall(enemyDeath5, enemyVocals);
                soundPlaying = true;
            }
        }
        else if (AlienType == 3)
        {
            if (soundPlaying == false)
            {
                SoundCall(enemyDeath2, enemyVocals);
                soundPlaying = true;
            }
        }
        else if (AlienType == 4)
		{
			if (soundPlaying == false)
			{
                SoundCall(enemyDeath1, enemyVocals);
                soundPlaying = true;
			}
		}
		else if (AlienType == 5)
		{
			if (soundPlaying == false)
			{
                SoundCall(enemyDeath3, enemyVocals);
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
			StartCoroutine(HitByAttack(100, 200, 0.3f));
            if (AlienType == 4)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 4)
            {
                SoundCall(hitDamage, enemyDamage);
            }

        }
		if (collision.gameObject.name == "LightningBullet2(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage * 1.2f;
			StartCoroutine(HitByAttack(200, 200, 0.5f));
            if (AlienType == 4)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 4)
            {
                SoundCall(hitDamage, enemyDamage);
            }
        }
		if (collision.gameObject.name == "LightningBullet3(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage * 1.5f;
			StartCoroutine(HitByAttack(300, 200, 1));
            if (AlienType == 4)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 4)
            {
                SoundCall(hitDamage, enemyDamage);
            }
        }
		if (collision.gameObject.name == "LightningBullet4(Clone)") {
			EnemyHealth -= DamageValues.ElectricDamage * 2.0f;
			StartCoroutine(HitByAttack(400, 200, 1.5f));
            if (AlienType == 4)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 4)
            {
                SoundCall(hitDamage, enemyDamage);
            }
        }
		else if (collision.gameObject.tag == "Earth") {
			StartCoroutine(HitByAttack(0, 400, 2));
			EnemyHealth -= DamageValues.EarthDamage;
		}
		else if (collision.gameObject.tag == "Speed") {
			StartCoroutine(HitByAttack(0, 200, 0.3f));
			EnemyHealth -= DamageValues.SpeedDamage;
		}
		else if (collision.gameObject.name == "AnchorArms") {
			StartCoroutine(HitByAttack(200, 300, 1));
			EnemyHealth -= DamageValues.JackedDamage;
		}
			// Takes damage from stream attacks
		else if (collision.gameObject.tag == "Fire") {
			InvokeRepeating("TakeFireDamage", 0, 0.5f);
            if (AlienType == 1)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 1)
            {
                SoundCall(hitDamage, enemyDamage);
            }
        }
		else if (collision.gameObject.tag == "Ice") {
			GameObject Projectile = Instantiate (IceBlock, transform.position + new Vector3(0, 0, 0), 
			Quaternion.identity) as GameObject;
			Rigidbody.velocity = Vector2.zero;
			StartCoroutine(HitByAttack(0, 0, 3));
            if (AlienType == 3)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 3)
            {
                SoundCall(hitDamage, enemyDamage);
            }
        }
		else if (collision.gameObject.tag == "IceBlock") {
			InvokeRepeating("TakeIceDamage", 0, 0.5f);
		}
		else if (collision.gameObject.tag == "Water") {
			InvokeRepeating("TakeWaterDamage", 0, 0.5f);
            if (AlienType == 2)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 2)
            {
                SoundCall(hitDamage, enemyDamage);
            }
        }
		else if (collision.gameObject.tag == "Wind") {
			InvokeRepeating("TakeWindDamage", 0, 0.5f);
			StartCoroutine(HitByAttack(300, 600, 2));
            if (AlienType == 5)
            {
                SoundCall(criticalDamage, enemyDamage);
            }
            if (AlienType != 5)
            {
                SoundCall(hitDamage, enemyDamage);
            }
		}
	}

	private IEnumerator HitByAttack (int xSpeed, int ySpeed, float Seconds) {
		Freeze = true;
		GetComponent<SpriteRenderer>().material = HotFlash;
		Rigidbody.AddForce(Vector3.up * ySpeed);
		if (Player.facingRight) {
			Rigidbody.AddForce(Vector3.right * xSpeed);
		}
		else if (!Player.facingRight) {
			Rigidbody.AddForce(Vector3.left * xSpeed);
		}
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().material = DefaultMaterial;
		yield return new WaitForSeconds(Seconds);
		Freeze = false;
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
		}
	}

	void TakeFireDamage() {
		EnemyHealth -= DamageValues.FireDamage;
		StartCoroutine(HitByAttack(100, 100, 0.5f));
	}
	void TakeWaterDamage() {
		EnemyHealth -= DamageValues.WaterDamage;
		StartCoroutine(HitByAttack(100, 100, 0.5f));
	}
	void TakeWindDamage() {
		EnemyHealth -= DamageValues.WindDamage;
	}
	void TakeIceDamage() {
		EnemyHealth -= DamageValues.IceDamage;
	}

	private void Roam () {
		if (CanRoam) {
			ChaseDirection();
		}
	}

    void SoundCall(AudioClip clip, AudioSource source)
    {

        source.clip = clip;
        source.loop = false;
        source.loop |= (source.clip == enemyLaser);
        source.Play();
    }

}
