using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActivateButton : MonoBehaviour {

	private TutorialPopups tutPopups;

	private void Awake() {

		tutPopups = GameObject.Find("Controller").GetComponent<TutorialPopups>();
		
	}
	
	private void OnEnable() {

		transform.GetChild(0).GetComponent<Button>().Select();
		
	}
}
