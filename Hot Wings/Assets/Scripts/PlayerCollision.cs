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

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground" || collider.gameObject.tag == "Enemy") {
            if (!Player.animator.GetCurrentAnimatorStateInfo(0).IsName("HotWingsJump") &&
            !Player.animator.GetCurrentAnimatorStateInfo(0).IsName("HotWingsBuffJumpIni")) {
                Player.isJumping = false;
            }
            if (Player.animator.GetCurrentAnimatorStateInfo(0).IsName("HotWingsFall") ||
            Player.animator.GetCurrentAnimatorStateInfo(0).IsName("HotWingsBuffJumpFall")) {
                if (!Player.isBuff)
                    Player.animator.Play("HotWingsIdle2");
                    Debug.Log("Called");
                if (Player.isBuff) {
                    Player.animator.Play("HotWingsBuffIdle");
                }
            }

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
                Player.isImmune = true;
                Player.health -= 10;
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
                Player.isImmune = true;
                Player.health -= 20;
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
                Player.isImmune = true;
                Player.health -= 12;
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
                Player.isImmune = true;
                Player.health -= 15;
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
            Player.isImmune = true;
            Player.health -= 10;
            StartCoroutine(Player.iFrames());
        }
    }
}
