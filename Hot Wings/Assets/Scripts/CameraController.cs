using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private Rigidbody2D playerRb2D;
    private Renderer[] renderers;
    private Vector3 velocity;
    private float smoothTimeY;
    private float smoothTimeX;
    public bool bounds;
    public Vector3 minCameraPosition;
    public Vector3 maxCameraPosition;

    private AudioSource bgm;
    public AudioClip gameplaySong;

    private float horizontal;
    private float vertical;

    private bool isWrappingX = false;
    private bool isWrappingY = false;

    void Start () {

        player = GameObject.Find("Player");
        playerRb2D = player.GetComponent<Rigidbody2D>();
        renderers = player.GetComponentsInChildren<Renderer>();
        bgm = gameObject.GetComponent<AudioSource>();
        bgm.clip = gameplaySong;
        bgm.loop = true;
        bgm.Play();
	}
	
	void LateUpdate () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        ScreenWrapping();
    } 

	void FixedUpdate () {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bounds) {

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                                             Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                                             Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
        }
    }

    void ScreenWrapping() {

        bool isVisible = CheckRenderers();

        if(isVisible) {

            isWrappingX = false;
            isWrappingY = false;
            smoothTimeX = 0.2f;
            smoothTimeY = 0.3f;
            return; 
        }
        if(isWrappingX && isWrappingY) {
            return;
        }

        Vector3 newPostion = player.transform.position; 

        if (newPostion.x > 1 || newPostion.x < 0) {

            smoothTimeX = 0f;
            smoothTimeY = 0f;
            newPostion.x = -newPostion.x;
            isWrappingX = true;
        }
        if (newPostion.y > 1 || newPostion.y < 0) {

            smoothTimeX = 0f;
            smoothTimeY = 0f;
            newPostion.y = +newPostion.y;
            isWrappingY = true;
        } 
        player.transform.position = newPostion;

    }

    bool CheckRenderers() {

        foreach (Renderer renderer in renderers) {

            if (renderer.isVisible) {
                return true;
            }
        }
        return false;
    }

}