using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    private AudioSource menuSounds;
    public AudioClip clickSound;
    public AudioClip pauseSound;

    public GameObject pauseMenuUI;
    // Use this for initialization
    private void Start()
    {
        menuSounds = gameObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
        
			if (GameIsPaused)
			{
				Resume();
			} else
			{
				Pause();
			
			}
			 menuSounds.clip = pauseSound;
            menuSounds.loop = false;
            menuSounds.Play();
			}

		}

	void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;

	}
	void Pause()
	{
pauseMenuUI.SetActive(true);
Time.timeScale = 0f;
GameIsPaused = true;
	}
	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("StartMenu");
	pauseMenuUI.SetActive(false);

	}
}

