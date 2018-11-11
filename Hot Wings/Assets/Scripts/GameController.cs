using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] SaucerObject;
    private GameObject hazard;
    private Text waveDisplay;
    public Text scoreDisplay;
    public int EnemiesLeft = 0;
    [HideInInspector] public bool GoSpawn;
    public int WaveCount;
    public Text inventoryDisplay;
    public int score;
    public GameObject player;
    public System.Action SpawnTheEnemies;
    public System.Action SpawnPeppers;
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
        inventoryDisplay.text = "Inventory:" + "\n"+ player.GetComponent<playerControls>().pepperA + "\n" + player.GetComponent<playerControls>().pepperB;

        score = 0;
        scoreDisplay.text = "Score: " + score;

        WaveCount = 0;
        
    }

    void Update()
    {
        inventoryDisplay.text = "Inventory:" + "\n" + player.GetComponent<playerControls>().pepperA + "\n" + player.GetComponent<playerControls>().pepperB;

        waveDisplay.text = "Wave: " + WaveCount;
        scoreDisplay.text = "Score: " + score;
        if (EnemiesLeft <= 0 && !GoSpawn)
        {
            GoSpawn = true;
            StartCoroutine("Count");
            SpawnEnemies();
        }
        else if (EnemiesLeft > 0)
        {
            GoSpawn = false;
        }

    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(0.1f);
        WaveCount += 1;
        if (OnWaveIncremented != null) {
            OnWaveIncremented(WaveCount);
        }
        if (SpawnTheEnemies != null) {
            SpawnTheEnemies();
        }
        if (SpawnPeppers != null) {
            SpawnPeppers();
        }
    }

    void SpawnEnemies()
    {
        
        GameObject Saucer;
        Saucer = Instantiate(SaucerObject[0], new Vector3 (-8.3f, 5, 0), Quaternion.identity) as GameObject;
        Saucer.GetComponent<SpriteRenderer>().sortingLayerName = "Midground1";
        Saucer = Instantiate(SaucerObject[1], new Vector3 (-44.6f, 5, 0), Quaternion.identity) as GameObject;
        Saucer.GetComponent<SpriteRenderer>().sortingLayerName = "Midground1";
        Saucer = Instantiate(SaucerObject[2], new Vector3 (27.8f, 5, 0), Quaternion.identity) as GameObject;
        Saucer.GetComponent<SpriteRenderer>().sortingLayerName = "Midground1";
    
    }
}
