using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private playerControls Player;

	void Start () {

		Player = gameObject.GetComponentInParent<playerControls>();
		
	}
	
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Enemy") {
            Player.isJumping = false;
            Player.anim.SetBool("isJumping", false);
            Player.anim.SetBool("isFalling", false);

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemyShotT1" && !Player.Dead) {
            if (!Player.isImmune) {
                if (!Player.playerSounds.isPlaying)
                {
                    Player.playerSounds.clip = Player.playerHit;
                    Player.playerSounds.loop = false;
                    Player.playerSounds.Play();
                }
                Player.anim.SetBool("isHit", true);
                Player.isImmune = true;
                Player.health -= 5;
                StartCoroutine(Player.iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyShotT2" && !Player.Dead) {
            if (!Player.isImmune) {
                if (!Player.playerSounds.isPlaying)
                {
                    Player.playerSounds.clip = Player.playerHit;
                    Player.playerSounds.loop = false;
                    Player.playerSounds.Play();
                }
                Player.anim.SetBool("isHit", true);
                Player.isImmune = true;
                Player.health -= 10;
                StartCoroutine(Player.iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyExplosion" && !Player.Dead) {
            if (!Player.isImmune) {
                if (!Player.playerSounds.isPlaying)
                {
                    Player.playerSounds.clip = Player.playerHit;
                    Player.playerSounds.loop = false;
                    Player.playerSounds.Play();
                }
                Player.anim.SetBool("isHit", true);
                Player.isImmune = true;
                Player.health -= 10;
                StartCoroutine(Player.iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyFist" && !Player.Dead) {
            if (!Player.isImmune) {
                if (!Player.playerSounds.isPlaying)
                {
                    Player.playerSounds.clip = Player.playerHit;
                    Player.playerSounds.loop = false;
                    Player.playerSounds.Play();
                }
                Player.anim.SetBool("isHit", true);
                Player.isImmune = true;
                Player.health -= 7;
                StartCoroutine(Player.iFrames());
            }
        }
        if (collider.gameObject.tag == "enemyDeathRay" && !Player.Dead) {
            //SaucerColliding = true;
            InvokeRepeating("CollidingDeathRay", 0, 0.4f);
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "enemyDeathRay") {
            CancelInvoke("CollidingDeathRay");
        }
    }
    void CollidingDeathRay () {
        if (!Player.isImmune) {
            if (!Player.playerSounds.isPlaying)
            {
                Player.SoundCall(Player.playerHit, Player.playerVocals);
            }
            Player.anim.SetBool("isHit", true);
            Player.isImmune = true;
            Player.health -= 10;
            StartCoroutine(Player.iFrames());
        }
    }
}
