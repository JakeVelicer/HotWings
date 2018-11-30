using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private Text scoreDisplay;
    private Text waveDisplay;
    private Text healthDisplay;
    public int EnemiesLeft = 0;
    public int WaveCount;
    public int score;
    private GameObject player;
    private GameObject[] PeppersInScene;
    public System.Action SpawnTheEnemies;
    public System.Action SpawnPeppers;
    [HideInInspector] public bool GoSpawn;
    [HideInInspector] public bool PepperWait;
    public static System.Action<int> OnWaveIncremented;
    public Image healthBar;
    
    //private Text inventoryDisplay;

    void Awake() {

        Time.timeScale = 1;
    }

    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        waveDisplay = GameObject.Find("Wave").GetComponent<Text>();
        //inventoryDisplay = GameObject.Find("Inventory").GetComponent<Text>();
        //player.GetComponent<playerControls>().pepperB = " ";
        //inventoryDisplay.text = "Inventory:" + "\n"+ player.GetComponent<playerControls>().pepperA + "\n" + player.GetComponent<playerControls>().pepperB;

        score = 0;
        scoreDisplay.text = "Score: " + score;

        WaveCount = 0;
        
    }

    void Update() {

        /*
        inventoryDisplay.text = "Inventory:" + "\n" + player.GetComponent<playerControls>().
            pepperA + "\n" + player.GetComponent<playerControls>().pepperB;
        */
        waveDisplay.text = "Wave: " + WaveCount;
        scoreDisplay.text = "Score: " + score;

        // New Wave
        if (EnemiesLeft <= 0 && !GoSpawn) {
            GoSpawn = true;
            StartCoroutine(NextWave());
        }

        // Respawns Peppers
        PeppersInScene = GameObject.FindGameObjectsWithTag("Pepper");
		if (PeppersInScene.Length == 0 && !PepperWait) {
            PepperWait = true;
            StartCoroutine(PepperRespawn());
		}

    }

    // Sets all elements needed for the next wave to happen
    IEnumerator NextWave() {

        if (WaveCount != 0) {
            yield return new WaitForSeconds(4f);
        }
        WaveCount += 1;
        if (OnWaveIncremented != null) {
            OnWaveIncremented(WaveCount);
        }
        if (SpawnPeppers != null) {
            SpawnPeppers();
        }
        StartCoroutine(SpawnerAnims());
        yield return new WaitForSeconds(6f);
        if (SpawnTheEnemies != null) {
            SpawnTheEnemies();
        }
        
    }

    // Plays UFO swooping down animations
    private IEnumerator SpawnerAnims() {

        yield return new WaitForSeconds(4.5f);

        GameObject.Find("AlienSpawner1").GetComponent<Animator>().Play("SwoopSpawning1");
        if (WaveCount >=2) {
            GameObject.Find("AlienSpawner2").GetComponent<Animator>().Play("SwoopSpawning2");
        }
        if (WaveCount >=3) {
            GameObject.Find("AlienSpawner3").GetComponent<Animator>().Play("SwoopSpawning3");
        }

    }

    // Waits a second and respawns peppers
    private IEnumerator PepperRespawn() {

        yield return new WaitForSeconds(1);
        if (SpawnPeppers != null) {
            SpawnPeppers();
        }
        PepperWait = false;
    }

    private void HealthUpdate()
    {
       
    }

}
