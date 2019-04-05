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
    private bool InitialPickup;
    private bool PepperWait;
    private GameObject player;
    private GameObject[] PeppersInScene;
    public System.Action SpawnTheEnemies;
    public System.Action SpawnPeppers;
    [HideInInspector] public bool GoSpawn;
    public static System.Action<int> OnWaveIncremented;
    public Image healthBar;
    private playerControls currentHealth;

    void Awake() {

        Time.timeScale = 1;
        OnWaveIncremented = null;
    }

    void Start() {

        player = GameObject.Find("Player");
        scoreDisplay = GameObject.Find("Score").GetComponent<Text>();
        waveDisplay = GameObject.Find("Wave").GetComponent<Text>();
        currentHealth = GameObject.Find("Player").GetComponent<playerControls>();

        WaveCount = 0;
        score = 0;
        scoreDisplay.text = "Score: " + score;
        
    }

    void Update() {

        waveDisplay.text = "Wave: " + WaveCount;
        scoreDisplay.text = "Score:" + "\n" + score;

        // Updates the length of the health bar
        var healthBarRectTransform = healthBar.transform as RectTransform;
        healthBarRectTransform.sizeDelta = new Vector2(currentHealth.health/1.11f, healthBarRectTransform.sizeDelta.y);

        // Stops first wave from starting before they pickup a pepper
        if (player.GetComponent<playerControls>().pepperIndexA == 1 ||
        player.GetComponent<playerControls>().pepperIndexA == 2) {
            InitialPickup = true;
        }

        // New Wave
        if (EnemiesLeft <= 0 && !GoSpawn && InitialPickup) {
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

        yield return new WaitForSeconds(2f);
        GameObject.Find("Wave").GetComponent<Animator>().SetTrigger("Zoom");
        yield return new WaitForSeconds(1.5f);
        WaveCount += 1;

        if (WaveCount != 1) {
            if (OnWaveIncremented != null) {
                OnWaveIncremented(WaveCount);
            }
            if (SpawnPeppers != null) {
                SpawnPeppers();
            }
        }
        StartCoroutine(SpawnerAnims());
        yield return new WaitForSeconds(7f);
        if (SpawnTheEnemies != null) {
            SpawnTheEnemies();
        }
        
    }

    // Plays UFO swooping down animations
    private IEnumerator SpawnerAnims() {

        yield return new WaitForSeconds(5.5f);

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
