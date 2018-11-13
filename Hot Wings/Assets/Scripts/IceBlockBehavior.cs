using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockBehavior : MonoBehaviour {

	Collider2D Collider;
	public int Countdown;

	// Use this for initialization
	void Start () {

		StartCoroutine(IceSequence());
		Collider = GetComponent<Collider2D>();
	}

	IEnumerator IceSequence () {
		
        yield return new WaitForSeconds(Countdown);
		Collider.enabled = false;
        yield return new WaitForSeconds(0.1f);
		Destroy(gameObject);
	}
}
