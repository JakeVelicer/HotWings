using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WindBehavior : MonoBehaviour {

	private Animator WindGrow;
	private playerControls Player;
	private SpriteRenderer Sprite;

	// Use this for initialization
	void Start () {

		Player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControls>();
		WindGrow = gameObject.GetComponent<Animator>();
		Sprite = gameObject.GetComponent<SpriteRenderer>();

		if (Player.facingRight == true) {
			WindGrow.Play("WindBlowRight");
		}
		else if (Player.facingRight == false) {
			WindGrow.Play("WindBlowLeft");
		}
		StartCoroutine(FadeImage());
		
	}
	
	// Update is called once per frame
	void Update () {

		if (WindGrow.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !WindGrow.IsInTransition(0)) {
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
