using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide : MonoBehaviour {

	public CanvasGroup popup_message;	// message display
	public Rigidbody2D player;

	private bool act_button_pressed;	// true when action button is pressed
	private bool hide; 					// true when player in state "hide"
	private Text popup_text;			// text displayed on message display

	private const string HIDE = "Press H to hide";
	private const string UNHIDE = "Press H to unhide";

	// Use this for initialization
	void Start () {
		hide = false;
		act_button_pressed = false;
		popup_text = popup_message.GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.H)) {
			act_button_pressed = true;
		} else {
			act_button_pressed = false;
		}

		if (hide) {
			popup_message.alpha = 1;
			popup_text.text = UNHIDE;

			if (act_button_pressed) {
				hide = false;
				popup_message.alpha = 0;
				player.constraints = RigidbodyConstraints2D.FreezeRotation;
				player.gameObject.SetActive (true);
			}
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		
		if (coll.gameObject.tag == "Player" && !hide) {
			popup_message.alpha = 1;
			popup_text.text = HIDE;

			if (act_button_pressed) {
				hide = true;
				popup_message.alpha = 0;
				player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
				player.gameObject.SetActive (false);
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			popup_message.alpha = 0;
		}

	}

}
