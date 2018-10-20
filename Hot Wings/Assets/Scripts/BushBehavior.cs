using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBehavior : MonoBehaviour {

	public GameObject[] PepperDrops;
	private GameObject ChosenPepper;

	// Use this for initialization
	void Start () {

		ChosenPepper = PepperDrops[Random.Range(0,6)];

		GameObject shot = Instantiate(ChosenPepper, transform.position + new Vector3 (0, 0, 0),
		Quaternion.identity) as GameObject;
		shot.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
