using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] SaucerObject;
    private GameObject hazard;
    private Text waveDisplay;
    //public Vector3 spawnValues;
    public Text scoreDisplay;
    public int EnemiesLeft = 0;
    //public int SaucerCount;
    [HideInInspector] public bool GoSpawn;
    public int WaveCount;
    public Text inventoryDisplay;
    public int score;
    public GameObject player;
    public System.Action SpawnObjects;

    public static System.Action<int> OnWaveIncremented;

    void Awake()
    {

        Time.timeScale = 1;
    }

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        waveDisplay = GameObject.Find("Wave").GetComponent<Text>();
        inventoryDisplay = GameObject.Find("Inventory").GetComponent<Text>();

        player.GetComponent<playerControls>().pepperB = " ";
        inventoryDisplay.text = "Inventory:" + player.GetComponent<playerControls>().pepperA + "\n" + "\t \t \t \t" + player.GetComponent<playerControls>().pepperB;

        score = 0;
        scoreDisplay.text = "Score: " + score;

        WaveCount = 0;
        //StartCoroutine (SpawnWaves ());
    }

    void Update()
    {
        inventoryDisplay.text = "Inventory:" + player.GetComponent<playerControls>().pepperA + "\n" + "\t \t \t \t" + player.GetComponent<playerControls>().pepperB;
        waveDisplay.text = "Wave: " + WaveCount;
        scoreDisplay.text = "Score: " + score;
        if (EnemiesLeft <= 0)
        {
            GoSpawn = true;
            StartCoroutine("Count");
        }
        else if (EnemiesLeft > 0)
        {
            GoSpawn = false;
        }
        SpawnEnemies();
        //Debug.Log ("Wave Number: " + WaveCount);
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(0.1f);
        WaveCount += 1;
        if (OnWaveIncremented != null) {
            OnWaveIncremented(WaveCount);
        }
        if (SpawnObjects != null) {
            SpawnObjects();
        }
    }

    void SpawnEnemies()
    {
        if (GoSpawn == true)
        {
            /*
            for (int i = 0; i < SaucerCount; i++)
            {
                hazard = SaucerObject[Random.Range(0, 3)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
            }
            */

            Instantiate(SaucerObject[0], new Vector3 (-8.3f, 5, 0), Quaternion.identity);
            Instantiate(SaucerObject[1], new Vector3 (-44.6f, 5, 0), Quaternion.identity);
            Instantiate(SaucerObject[2], new Vector3 (27.8f, 5, 0), Quaternion.identity);
        }
    }
}
