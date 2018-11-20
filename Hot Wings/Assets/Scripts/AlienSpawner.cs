using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour {

	public int SaucerNumber;
	public int EnemiesToSpawn;
	public GameObject MotherShip;
	public GameObject[] Aliens;
	private Animator Anim;
	private GameController Controller;
	private SpriteRenderer Sprite;

	// Use this for initialization
	void Start () {

		Anim = this.transform.GetComponent<Animator>();
		Controller = GameObject.Find("Controller").GetComponent<GameController>();
		Controller.SpawnTheEnemies += CallSpawnEnemies;
		Sprite = gameObject.GetComponent<SpriteRenderer>();

	}

	void CallSpawnEnemies () {

		if (Controller.WaveCount <= 5) {

			switch(Controller.WaveCount) {

				case 1:

					switch(SaucerNumber) {
						case 1:
							StartCoroutine(SpawnEnemy(5, Aliens[0]));
							break;
					}
					break;

				case 2:

					switch(SaucerNumber) {
						case 1:
							StartCoroutine(SpawnEnemy(5, Aliens[0]));
							break;
						case 2:
							StartCoroutine(SpawnEnemy(5, Aliens[1]));
							break;
					}
					break;
				
				case 3:

					switch(SaucerNumber) {
						case 1:
							StartCoroutine(SpawnEnemy(5, Aliens[0]));
							break;
						case 2:
							StartCoroutine(SpawnEnemy(5, Aliens[1]));
							break;
						case 3:
							StartCoroutine(SpawnEnemy(5, Aliens[2]));
							break;
					}
					break;

				case 4:

					switch(SaucerNumber) {
						case 1:
							StartCoroutine(SpawnEnemy(5, Aliens[0]));
							break;
						case 2:
							StartCoroutine(SpawnEnemy(5, Aliens[1]));
							break;
						case 3:
							StartCoroutine(SpawnEnemy(5, Aliens[2]));
							StartCoroutine(SpawnEnemy(5, Aliens[3]));
							break;
					}
					break;

				case 5:

					switch(SaucerNumber) {
						case 1:
							StartCoroutine(SpawnEnemy(5, Aliens[0]));
							SpawnAttackSaucer();
							break;
						case 2:
							StartCoroutine(SpawnEnemy(5, Aliens[1]));
							break;
						case 3:
							StartCoroutine(SpawnEnemy(5, Aliens[2]));
							StartCoroutine(SpawnEnemy(5, Aliens[3]));
							break;
					}
					break;
			}
		}
		else if (Controller.WaveCount >= 6) {

			EnemiesToSpawn = EnemiesToSpawn + 1;

			switch(SaucerNumber) {
				case 1:
					StartCoroutine(SpawnEnemy(EnemiesToSpawn, Aliens[Random.Range(0,4)]));
					if (Controller.WaveCount % 5 == 0) {
						SpawnAttackSaucer();
					}
					break;
				case 2:
					StartCoroutine(SpawnEnemy(EnemiesToSpawn, Aliens[Random.Range(0,4)]));
					break;
				case 3:
					StartCoroutine(SpawnEnemy(EnemiesToSpawn, Aliens[Random.Range(0,4)]));
					StartCoroutine(SpawnEnemy(EnemiesToSpawn, Aliens[Random.Range(0,4)]));
					break;
			}
		}
	}

	IEnumerator SpawnEnemy (int EnemiesToSpawn, GameObject ChosenAlien) {
		for (int i = 0; i < EnemiesToSpawn; i++) {
			GameObject shot = Instantiate(ChosenAlien, transform.position + new Vector3 (0, 0, 0),
			Quaternion.identity) as GameObject;
			if (Controller.WaveCount > 5) {
				shot.GetComponent<BasicEnemyControls>().EnemyHealth =
				shot.GetComponent<BasicEnemyControls>().EnemyHealth * 1.1f;
			}
			//shot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            yield return new WaitForSeconds(1);
        }
		Controller.GoSpawn = false;
		//Controller.SpawnTheEnemies = null;
		//Destroy(gameObject, 2);
	}

	void SpawnAttackSaucer () {
		GameObject Saucer = Instantiate (MotherShip, new Vector3 (0, -2.24f, 0), Quaternion.identity) as GameObject;
		//Saucer.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Midground2";
	}
}
