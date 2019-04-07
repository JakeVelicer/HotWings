using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSaucerCollision : MonoBehaviour {

	private AttackUFOBehavior AttackUFOScript;
	private EnemyDamageValues DamageValues;
	private bool CanTakeDamage = true;

	void Start() {

		AttackUFOScript = GetComponentInParent<AttackUFOBehavior>();
		DamageValues = GetComponentInParent<EnemyDamageValues>();
		
	}

    void OnTriggerEnter2D(Collider2D collision) {

			// Takes damage from burst attacks
		if (collision.gameObject.name == "LightningBullet(Clone)") {
			Destroy(collision.gameObject);
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage;
			StartCoroutine(AttackUFOScript.HitByAttack(100, 200, 0.3f));
			AttackUFOScript.DisplayDamage(DamageValues.ElectricDamage);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		if (collision.gameObject.name == "LightningBullet2(Clone)") {
			Destroy(collision.gameObject);
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage * 1.2f;
			StartCoroutine(AttackUFOScript.HitByAttack(200, 200, 0.5f));
			AttackUFOScript.DisplayDamage(DamageValues.ElectricDamage * 1.2f);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		if (collision.gameObject.name == "LightningBullet3(Clone)") {
			Destroy(collision.gameObject);
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage * 1.5f;
			StartCoroutine(AttackUFOScript.HitByAttack(300, 200, 1));
			AttackUFOScript.DisplayDamage(DamageValues.ElectricDamage * 1.5f);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		if (collision.gameObject.name == "LightningBullet4(Clone)") {
			Destroy(collision.gameObject);
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage * 2.0f;
			StartCoroutine(AttackUFOScript.HitByAttack(400, 200, 1.5f));
			AttackUFOScript.DisplayDamage(DamageValues.ElectricDamage * 2.0f);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		else if (collision.gameObject.tag == "Earth") {
			StartCoroutine(AttackUFOScript.HitByAttack(0, 400, 2));
			AttackUFOScript.EnemyHealth -= DamageValues.EarthDamage;
            AttackUFOScript.DisplayDamage(DamageValues.EarthDamage);
        }
		else if (collision.gameObject.tag == "Speed") {
			StartCoroutine(AttackUFOScript.HitByAttack(0, 200, 0.3f));
			AttackUFOScript.EnemyHealth -= DamageValues.SpeedDamage;
            AttackUFOScript.DisplayDamage(DamageValues.SpeedDamage);
        }
		else if (collision.gameObject.name == "AnchorArms") {
			StartCoroutine(AttackUFOScript.HitByAttack(200, 300, 1));
			AttackUFOScript.EnemyHealth -= DamageValues.JackedDamage;
            AttackUFOScript.DisplayDamage(DamageValues.JackedDamage);
        }
		else if (collision.gameObject.tag == "Fire") {
			if (CanTakeDamage) {
				AttackUFOScript.StartTheInvokes("TakeFireDamage", 0.5f);
				AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
			}
        }
		else if (collision.gameObject.tag == "Ice") {
			if (AttackUFOScript.CanSpawnIceBlock == true) {
				Destroy(collision.gameObject);
				StartCoroutine(AttackUFOScript.TakeIceDamage());
			}
            AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		else if (collision.gameObject.tag == "Water") {
			if (CanTakeDamage) {
				AttackUFOScript.StartTheInvokes("TakeWaterDamage", 0.5f);
				AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);				
			}
        }
		else if (collision.gameObject.tag == "Wind") {
			if (CanTakeDamage) {
				AttackUFOScript.StartTheInvokes("TakeWindDamage", 0.5f);
				StartCoroutine(AttackUFOScript.HitByAttack(300, 600, 2));
				AttackUFOScript.SoundCall(AttackUFOScript.criticalDamage, AttackUFOScript.enemyDamage);
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Fire") {
			AttackUFOScript.StopTheInvokes("TakeFireDamage");
			StartCoroutine(DamageWait());
		}
		else if (collider.gameObject.tag == "Water") {
			AttackUFOScript.StopTheInvokes("TakeWaterDamage");
			StartCoroutine(DamageWait());
		}
		else if (collider.gameObject.tag == "Wind") {
			AttackUFOScript.StopTheInvokes("TakeWindDamage");
			StartCoroutine(DamageWait());
		}
	}

	private IEnumerator DamageWait() {
		CanTakeDamage = false;
		yield return new WaitForSeconds(0.6f);
		CanTakeDamage = true;
	}
}
