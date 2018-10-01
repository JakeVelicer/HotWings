using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] EnemyType;
	private GameObject hazard;
    public Vector3 spawnValues;

	public int EnemiesLeft = 0;
    public int EnemyCount;
	private bool GoSpawn; 
   
	
    //public float SpawnWait;
    //public float StartWait;
    //public float WaveWait;
	//public int WaveCount;

    void Start () {

        //StartCoroutine (SpawnWaves ());
    }

	void Update() {

		if (EnemiesLeft <= 0) {
			GoSpawn = true;
		}
		else if (EnemiesLeft > 0) {
			GoSpawn = false;
		}

    	if (GoSpawn == true) {
            for (int i = 0; i < EnemyCount; i++) {
				hazard = EnemyType[Random.Range (0, 5)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                //yield return new WaitForSeconds (SpawnWait);
            }
        }
	}
}