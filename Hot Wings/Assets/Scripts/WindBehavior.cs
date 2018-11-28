using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WindBehavior : MonoBehaviour {

	private Animator WindGrow;
	private SpriteRenderer Sprite;
	public bool GoRight;

	// Use this for initialization
	void Start () {

		WindGrow = gameObject.GetComponent<Animator>();
		Sprite = gameObject.GetComponent<SpriteRenderer>();

		if (GoRight == true) {
			WindGrow.Play("WindBlowRight");
		}
		else if (GoRight == false) {
			WindGrow.Play("WindBlowLeft");
		}
		StartCoroutine(FadeImage());
		
	}
	
	// Update is called once per frame
	void Update () {

		if (WindGrow.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !WindGrow.IsInTransition(0)) {
			GetComponent<Collider2D>().enabled = false;
			Destroy (gameObject, 0.1f);
		}
		
	}

	IEnumerator FadeImage() {
    	for (float i = 1; i >= 0; i -= Time.deltaTime) {
            Sprite.color = new Color(1f,1f,1f,i);
            yield return null;
        }
    }

}
