using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeathMenu : MonoBehaviour
{

	public static bool Death = false;
	public GameObject DeathMenuUI;
	public GameObject PauseButton;
	public Button MenuContinue;
	public Text waveText;
	public Text scoreText;
	private PlayerControls PlayerHealth;
	private GameController Controller;
	private bool onMobile;

	
	void Start ()
	{
		PlayerHealth = GameObject.Find("Player").GetComponent<PlayerControls> ();
		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			onMobile = true;
		}
		else
		{
			onMobile = false;
		}
	}

	void Update ()
	{
		if (PlayerHealth.health <= 0)
		{
			Dead();
		}
	}

	void OnEnable ()
	{
		Controller = GameObject.Find("Controller").GetComponent<GameController>();
	}

	// Use this for initialization
	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("StartMenu");
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}

	void Dead()
	{
        if (!Death)
        { 

            PlayerHealth.playerSounds.clip = PlayerHealth.playerDeath;
            PlayerHealth.playerSounds.loop = false;
            PlayerHealth.playerSounds.Play();
            Death = true;
        }
        StartCoroutine(EndGame());

	}
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.5f);
        DeathMenuUI.SetActive(true);
		PauseButton.SetActive(false);
        Time.timeScale = 0f;
		if (onMobile)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
		else
		{
			MenuContinue.Select();
		}
		waveText.text = "" + Controller.WaveCount;
        scoreText.text = "" + Controller.score;
    }

}