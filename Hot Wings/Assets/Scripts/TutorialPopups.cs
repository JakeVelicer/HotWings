using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopups : MonoBehaviour {



private GameController Controller;
public GameObject TutorialCanvas;
public static bool GameIsPaused = false; 

public GameObject Wave1;
public GameObject Wave2;
public GameObject Wave3;
public GameObject Wave4;
public GameObject Wave5;

void Start () {
	Controller = gameObject.GetComponent<GameController>();

}

void Update (){


	if (Controller.GoSpawn) {
Debug.Log ("hello");
		if (Controller.WaveCount == 1) {
			Debug.Log ("true");
			Wave1.SetActive(true);
		}
	}

	if (GameIsPaused)
			{
				Resume();
			} else
			{
				Pause();
			}
}
		

	void Resume()
	{
		Time.timeScale = 1f;
		GameIsPaused = false;

	}
	void Pause()
	{
	Time.timeScale = 0f;
	GameIsPaused = true;
	}

}


