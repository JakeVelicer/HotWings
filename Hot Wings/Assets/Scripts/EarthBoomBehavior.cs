using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoomBehavior : MonoBehaviour {
	
	private Animator EarthGrow;
	private SpriteRenderer Sprite;

	// Use this for initialization
	void Start () {

		EarthGrow = gameObject.GetComponent<Animator>();
		Sprite = gameObject.GetComponent<SpriteRenderer>();
		//StartCoroutine(FadeImage());
		
	}
	
	// Update is called once per frame
	void Update () {

		if (EarthGrow.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !EarthGrow.IsInTransition(0)) {
			Destroy (gameObject);
		}
		
	}

	IEnumerator FadeImage() {
    	for (float i = 1f; i >= 0; i -= Time.deltaTime) {
            Sprite.color = new Color(1f,1f,1f,i);
            yield return null;
        }
    }
}
