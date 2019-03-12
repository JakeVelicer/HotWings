using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    private playerControls m_playerControls;
    private GameController Controller;

    private int m_maxHealth;

    private Dictionary<int, int> m_alienDeathDictionary = new Dictionary<int, int>();
        private Dictionary<int, int> m_pepperUsedDictionary = new Dictionary<int, int>();

    public int enemiesKilled;

  

    private bool m_didNotTakeDamageDuringTutorial;
    public bool DidNotTakeDamageDuringTutorial
    {
        get
        {
            return m_didNotTakeDamageDuringTutorial;
        }
    }


    private void OnEnable()
    {
        GameController.OnWaveIncremented += OnWaveIncremented;
        BasicEnemyControls.OnEnemyDeath += OnEnemyDeath;
        AttackUFOBehavior.OnEnemyDeath += OnEnemyDeath;
       
    }

    private void OnDisable()
    {
        GameController.OnWaveIncremented -= OnWaveIncremented;
        BasicEnemyControls.OnEnemyDeath -= OnEnemyDeath;
        AttackUFOBehavior.OnEnemyDeath -= OnEnemyDeath;
    }

    private void Start()
    {
        m_maxHealth = m_playerControls.health;
        Controller = GameObject.Find("Controller").GetComponent<GameController>();
        
        
    }

    public void OnWaveIncremented(int waveCount)
    {
        //if we're past 5 waves (the # of tutorial waves) && our health is the max health
        //then award the achievement
        if (waveCount >= 5 && m_maxHealth == m_playerControls.health)
        
        {
            //achievement unlocked!
            m_didNotTakeDamageDuringTutorial = true;
        }
        
        
       
    }

    public void OnEnemyDeath(int alienNumber)
    {
        if (!m_alienDeathDictionary.ContainsKey(alienNumber))
        {
            m_alienDeathDictionary.Add(alienNumber, 0);
        }
        m_alienDeathDictionary[alienNumber] += 1;
        Debug.Log("We have killed " + m_alienDeathDictionary[alienNumber] + " of Alien #" + alienNumber);
        Debug.Log("We have killed the most of Alien #" + GetMostKilledAlienNumber());
    }
     public void OnPepperUse(int pepperNumber)
    {
        if (!m_pepperUsedDictionary.ContainsKey(pepperNumber))
        {
            m_pepperUsedDictionary.Add(pepperNumber, 0);
        }
        m_pepperUsedDictionary[pepperNumber] += 1;
        Debug.Log("We have used " + m_pepperUsedDictionary[pepperNumber] + " of pepper #" + pepperNumber);
        Debug.Log("We have used the most of Pepper #" + PeppersUsed());
    }

    public int GetMostKilledAlienNumber()
    {
        int mostKilledAlien = -1;
        int mostDeaths = -1;
        foreach (int key in m_alienDeathDictionary.Keys)
        {
            int alienNumber = key;
            int deaths = m_alienDeathDictionary[alienNumber];
            if (deaths > mostDeaths)
            {
                mostKilledAlien = alienNumber;
                mostDeaths = deaths;
            }
        }
        return mostKilledAlien;
    }
     public int PeppersUsed()
    {
        int MostUsed = -1;
        int a_Used = -1;
        foreach (int key in m_pepperUsedDictionary.Keys)
        {
            int pepperNumber = key;
            int used = m_pepperUsedDictionary[pepperNumber];
            if (used > a_Used)
            {
                MostUsed = pepperNumber;
                a_Used = used;
            }
        }
        return MostUsed;
    }



}