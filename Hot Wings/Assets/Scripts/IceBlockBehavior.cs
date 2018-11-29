using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockBehavior : MonoBehaviour {

	Collider2D Collider;
	public int Countdown;

	// Use this for initialization
	void Start () {

		Collider = GetComponent<Collider2D>();
		StartCoroutine(IceSequence());
	}

	IEnumerator IceSequence () {
		
        yield return new WaitForSeconds(Countdown);
		Collider.enabled = false;
		Destroy(gameObject);
	}
}
