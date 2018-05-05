using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDoor : MonoBehaviour {
	public Sprite open;

	private bool act_button_pressed;	// true when action button is pressed
	private Text popup_text;			// text displayed on message display
	private LevelManager manager;
	private AudioSource audioSource;
	private CanvasGroup popup_message;	// message display

	// const strings
	private const string OPEN = "Press E to open";

	// Use this for initialization
	void Start () {

		// initialize variables
		act_button_pressed = false;

		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		audioSource = GetComponent<AudioSource> ();

		popup_message = manager.popUpMsg;
		popup_text = popup_message.GetComponentInChildren<Text> ();

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.E)) {
			act_button_pressed = true;
		} else {
			act_button_pressed = false;
		}

	}

	// When touched by player 
	void OnCollisionStay2D (Collision2D coll) {

		// display popup on player contact and receive key press
		if (coll.gameObject.tag == "Player") {
			popup_message.alpha = 1;
			popup_text.text = OPEN;

			if (act_button_pressed) {
				if (!audioSource.isPlaying) audioSource.Play ();
				popup_message.alpha = 0;
				OpenDoor ();
				
			}
		}

	}

	// disable popup when leaving goal
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			popup_message.alpha = 0;
		}

	}

	public void OpenDoor() {
		GetComponent<SpriteRenderer> ().sprite = open;
		GetComponent<BoxCollider2D> ().enabled = false;
	}

}
