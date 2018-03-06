using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Sight : MonoBehaviour {

	public CanvasGroup popup_message;	// message display

	private bool game_lost;				// true when goal already done
	private Text popup_text;			// text displayed on message display

	// const strings
	private const string GAME_LOST = "You got caught!";


	// Use this for initialization
	void Start () {

		// initialize variable
		popup_text = popup_message.GetComponentInChildren<Text> ();
		game_lost = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// lose condition if touched by player
	void OnTriggerEnter2D (Collider2D coll) {
	
		// display popup on player contact and receive key press
		if (coll.gameObject.tag == "Player" && !game_lost) {
			popup_message.alpha = 1;
			popup_text.text = GAME_LOST;
			game_lost = true;
		}

	}
}
