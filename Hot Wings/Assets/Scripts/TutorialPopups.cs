using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopups : MonoBehaviour
{
    private GameController Controller;
    public GameObject TutorialCanvas;

    public GameObject[] Waves;

    void OnEnable()
    {
        GameController.OnWaveIncremented += OnWaveIncremented;
    }

    void OnDisable()
    {
        GameController.OnWaveIncremented -= OnWaveIncremented;
    }

    private void OnWaveIncremented(int waveCount)
    {
        Debug.Log("Tutorial Popup knows that the wave was incremented to " + waveCount);
        
        if (waveCount <= Waves.Length)
        {
            Waves[waveCount - 1].SetActive(true);
            Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

}


