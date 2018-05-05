using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Sight : MonoBehaviour {

	public CanvasGroup popup_message;	// message display

	private bool game_lost;				// true when goal already done
	private bool act_button_pressed;	// true if space pressed
	private Text popup_text;			// text displayed on message display
	private LevelManager manager;

	// const strings
	private const string GAME_LOST = "You got caught! Press space to restart.";


	// Use this for initialization
	void Start () {

		// initialize variable
		popup_text = popup_message.GetComponentInChildren<Text> ();
		game_lost = false;
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();

	}
	
	// Update is called once per frame
	void Update () {

		// change on buttonpress
		if (Input.GetKeyDown (KeyCode.Space) && game_lost) {
			Time.timeScale = 1;
			SceneManager.LoadScene ("prototype_scene");
		} 

	}

	// lose condition if touched by player
	void OnTriggerEnter2D (Collider2D coll) {
	
		// display popup on player contact and receive key press
		if (coll.gameObject.tag == "Player" && !game_lost && !manager.playerIsHiding) {
			Debug.Log ("got player");
			Debug.DrawRay (transform.parent.transform.position, coll.transform.position - transform.parent.transform.position, Color.red, 1000, true);
			GetComponent<EdgeCollider2D> ().enabled = false;
			RaycastHit2D raycastHit = Physics2D.Raycast (transform.parent.transform.position, coll.transform.position - transform.parent.transform.position);
			if (raycastHit) {
				if (raycastHit.collider.name == "Player") {
					Debug.Log ("hit player");
					popup_message.alpha = 1;
					popup_text.text = GAME_LOST;
					game_lost = true;
					Time.timeScale = 0;				
				} else {
					Debug.Log ("hit " + raycastHit.collider.name);
					GetComponent<EdgeCollider2D> ().enabled = true;
				}

			} else {
				Debug.Log ("hit nothing");
			}
				
		} else {
			Debug.Log ("hit " + coll.gameObject.name);
		}

	}
}
