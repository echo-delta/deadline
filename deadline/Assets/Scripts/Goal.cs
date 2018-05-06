using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Goal : MonoBehaviour {

	private CanvasGroup popup_message;	// message display
	private bool act_button_pressed;	// true when action button is pressed
	private bool goal_done;				// true when goal already done
	private Text popup_text;			// text displayed on message display
	private LevelManager manager;

	// const strings
	private const string GOAL_ACT = "Press E to handle your work!";
	private const string GOAL_DONE = "Homework handled! Press Space to return to menu.";

	// Use this for initialization
	void Start () {

		// initialize variable
		act_button_pressed = false;
		goal_done = false;
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		popup_message = manager.popUpMsg;
		popup_text = manager.popUpTxt;

	}
	
	// Update is called once per frame
	void Update () {

		// change status on buttonpress
		if (Input.GetKey (KeyCode.E)) {
			act_button_pressed = true;
		} else {
			act_button_pressed = false;
		}

		// restart game on buttonpress
		if (Input.GetKeyDown (KeyCode.Space) && goal_done) {
			SceneManager.LoadScene ("mainmenu");
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
				Time.timeScale = 0;
				manager.allowPlayerMovement = false;
				manager.DisableInGameUI();
			}
		}

	}

	// disable popup when leaving goal
	void OnCollisionExit2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player" && !goal_done) {
			popup_message.alpha = 0;
		}

	}

}
