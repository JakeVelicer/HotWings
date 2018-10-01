using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {
	public static bool Death = false; 

public GameObject DeathMenuUI;

	private playerControls PlayerHealth;
	
	void Start () {

		PlayerHealth = GameObject.FindWithTag ("Player").GetComponent<playerControls> ();

	}

	void Update () {

		if (PlayerHealth.health <= 0) {
			Pause();
		}
		
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
		Debug.Log("Gamequit");
	}
	void Pause()
	{
		DeathMenuUI.SetActive(true);
		Time.timeScale = 0f;
		Death = true;
	}
	
}