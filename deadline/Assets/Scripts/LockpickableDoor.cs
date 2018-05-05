using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LockpickableDoor : MonoBehaviour {

	public CanvasGroup popup_message;	// message display
	public GameObject lockpick_puzzle;	// the puzzle

	private bool act_button_pressed;	// true when action button is pressed
	private bool puzzle_started;		// puzzle play status
	private Text popup_text;			// text displayed on message display
	private float player_speed;			// saved to be returned after puzzle
	private InventoryManager inventory;

	// const strings
	private const string LOCKPICK = "Press E to lockpick";
	private const string OPEN = "Press E to open door using key";

	// Use this for initialization
	void Start () {

		// initialize variables
		act_button_pressed = false;
		puzzle_started = false;
		popup_text = popup_message.GetComponentInChildren<Text> ();
		inventory = GameObject.Find ("InventoryManager").GetComponent<InventoryManager> ();

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
		if (coll.gameObject.tag == "Player" && !puzzle_started) {
			popup_message.alpha = 1;
			if (inventory.HaveItem (1)) {
				popup_text.text = OPEN;
			} else {
				popup_text.text = LOCKPICK;
			}

			if (act_button_pressed) {
				if (inventory.HaveItem (1)) {
					popup_message.alpha = 0;
					Destroy (gameObject);
				} else {
					popup_message.alpha = 0;
					puzzle_started = true;
					lockpick_puzzle.GetComponent<LockpickPuzzle> ().StartPuzzle (gameObject, coll.gameObject);
				}
			}
		}
	
	}

	// disable popup when leaving goal
	void OnCollisionExit2D(Collision2D coll) {
		puzzle_started = false;
		if (coll.gameObject.tag == "Player") {
			popup_message.alpha = 0;
		}

	}

}
