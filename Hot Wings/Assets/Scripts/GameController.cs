using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] EnemyType;
	private GameObject hazard;
    public Vector3 spawnValues;

	public int EnemyCount = 0;
    public int HazardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;
	//public int WaveCount;
	private bool GoSpawn;

    void Start () {

        StartCoroutine (SpawnWaves ());
    }

	void Update() {

		if (EnemyCount <= 0) {
			GoSpawn = true;
		}
	}

    IEnumerator SpawnWaves () {

        yield return new WaitForSeconds (StartWait);

    	while (GoSpawn == true) {
            for (int i = 0; i < HazardCount; i++) {
				hazard = EnemyType[Random.Range (0, 5)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (SpawnWait);
            }
            yield return GoSpawn = false;
        }
    }
}