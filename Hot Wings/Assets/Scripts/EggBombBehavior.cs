using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBombBehavior : MonoBehaviour {

	private Animator ExplodeAnim;
	public float Timer;
	private bool PlayAnim;
	private Collider2D Collider;
    private AudioSource bombSound;
    public AudioClip fireBomb;
    public AudioClip waterBomb;
    public AudioClip shockBomb;
    public AudioClip iceBomb;

    // Use this for initialization
    void Start () {

        bombSound = gameObject.GetComponent<AudioSource>();
        if(gameObject.name == "EggElectricBomb")
        {
            bombSound.clip = shockBomb;
        }
        else if (gameObject.name == "EggFireBomb")
        {
            bombSound.clip = fireBomb;
        }
        else if (gameObject.name == "EggIceBomb")
        {
            bombSound.clip = iceBomb;
        }
        else if (gameObject.name == "EggWaterBomb")
        {
            bombSound.clip = waterBomb;
        }
        ExplodeAnim = gameObject.transform.GetChild(0).GetComponent<Animator>();
		PlayAnim = true;
		Collider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
		//gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Player";
		
	}
	
	// Update is called once per frame
	void Update () {
	
		Timer -= Time.deltaTime;
		if (Timer <= 0 && PlayAnim == true) {
			PlayAnim = false;
			ExplodeAnim.SetTrigger("Boom");
			Collider.enabled = true;
            bombSound.Play();
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, 0.6f);
		}
	}
}
