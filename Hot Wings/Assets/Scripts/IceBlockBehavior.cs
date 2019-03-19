using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockBehavior : MonoBehaviour {

	public int Countdown;

	// Use this for initialization
	void Start () {

		StartCoroutine(IceSequence());
	}

	IEnumerator IceSequence () {
		
        yield return new WaitForSeconds(Countdown);
		Destroy(gameObject);
	}
}
