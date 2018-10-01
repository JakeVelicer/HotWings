using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject hazard;
    public Vector3 spawnValues;
	public GameObject[] EnemyTypes;

	public int WaveCount = 0;
    public int hazardCount;
    public float SpawnWait;
    public float StartWait;
    public float WaveWait;
	private bool GoSpawn;

    void Start () {

        StartCoroutine (SpawnWaves ());
    }

	void Update() {

		if (WaveCount <= 0) {
			GoSpawn = true;
		}
	}

    IEnumerator SpawnWaves () {

        yield return new WaitForSeconds (StartWait);

        while (GoSpawn == true) {
            for (int i = 0; i < hazardCount; i++) {
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
				WaveCount += WaveCount;
                yield return new WaitForSeconds (SpawnWait);
            }
            yield return GoSpawn = false;
        }
    }
}