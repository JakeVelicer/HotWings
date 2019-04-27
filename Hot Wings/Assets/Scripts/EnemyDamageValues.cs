using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageValues : MonoBehaviour {

	private BasicEnemyControls Enemy;

	[HideInInspector] public float FireDamage;
	[HideInInspector] public float WaterDamage;
	[HideInInspector] public float ElectricDamage;
	[HideInInspector] public float SpeedDamage;
	[HideInInspector] public float IceDamage;
	[HideInInspector] public float EarthDamage;
	[HideInInspector] public float WindDamage;
	[HideInInspector] public float JackedDamage;

	// Use this for initialization
	void Start () {

		if (gameObject.name == "Mothership(Clone)" || gameObject.name == "Mothership") {

			FireDamage = 5;
			WaterDamage = 5;
			ElectricDamage = 10;
			SpeedDamage = 25;
			IceDamage = 10;
			EarthDamage = 100;
			WindDamage = 30;
			JackedDamage = 30;
		}
		else {

			Enemy = gameObject.GetComponent<BasicEnemyControls> ();

			// Roly Poly Alien
			if (Enemy.AlienType == 1) {
				FireDamage = 15;
				WaterDamage = 8;
				ElectricDamage = 7;
				SpeedDamage = 15;
				IceDamage = 7;
				EarthDamage = 100;
				WindDamage = 3;
				JackedDamage = 50;
			}
			// Blob Alien
			if (Enemy.AlienType == 2) {
				FireDamage = 10;
				WaterDamage = 20;
				ElectricDamage = 15;
				SpeedDamage = 15;
				IceDamage = 5;
				EarthDamage = 100;
				WindDamage = 5;
				JackedDamage = 50;
			}
			// Beefy Alien
			if (Enemy.AlienType == 3) {
				FireDamage = 10;
				WaterDamage = 5;
				ElectricDamage = 10;
				SpeedDamage = 25;
				IceDamage = 18;
				EarthDamage = 100;
				WindDamage = 5;
				JackedDamage = 50;
			}
			// Armored Alien
			if (Enemy.AlienType == 4) {
				FireDamage = 8;
				WaterDamage = 4;
				ElectricDamage = 25;
				SpeedDamage = 30;
				IceDamage = 8;
				EarthDamage = 100;
				WindDamage = 10;
				JackedDamage = 50;
			}
		}
	}
}