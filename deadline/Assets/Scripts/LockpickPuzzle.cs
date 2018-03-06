using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickPuzzle : MonoBehaviour {

	public float speed = 1;						// lockpick travel speed 
	public float lockpick_treshold = 1.58f;		// lockpick movement treshold
	public float win_treshold = 0.15f;			// lockpick win treshold
	public Transform lockpick;					// the lockpick

	private Transform lockpick_bar;				// the bar
	private int lockpick_direction;				// 1 = right, 0 = left
	private bool puzzle_started;				// play status
	private Vector3 lockpick_init;				// lockpick initial position
	private GameObject source_door;				// door that activate this puzzle
	private GameObject source_player;			// player object
	private float temp_player_speed;				// saved player speed

	// Use this for initialization
	void Start () {

		// initialize variables
		lockpick_bar = transform.GetChild(0);
		//lockpick = transform.GetChild (1);
		lockpick_direction = 0;
		puzzle_started = false;

		// play game
		//StartPuzzle ();

	}
	
	// Update is called once per frame
	void Update () {

		if (puzzle_started) {
			MoveLockpick ();

			if (Input.GetKeyDown (KeyCode.Space)) {
				EndPuzzle ();
			}

		}
		
	}

	// start puzzle
	public void StartPuzzle (GameObject door, GameObject player) {

		source_door = door;
		source_player = player;

		temp_player_speed = source_player.GetComponent<PlayerMovement> ().speed;
		player.GetComponent<PlayerMovement> ().speed = 0;
		transform.position = new Vector3 (source_player.transform.position.x, 
			source_player.transform.position.y, -2);
		puzzle_started = true;
		lockpick_init = new Vector3 (lockpick.position.x, 
			lockpick.position.y, lockpick.position.z);

	}

	// lockpick movement
	void MoveLockpick() {
		if (lockpick_direction == 0) {
			if (lockpick.transform.position.x >
			    (lockpick_init.x - lockpick_treshold)) {
				lockpick.Translate (Vector3.left * Time.deltaTime * speed);
			} else {
				lockpick_direction = 1;
			}
		} else {
			if (lockpick.transform.position.x <
				(lockpick_init.x + lockpick_treshold)) {
				lockpick.Translate (Vector3.right * Time.deltaTime * speed);
			} else {
				lockpick_direction = 0;
			}
		}
	}

	// end puzzle
	void EndPuzzle() {
		
		puzzle_started = false;
		if (lockpick.transform.position.x > (lockpick_init.x - win_treshold) &&
		    lockpick.transform.position.x < (lockpick_init.x + win_treshold)) {
			source_door.SetActive (false);
		}

		source_player.GetComponent<PlayerMovement> ().speed = temp_player_speed;
		lockpick.position = lockpick_init;
		transform.position = new Vector3 (source_player.transform.position.x, 
			source_player.transform.position.y, 2);

	}

}
