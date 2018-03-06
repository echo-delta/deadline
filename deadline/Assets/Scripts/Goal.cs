using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Goal : MonoBehaviour {

	public CanvasGroup popup_message;	// message display

	private bool act_button_pressed;	// true when action button is pressed
	private bool goal_done;				// true when goal already done
	private Text popup_text;			// text displayed on message display

	// const strings
	private const string GOAL_ACT = "Press E to handle your work!";
	private const string GOAL_DONE = "Homework handled!";

	// Use this for initialization
	void Start () {

		// initialize variable
		act_button_pressed = false;
		popup_text = popup_message.GetComponentInChildren<Text> ();
		goal_done = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.E)) {
			act_button_pressed = true;
		} else {
			act_button_pressed = false;
		}

	}

	// start goal mechanism when player reached here
	void OnCollisionStay2D(Collision2D coll) {

		// display popup on player contact and receive key press
		if (coll.gameObject.tag == "Player" && !goal_done) {
			popup_message.alpha = 1;
			popup_text.text = GOAL_ACT;

			if (act_button_pressed) {
				popup_text.text = GOAL_DONE;
				goal_done = true;
				coll.gameObject.GetComponent<PlayerMovement> ().speed = 0;
				StartCoroutine (DisablePopUp (2));
			}
		}

	}

	// disable popup when leaving goal
	void OnCollisionExit2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player" && !goal_done) {
			popup_message.alpha = 0;
		}

	}

	// diasble popup function
	IEnumerator DisablePopUp(int sec) {
		yield return new WaitForSeconds (sec);
		popup_message.alpha = 0;
	}

}
