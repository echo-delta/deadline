﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LockpickableDoor : MonoBehaviour {

	public Sprite open;

	private bool act_button_pressed;	// true when action button is pressed
	public bool puzzle_started;		// puzzle play status
	private Text popup_text;			// text displayed on message display
	private float player_speed;			// saved to be returned after puzzle
	private LevelManager manager;
	private InventoryManager inventory;
	private AudioSource audioSource;
	private CanvasGroup popup_message;	// message display
	private GameObject lockpick_puzzle;	// the puzzle

	// const strings
	private const string LOCKPICK = "Press E to lockpick";
	private const string LOCKPICK_HOWTO = "Press Space when the bar hit the target";
	private const string OPEN = "Press E to open door using key";

	// Use this for initialization
	void Start () {

		// initialize variables
		act_button_pressed = false;
		puzzle_started = false;

		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		inventory = GameObject.Find ("InventoryManager").GetComponent<InventoryManager> ();
		audioSource = GetComponent<AudioSource> ();

		popup_message = manager.popUpMsg;
		popup_text = popup_message.GetComponentInChildren<Text> ();

		lockpick_puzzle = manager.lockpickPuzzle;
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
					if (!audioSource.isPlaying) audioSource.Play ();
					popup_message.alpha = 0;
					OpenDoor ();
				} else {
					popup_text.text = LOCKPICK_HOWTO;
					puzzle_started = true;
					manager.allowPlayerMovement = false;
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

	public void OpenDoor() {
		GetComponent<SpriteRenderer> ().sprite = open;
		GetComponent<BoxCollider2D> ().enabled = false;
	}

}
