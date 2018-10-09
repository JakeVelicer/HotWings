using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] EnemyType;
	private GameObject hazard;
    private Text WaveDisplay;
    public Vector3 spawnValues;
    public Text scoreDisplay;
    public int EnemiesLeft = 0;
    public int EnemyCount;
	private bool GoSpawn;
    private int WaveCount;
    public Text inventoryDisplay; 
    public int score;
    public GameObject player;

    void Awake() {

        Time.timeScale = 1;
    }

    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        WaveDisplay = GameObject.Find("Wave").GetComponent<Text>();
        inventoryDisplay = GameObject.Find("Inventory").GetComponent<Text>();
        
        player.GetComponent<playerControls>().pepperB = " ";
        inventoryDisplay.text = "Inventory:" + player.GetComponent<playerControls>().pepperA + "\n" + "\t \t \t \t" + player.GetComponent<playerControls>().pepperB;
        
        score = 0;
        scoreDisplay.text = "Score: " + score;
        
        WaveCount = 0;
        //StartCoroutine (SpawnWaves ());
    }

	void Update() {
        inventoryDisplay.text = "Inventory:" + player.GetComponent<playerControls>().pepperA + "\n" + "\t \t \t \t" + player.GetComponent<playerControls>().pepperB;
        WaveDisplay.text = "Wave: " + WaveCount;
        scoreDisplay.text = "Score: " + score;
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
				hazard = EnemyType[Random.Range (0, 4)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
            }
        }
    }
}
