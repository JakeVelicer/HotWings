using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopups : MonoBehaviour
{
    private GameController Controller;
    public GameObject[] Waves;
    private PauseMenu pauseMenu;
    public GameObject ScreenTint;
    [HideInInspector] public bool TutorialPopupPause;
    [HideInInspector] public Button currentButton;
    private int currentTutPop = 0;

    void Start() {

        pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenu>();
        currentButton = Waves[currentTutPop].transform.GetChild(0).GetComponent<Button>();
        Waves[0].SetActive(true);
        PauseForTut();
    }

    void Update() {

        if (TutorialPopupPause && !pauseMenu.GameIsPaused) {
            currentButton.Select();
        }
    }

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
        //Debug.Log("Tutorial Popup knows that the wave was incremented to " + waveCount);
        if (waveCount <= 5)
        {
            Waves[currentTutPop].SetActive(true);
            PauseForTut();
        }
    }

    public void TutorialDone()
    {
        Time.timeScale = 1f;
        ScreenTint.SetActive(false);
        TutorialPopupPause = false;
    }

    public void PauseForTut()
    {
        Time.timeScale = 0f;
        ScreenTint.SetActive(true);
        TutorialPopupPause = true;
    }

    public void NextPopUp()
    {
        currentTutPop ++;
        currentButton = Waves[currentTutPop].transform.GetChild(0).GetComponent<Button>();
    }

}


