using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour {

	public int SaucerNumber;
	private int EnemiesToSpawn = 5;
	public GameObject MotherShip;
	public GameObject[] Aliens;
	private GameController Controller;

	// Use this for initialization
	void Start () {

		Controller = GameObject.Find("Controller").GetComponent<GameController>();
		Controller.SpawnObjects += CallSpawnEnemies;
		Controller.EnemiesLeft++;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CallSpawnEnemies () {

		if (Controller.WaveCount <= 5) {

			switch(Controller.WaveCount) {

				case 1:

					switch(SaucerNumber) {
						case 1:
							StartCoroutine(SpawnEnemy(5, Aliens[0]));
							break;
						case 2:
							Controller.EnemiesLeft--;
							Destroy(gameObject);
							break;
						case 3:
							Controller.EnemiesLeft--;
							Destroy(gameObject);
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
						case 3:
							Controller.EnemiesLeft--;
							Destroy(gameObject);
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
							break;
						case 2:
							StartCoroutine(SpawnEnemy(5, Aliens[1]));
							break;
						case 3:
							StartCoroutine(SpawnEnemy(5, Aliens[2]));
							StartCoroutine(SpawnEnemy(5, Aliens[3]));
							break;
					}
					GameObject shot = Instantiate (MotherShip, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					break;
			}
		}
		else if (Controller.WaveCount >= 6) {

			EnemiesToSpawn = EnemiesToSpawn + 1;

			switch(SaucerNumber) {
				case 1:
					StartCoroutine(SpawnEnemy(EnemiesToSpawn, Aliens[Random.Range(0,4)]));
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
			shot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            yield return new WaitForSeconds(1);
        }
		Controller.EnemiesLeft--;
		Destroy(gameObject, 2);
	}
}
