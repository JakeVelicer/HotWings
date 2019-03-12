using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSaucerCollision : MonoBehaviour {

	private AttackUFOBehavior AttackUFOScript;
	private EnemyDamageValues DamageValues;

	void Start() {

		AttackUFOScript = GetComponentInParent<AttackUFOBehavior>();
		DamageValues = GetComponentInParent<EnemyDamageValues>();
		
	}

    void OnTriggerEnter2D(Collider2D collision) {

			// Takes damage from burst attacks
		if (collision.gameObject.name == "LightningBullet(Clone)") {
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage;
			StartCoroutine(AttackUFOScript.HitByAttack(100, 200, 0.3f));
			AttackUFOScript.TakeDamage(DamageValues.ElectricDamage);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);

        }
		if (collision.gameObject.name == "LightningBullet2(Clone)") {
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage * 1.2f;
			StartCoroutine(AttackUFOScript.HitByAttack(200, 200, 0.5f));
			AttackUFOScript.TakeDamage(DamageValues.ElectricDamage * 1.2f);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		if (collision.gameObject.name == "LightningBullet3(Clone)") {
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage * 1.5f;
			StartCoroutine(AttackUFOScript.HitByAttack(300, 200, 1));
			AttackUFOScript.TakeDamage(DamageValues.ElectricDamage * 1.5f);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		if (collision.gameObject.name == "LightningBullet4(Clone)") {
			AttackUFOScript.EnemyHealth -= DamageValues.ElectricDamage * 2.0f;
			StartCoroutine(AttackUFOScript.HitByAttack(400, 200, 1.5f));
			AttackUFOScript.TakeDamage(DamageValues.ElectricDamage * 2.0f);
			AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		else if (collision.gameObject.tag == "Earth") {
			StartCoroutine(AttackUFOScript.HitByAttack(0, 400, 2));
			AttackUFOScript.EnemyHealth -= DamageValues.EarthDamage;
            AttackUFOScript.TakeDamage(DamageValues.EarthDamage);
        }
		else if (collision.gameObject.tag == "Speed") {
			StartCoroutine(AttackUFOScript.HitByAttack(0, 200, 0.3f));
			AttackUFOScript.EnemyHealth -= DamageValues.SpeedDamage;
            AttackUFOScript.TakeDamage(DamageValues.SpeedDamage);
        }
		else if (collision.gameObject.name == "AnchorArms") {
			StartCoroutine(AttackUFOScript.HitByAttack(200, 300, 1));
			AttackUFOScript.EnemyHealth -= DamageValues.JackedDamage;
            AttackUFOScript.TakeDamage(DamageValues.JackedDamage);
        }
			// Takes damage from stream attacks
		else if (collision.gameObject.tag == "Fire") {
			AttackUFOScript.StartTheInvokes("TakeFireDamage", 0.5f);
            AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		else if (collision.gameObject.tag == "Ice") {
			if (AttackUFOScript.CanSpawnIceBlock) {
				AttackUFOScript.CanSpawnIceBlock = false;
				StartCoroutine(AttackUFOScript.TakeIceDamage());
			}
            AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		else if (collision.gameObject.tag == "Water") {
			AttackUFOScript.StartTheInvokes("TakeWaterDamage", 0.5f);
            AttackUFOScript.SoundCall(AttackUFOScript.hitDamage, AttackUFOScript.enemyDamage);
        }
		else if (collision.gameObject.tag == "Wind") {
			AttackUFOScript.StartTheInvokes("TakeWindDamage", 0.5f);
			StartCoroutine(AttackUFOScript.HitByAttack(300, 600, 2));
            AttackUFOScript.SoundCall(AttackUFOScript.criticalDamage, AttackUFOScript.enemyDamage);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == "Fire") {
			AttackUFOScript.StopTheInvokes("TakeFireDamage");
		}
		else if (collider.gameObject.tag == "Water") {
			AttackUFOScript.StopTheInvokes("TakeWaterDamage");
		}
		else if (collider.gameObject.tag == "Wind") {
			AttackUFOScript.StopTheInvokes("TakeWindDamage");
		}
	}
}
