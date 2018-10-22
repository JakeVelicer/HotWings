using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour {

	public int SaucerNumber;
	public int EnemiesToSpawn;
	public GameObject[] Aliens;
	private GameObject ChosenAlien;
	private GameController Controller;

	// Use this for initialization
	void Start () {

		Controller = GameObject.Find("Controller").GetComponent<GameController>();
		Controller.SpawnObjects += CallCoroutine;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CallCoroutine () {
		StartCoroutine(BetweenSpawn());
	}

	IEnumerator BetweenSpawn () {

		for (int i = 0; i < EnemiesToSpawn; i++) {
            CallSpawnEnemies();
            yield return new WaitForSeconds(1f);
        }
		Destroy(gameObject, 2);
	}

	void CallSpawnEnemies () {

		if (Controller.WaveCount <= 5) {

			switch(Controller.WaveCount) {

				case 1:

					switch(SaucerNumber) {
					case 1:
						ChosenAlien = Aliens[0];
						SpawnEnemy();
						break;
					case 2:
						ChosenAlien = Aliens[1];
						SpawnEnemy();
						break;
					case 3:
						ChosenAlien = Aliens[2];
						SpawnEnemy();
						break;
					}
					break;

				case 2:

					switch(SaucerNumber) {
					case 1:
						ChosenAlien = Aliens[0];
						SpawnEnemy();
						break;
					case 2:
						ChosenAlien = Aliens[1];
						SpawnEnemy();
						break;
					case 3:
						ChosenAlien = Aliens[2];
						SpawnEnemy();
						break;
					}
					break;
				
				case 3:

					switch(SaucerNumber) {
					case 1:
						ChosenAlien = Aliens[0];
						SpawnEnemy();
						break;
					case 2:
						ChosenAlien = Aliens[1];
						SpawnEnemy();
						break;
					case 3:
						ChosenAlien = Aliens[2];
						SpawnEnemy();
						break;
					}
					break;

				case 4:

					switch(SaucerNumber) {
					case 1:
						ChosenAlien = Aliens[0];
						SpawnEnemy();
						break;
					case 2:
						ChosenAlien = Aliens[1];
						SpawnEnemy();
						break;
					case 3:
						ChosenAlien = Aliens[2];
						SpawnEnemy();
						break;
					}
					break;

				case 5:

					switch(SaucerNumber) {
					case 1:
						ChosenAlien = Aliens[0];
						SpawnEnemy();
						break;
					case 2:
						ChosenAlien = Aliens[1];
						SpawnEnemy();
						break;
					case 3:
						ChosenAlien = Aliens[2];
						SpawnEnemy();
						break;
					}
					break;
			}
		}
		else if (Controller.WaveCount >= 6) {

			switch(SaucerNumber) {

				case 1:
					ChosenAlien = Aliens[Random.Range(0,5)];
					SpawnEnemy();
					break;
				case 2:
					ChosenAlien = Aliens[Random.Range(0,5)];
					SpawnEnemy();
					break;
				case 3:
					ChosenAlien = Aliens[Random.Range(0,5)];
					SpawnEnemy();
					break;
			}
		}
	}

	void SpawnEnemy () {
		GameObject shot = Instantiate(ChosenAlien, transform.position + new Vector3 (0, 0, 0),
		Quaternion.identity) as GameObject;
		shot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
	}
}
