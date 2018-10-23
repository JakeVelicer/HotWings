using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBehavior : MonoBehaviour {

	public int BushType;
	public GameObject[] PepperDrops;
	private GameObject ChosenPepper;
	private GameController Controller;

	// Use this for initialization
	void Start () {

		Controller = GameObject.Find("Controller").GetComponent<GameController>();
		Controller.SpawnPeppers += CallSpawn;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CallSpawn () {

		if (Controller.WaveCount <= 5) {

			switch(Controller.WaveCount) {

				case 1:

					switch(BushType) {
					case 1:
						ChosenPepper = PepperDrops[0];
						SpawnPepper();
						break;
					case 2:
						ChosenPepper = PepperDrops[1];
						SpawnPepper();
						break;
					}
					break;

				case 2:

					switch(BushType) {
					case 1:
						ChosenPepper = PepperDrops[0];
						SpawnPepper();
						break;
					case 2:
						ChosenPepper = PepperDrops[1];
						SpawnPepper();
						break;
					case 3:
						ChosenPepper = PepperDrops[2];
						SpawnPepper();
						break;
					}
					break;
				
				case 3:

					switch(BushType) {
					case 1:
						ChosenPepper = PepperDrops[0];
						SpawnPepper();
						break;
					case 2:
						ChosenPepper = PepperDrops[1];
						SpawnPepper();
						break;
					case 3:
						ChosenPepper = PepperDrops[2];
						SpawnPepper();
						break;
					case 4:
						ChosenPepper = PepperDrops[3];
						SpawnPepper();
						break;
					}
					break;

				case 4:

					switch(BushType) {
					case 1:
						ChosenPepper = PepperDrops[0];
						SpawnPepper();
						break;
					case 2:
						ChosenPepper = PepperDrops[1];
						SpawnPepper();
						break;
					case 3:
						ChosenPepper = PepperDrops[2];
						SpawnPepper();
						break;
					case 4:
						ChosenPepper = PepperDrops[3];
						SpawnPepper();
						break;
					case 5:
						ChosenPepper = PepperDrops[4];
						SpawnPepper();
						break;
					}
					break;

				case 5:

					switch(BushType) {
					case 1:
						ChosenPepper = PepperDrops[0];
						SpawnPepper();
						break;
					case 2:
						ChosenPepper = PepperDrops[1];
						SpawnPepper();
						break;
					case 3:
						ChosenPepper = PepperDrops[2];
						SpawnPepper();
						break;
					case 4:
						ChosenPepper = PepperDrops[3];
						SpawnPepper();
						break;
					case 5:
						ChosenPepper = PepperDrops[4];
						SpawnPepper();
						break;
					case 6:
						ChosenPepper = PepperDrops[5];
						SpawnPepper();
						break;
					}
					break;
			}
		}
		else if (Controller.WaveCount >= 6) {

			switch(BushType) {

				case 1:
					ChosenPepper = PepperDrops[Random.Range(0,6)];
					SpawnPepper();
					break;
				case 2:
					ChosenPepper = PepperDrops[Random.Range(0,6)];
					SpawnPepper();
					break;
				case 3:
					ChosenPepper = PepperDrops[Random.Range(0,6)];
					SpawnPepper();
					break;
				case 4:
					ChosenPepper = PepperDrops[Random.Range(0,6)];
					SpawnPepper();
					break;
				case 5:
					ChosenPepper = PepperDrops[Random.Range(0,6)];
					SpawnPepper();
					break;
				case 6:
					ChosenPepper = PepperDrops[Random.Range(0,6)];
					SpawnPepper();
					break;
				case 7:
					ChosenPepper = PepperDrops[Random.Range(6,9)];
					SpawnPepper();
					break;
			}
		}
	}

	void SpawnPepper () {
		GameObject shot = Instantiate(ChosenPepper, transform.position + new Vector3 (0, 0, 0),
		Quaternion.identity) as GameObject;
		shot.GetComponent<SpriteRenderer>().sortingLayerName = "Midground";
	}
}
