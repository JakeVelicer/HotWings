using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialVideo : MonoBehaviour {

	public RawImage videoImage;
	public VideoPlayer videoPlayer;
	private bool isPlaying;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.activeSelf && isPlaying == false) {
			StartCoroutine(StartVideo());
		}
	}

	private IEnumerator StartVideo() {

		videoPlayer.Prepare();
		while(!videoPlayer.isPrepared) {
			yield return new WaitForSeconds(0.5f);
			break;
		}
		videoImage.texture = videoPlayer.texture;
		videoPlayer.Play();
		isPlaying = true;
	}
}
