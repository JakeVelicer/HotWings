using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] EnemyType;
	private GameObject hazard;
    private Text WaveDisplay;
    public Vector3 spawnValues;

	public int EnemiesLeft = 0;
    public int EnemyCount;
	private bool GoSpawn;
    private int WaveCount;
	
    //public float SpawnWait;
    //public float StartWait;
    //public float WaveWait;

    void Awake() {

        Time.timeScale = 1;
    }

    void Start () {

        WaveCount = 0;
        WaveDisplay = GameObject.Find ("Wave").GetComponent<Text> ();
        //StartCoroutine (SpawnWaves ());
    }

	void Update() {

        WaveDisplay.text = "Wave: " + WaveCount;

		if (EnemiesLeft <= 0) {
			GoSpawn = true;
            StartCoroutine ("Count");
		}
		else if (EnemiesLeft > 0) {
			GoSpawn = false;
		}
        SpawnEnemies();
        //Debug.Log ("Wave Number: " + WaveCount);
	}

    IEnumerator Count () {
		yield return new WaitForSeconds(0.1f);
		WaveCount += 1;
	}

    void SpawnEnemies () {
        if (GoSpawn == true) {
            for (int i = 0; i < EnemyCount; i++) {
				hazard = EnemyType[Random.Range (0, 5)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
            }
        }
    }
}
