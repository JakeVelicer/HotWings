using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIControllerStart : MonoBehaviour {

	public Button StartButton;
	public Button BackButtonHelp;
	public Button BackButtonCredits;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene (sceneIndex);
    }

	public void QuitGame() {
		Application.Quit();
		Debug.Log("Game Quit Called");
	}

	public void HelpClicked() {
		BackButtonHelp.Select();
	}

	public void CreditsClicked() {
		BackButtonCredits.Select();
	}

	public void BackClicked() {
		StartButton.Select();
	}
}
