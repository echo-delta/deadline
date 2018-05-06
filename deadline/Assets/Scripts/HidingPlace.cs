using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour {

	public AudioClip sound;

	private LevelManager manager;
	private bool act_button_pressed;	// true when action button is pressed
	private float playerSavedSpeed;
	private Vector3 playerSavedPos;
	private bool playerHidingHere = false;
	private GameObject player;
	private AudioSource audioSource;

	private const string HIDE = "Press E to hide";
	private const string UNHIDE = "Press E to unhide";

	// Use this for initialization
	void Start () {

		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		audioSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.E)) {
			act_button_pressed = true;
		} else {
			act_button_pressed = false;
		}

		if (playerHidingHere) {
			if (act_button_pressed) {if (!audioSource.isPlaying)
				audioSource.PlayOneShot (sound);

				GetComponent<BoxCollider2D> ().enabled = true;
				manager.playerIsHiding = false;
				playerHidingHere = false;
				manager.popUpMsg.alpha = 0;
				player.transform.position = playerSavedPos;
				manager.allowPlayerMovement = true;
				act_button_pressed = false;
			}
		}

	}

	void OnCollisionStay2D (Collision2D coll) {

		if (coll.gameObject.tag == "Player" && !manager.playerIsHiding) {
			manager.popUpMsg.alpha = 1;
			manager.popUpTxt.text = HIDE;

			if (act_button_pressed) {
				if (!audioSource.isPlaying)
					audioSource.PlayOneShot (sound);
				playerHidingHere = true;
				GetComponent<BoxCollider2D> ().enabled = false;
				manager.playerIsHiding = true;
				manager.popUpMsg.alpha = 1;
				manager.popUpTxt.text = UNHIDE;
				manager.allowPlayerMovement = false;
				playerSavedPos = coll.transform.position;
				coll.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 0.1f);
				coll.gameObject.GetComponent<PlayerMovement> ().ResetPlayerSprite ();
				act_button_pressed = false;
			}
		} 
	}

	void OnCollisionExit2D(Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			
			manager.popUpMsg.alpha = 0;
		}

	}

}
