using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingSaucerPiece : MonoBehaviour {

	private Rigidbody2D Rigidbody;
	private SpriteRenderer Sprite;
	private int RightLeftCockpit;

	// Use this for initialization
	void Start () {

		Rigidbody = gameObject.GetComponent<Rigidbody2D>();
		Sprite = GetComponent<SpriteRenderer>();
		RightLeftCockpit = Random.Range(0,2);
		Explode();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Explode() {

		//if (gameObject.name.Equals("Saucer_Piece5")) {
		Rigidbody.AddForce(Vector3.up * Random.Range(190,220));
		if (RightLeftCockpit == 1) {
			Rigidbody.AddForce(Vector3.right * Random.Range(250,300));
		}
		else if (RightLeftCockpit == 0) {
			Rigidbody.AddForce(Vector3.left * Random.Range(250,300));
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		
		if (other.gameObject.tag == "Ground") {
			StartCoroutine(FadeImage());
		}
	}

	private IEnumerator FadeImage() {

		yield return new WaitForSeconds(2);
    	for (float i = 1; i >= 0; i -= Time.deltaTime) {
            Sprite.color = new Color(1,1,1,i);
            yield return null;
        }
		Destroy(gameObject, 1);
    }
}
