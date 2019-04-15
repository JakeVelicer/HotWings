using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShownInput : MonoBehaviour {

	public GameObject controlsPC;
	public GameObject controlsController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxisRaw("Horizontal") < 0){
			controlsController.SetActive(true);
			controlsPC.SetActive(false);
		}
		else if(Input.GetAxisRaw("Horizontal") > 0){
			controlsPC.SetActive(true);
			controlsController.SetActive(false);
		}
		
	}
}
