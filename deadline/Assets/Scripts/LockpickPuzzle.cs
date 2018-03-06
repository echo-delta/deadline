using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickPuzzle : MonoBehaviour {

	public float speed = 1;						// lockpick travel speed 
	public float lockpick_treshold = 1.58f;		// lockpick movement treshold
	public float win_treshold = 0.15f;			// lockpick win treshold

	private Transform lockpick_bar;			// the bar
	public Transform lockpick;				// the lockpick
	private int lockpick_direction;				// 1 = right, 0 = left
	private bool puzzle_started;				// play status
	private Vector3 lockpick_init;			// lockpick initial position

	// Use this for initialization
	void Start () {

		// initialize variables
		lockpick_bar = transform.GetChild(0);
		//lockpick = transform.GetChild (1);
		lockpick_direction = 0;
		puzzle_started = false;
		lockpick_init = new Vector3 (lockpick.position.x, 
			lockpick.position.y, lockpick.position.z);

		// play game
		StartPuzzle ();

	}
	
	// Update is called once per frame
	void Update () {

		if (puzzle_started) {
			MoveLockpick ();

			if (Input.GetKeyDown (KeyCode.Space)) {
				
			}

		}
		
	}

	// start puzzle
	void StartPuzzle () {

		transform.position.Set (transform.position.x, transform.position.y, -2);
		puzzle_started = true;

	}

	// lockpick movement
	void MoveLockpick() {
		Debug.Log (lockpick.position);
		if (lockpick_direction == 0) {
			if (lockpick.transform.position.x >
			    (lockpick_init.x - lockpick_treshold)) {
				Debug.Log ("change position");
				lockpick.position.Set(lockpick.position.x - 1, 
					lockpick.position.y, lockpick.position.z);
			} else {
				Debug.Log ("change direction");
				lockpick_direction = 1;
			}
		} else {
			if (lockpick.transform.position.x <
				(lockpick_init.x + lockpick_treshold)) {
				Debug.Log ("change position");
				lockpick.position.Set(lockpick.position.x + 1, 
					lockpick.position.y, lockpick.position.z);
			} else {
				Debug.Log ("change direction");
				lockpick_direction = 0;
			}
		}
		Debug.Log (lockpick.position);
	}

	// end puzzle
	void EndPuzzle() {
		
		puzzle_started = false;
		transform.position.Set (transform.position.x, transform.position.y, 2);

	}

}
