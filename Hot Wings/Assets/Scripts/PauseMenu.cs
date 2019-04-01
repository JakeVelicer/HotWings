using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {

    public AudioClip clickSound;
    public AudioClip pauseSound;
    public GameObject pauseMenuUI;
	public Button ResumeButton;
	public Button QuitButton;
	private AudioSource menuSounds;
	private TutorialPopups tutPopups;

    [HideInInspector] public bool GameIsPaused = false;

    // Use this for initialization
    private void Start()
    {
        menuSounds = gameObject.GetComponent<AudioSource>();
		tutPopups = GameObject.Find("Controller").GetComponent<TutorialPopups>();
    }


    // Update is called once per frame
    void Update () {

		if (Input.GetButtonDown("Pause")) {
        
			if (GameIsPaused) {
				Resume();
			}
			else {
				Pause();
			}
		}

	}
	
	public void Pause() {

		pauseMenuUI.SetActive(true);
		menuSounds.clip = pauseSound;
		menuSounds.loop = false;
		menuSounds.Play();
		Time.timeScale = 0f;
		GameIsPaused = true;
		ResumeButton.Select();
	}

	public void Resume() {

		pauseMenuUI.SetActive(false);
		menuSounds.clip = clickSound;
		menuSounds.loop = false;
		menuSounds.Play();
		if (!tutPopups.TutorialPopupPause) {
			Time.timeScale = 1f;
		}
		if (tutPopups.TutorialPopupPause) {
			tutPopups.currentButton.Select();
		}
		GameIsPaused = false;
	}

	public void LoadMenu() {

		Time.timeScale = 1f;
		pauseMenuUI.SetActive(false);
		SceneManager.LoadScene("StartMenu");
	}
}

