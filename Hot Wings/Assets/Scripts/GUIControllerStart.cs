using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIControllerStart : MonoBehaviour {

	public Button StartButton;
	public Button BackButtonHelp;
	public Button BackButtonCredits;
	public GameObject QuitButton;

	// Use this for initialization
	void Start ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer
		|| Application.platform == RuntimePlatform.Android)
		{
			QuitButton.SetActive(false);
		}
		else
		{
			StartButton.Select();
			QuitButton.SetActive(true);
		}
	}

    public void LoadByIndex(int sceneIndex)
	{
        SceneManager.LoadScene(sceneIndex);
    }

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Game Quit Called");
	}

	public void HelpClicked()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer
		&& Application.platform != RuntimePlatform.Android)
		{
			BackButtonHelp.Select();
		}
	}

	public void CreditsClicked()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer
		&& Application.platform != RuntimePlatform.Android)
		{
			BackButtonCredits.Select();
		}
	}

	public void BackClicked()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer
		&& Application.platform != RuntimePlatform.Android)
		{
			StartButton.Select();
		}
	}
}
