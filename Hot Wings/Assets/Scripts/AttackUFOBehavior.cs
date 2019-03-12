using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUFOBehavior : MonoBehaviour {

	// Private Objects
    private Rigidbody2D Rigidbody;
	private Transform Target;
	private GameController MainController;
	private EnemyDamageValues DamageValues;
	private DeathRayAnimation DeathRayAnimation;
	private SpriteRenderer SpriteRender;
    public static System.Action<int> OnEnemyDeath;

	// Number Elements
	public float EnemyHealth;
    public int enemyValue;
    public float MovementSpeed;
	public float ChaseRange;
	public float FireRange;
	public float CoolDown;
	private float CoolDownTimer = 0;
	private int DashDirection;

	// Boolean Elements
	private bool CanRoam;
    private bool CanChase;
	public bool TouchStop;
	private bool Freeze;
	private bool Dead;
	private bool CanAttack = true;
	private bool CanFireRay = true;
	public bool ToTheRight;
	[HideInInspector] public bool CanSpawnIceBlock = true;

	// Attack Objects and Elements
	private playerControls Player;
	public GameObject IceBlock;
	public GameObject SaucerRay;
	public GameObject ExplodingSaucer;
	public Sprite NormalAppearance;
	public Sprite AttackAppearance;
	public Material DefaultMaterial;
	public Material HotFlash;
	private Collider2D Collider;

	// The type of enemy this is
	public int AlienType;

	// Sound Elements
    public AudioSource enemyAttacks;
    public AudioSource enemyVocals;
    public AudioSource enemyDamage;
    public AudioSource enemyAmbient;

    public AudioClip enemyLaser;
    public AudioClip enemyDeath;
    public AudioClip laserCharge;
    public AudioClip hitDamage;
    public AudioClip criticalDamage;
    private bool soundPlaying = false;

    // Use this for initialization
    void Start () {


        FloatingTextController.Initialize();
        CriticalFloatingTextController.Initialize();
		
        // Assignment Calls
        Rigidbody = GetComponent<Rigidbody2D>();
		DamageValues = gameObject.GetComponent<EnemyDamageValues> ();
		Collider = gameObject.GetComponent<Collider2D> ();
		SpriteRender = gameObject.GetComponent<SpriteRenderer>();
		MainController = GameObject.Find ("Controller").GetComponent<GameController> ();
		Player = GameObject.Find("Player").GetComponent<playerControls>();
		DeathRayAnimation = SaucerRay.GetComponent<DeathRayAnimation>();

		// Setting elements to their proper states
		InvokeRepeating ("Roam", 0, 1.5f);
		TouchStop = false;
		GetComponent<SpriteRenderer>().material = DefaultMaterial;
		SpriteRender.sprite = NormalAppearance;
		MainController.EnemiesLeft++;
		TouchStop = true;
		
	}
	
	// Update is called once per frame
	void Update () {

		// Finds the Player's transform and stores it in target
		Target = GameObject.FindGameObjectWithTag ("Player").transform;

		ChaseTarget();

		if (EnemyHealth <= 0) {
			if (!Dead) {
				EnemyDeathSequence();
			}
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
			}
			else if (ToTheRight == true) {
     			if (TouchStop && CanAttack) {
					Vector2 myVel = Rigidbody.velocity;
                	myVel.x = MovementSpeed;
					Rigidbody.velocity = myVel;
				}
            }
		}
	}

	void ChaseTarget () {

		float Dist = Vector3.Distance(Target.position, transform.position);
		float DistX = Mathf.Abs(Target.position.x - transform.position.x);

		if (DistX <= FireRange && DistX > 0.5 && !Freeze) {
			CanChase = true;
			CanRoam = false;
			ChaseDirection();
			if (CanFireRay == true) {
				StartCoroutine(RayTime());
			}
		}
		// Saucer Attack Check
		else if (DistX <= 0.5 && !Freeze) {
			CanChase = false;
			Rigidbody.velocity = Vector2.zero;
			ChaseDirection();
			if (CanFireRay == true) {
				StartCoroutine(RayTime());
			}
		}
		// Roams out of range of chasing and attacking
		else if (!Freeze) {
			CanChase = false;
			CanRoam = true;
			SaucerRay.GetComponent<Collider2D>().enabled = false;
			CoolDownTimer = 0;
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
		else if (CanRoam == true) {
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

	// Saucer attack cycle
	private IEnumerator RayTime () {
		
		CanFireRay = false;
		yield return new WaitForSeconds(1.5f);
        SoundCall(laserCharge, enemyAmbient);
        yield return new WaitForSeconds(1);
		SpriteRender.sprite = AttackAppearance;
		DeathRayAnimation.TurnOnBeam();
        SoundCall(enemyLaser, enemyAttacks);
        yield return new WaitForSeconds(3);
		DeathRayAnimation.TurnOffBeam();
		yield return new WaitForSeconds(0.2f);
		SpriteRender.sprite = NormalAppearance;
        enemyAttacks.Stop();
		CanFireRay = true;
	}

	private void EnemyDeathSequence() {

		Dead = true;
		Freeze = true;
		Rigidbody.velocity = Vector2.zero;
		SpriteRender.sprite = NormalAppearance;
		DeathRayAnimation.TurnOffBeam();
		enemyAttacks.Stop();
		SoundCall(enemyDeath, enemyVocals);
		soundPlaying = true;
		SaucerRay.GetComponent<SpriteRenderer>().enabled = false;
		SaucerRay.GetComponent<Collider2D>().enabled = false;
		Instantiate(ExplodingSaucer, transform.position, Quaternion.identity);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		MainController.score += enemyValue;
		GameObject.Find("Score").GetComponent<Animator>().SetTrigger("Bulge");
		MainController.EnemiesLeft--;
		if (OnEnemyDeath != null)
		{
			OnEnemyDeath(AlienType);
		}
		Destroy(gameObject, 1.0f);
		SaucerRay.GetComponent<Collider2D>().enabled = false;
	}

	public void StartTheInvokes(string DamageToTake, float AmountPerSecond) {
		InvokeRepeating(DamageToTake, 0, AmountPerSecond);
	}

	public void StopTheInvokes(string DamageToStop) {
		CancelInvoke(DamageToStop);
	}

	public IEnumerator HitByAttack (int xSpeed, int ySpeed, float Seconds) {
		if (!Dead) {
			Freeze = true;
			GetComponent<SpriteRenderer>().material = HotFlash;
			yield return new WaitForSeconds(0.1f);
			GetComponent<SpriteRenderer>().material = DefaultMaterial;
			yield return new WaitForSeconds(Seconds);
			Freeze = false;
		}
	}

	void TakeFireDamage() {

		TakeDamage(DamageValues.FireDamage);
		EnemyHealth -= DamageValues.FireDamage;
		StartCoroutine(HitByAttack(100, 100, 0.5f));
	}

	void TakeWaterDamage() {

		TakeDamage(DamageValues.WaterDamage);
		EnemyHealth -= DamageValues.WaterDamage;
		StartCoroutine(HitByAttack(100, 100, 0.5f));
	}

	void TakeWindDamage() {

        CriticalTakeDamage(DamageValues.WindDamage);
        EnemyHealth -= DamageValues.WindDamage;
    }

	public IEnumerator TakeIceDamage() {

		StartCoroutine(HitByAttack(0, 0, 3));
		Rigidbody.velocity = Vector2.zero;
		GameObject Projectile = Instantiate (IceBlock, transform.position + new Vector3(0, 0, 0), 
		Quaternion.identity) as GameObject;
		Projectile.transform.parent = this.gameObject.transform;
		for (int i = 0; i < 3; i++) {
			TakeDamage(DamageValues.IceDamage);
			EnemyHealth -= DamageValues.IceDamage;
			yield return new WaitForSeconds(1);
			CanSpawnIceBlock = true;
		}
    }

	private void Roam () {
		if (CanRoam) {
			ChaseDirection();
		}
    }
  

    public void TakeDamage(float amount)
    {
        FloatingTextController.CreateFloatingText(amount.ToString(), this.transform);
       // Debug.LogFormat("{0} was dealt {1} damage", gameObject.name, amount);
    }
    public void CriticalTakeDamage(float amount)
    {
        CriticalFloatingTextController.CreateFloatingText(amount.ToString(), this.transform);
        // Debug.LogFormat("{0} was dealt {1} damage", gameObject.name, amount);
    }

    public void SoundCall(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.loop = false;
        source.loop |= (source.clip == enemyLaser);
        source.Play();
    }

}