using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 1;				// speed of movement

	private Rigidbody2D rBody;			// rigidbody for physics
	private int x, y;					// used for movement vector
	private Vector2 movement_vector;	// used for determining movement
	private Animator anim;
	private LevelManager manager;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {

		// initialize variables
		rBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		anim.SetFloat ("input_y", -1);
		audioSource = GetComponent<AudioSource> ();
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (manager.allowPlayerMovement) {

			// get movement input
			if (Input.GetKey (KeyCode.W)) {
				y = 1;
			}
			if (Input.GetKey (KeyCode.S)) {
				y = -1;
			}
			if (Input.GetKey (KeyCode.A)) {
				x = -1;
			}
			if (Input.GetKey (KeyCode.D)) {
				x = 1;
			}

			// update movement vector
			movement_vector = new Vector2 (x, y);

			// update position
			if (movement_vector != Vector2.zero) {
				if (!audioSource.isPlaying)
					audioSource.Play ();
				anim.SetBool ("iswalking", true);
				anim.SetFloat ("input_x", movement_vector.x);
				anim.SetFloat ("input_y", movement_vector.y);
				rBody.MovePosition (rBody.position +
				movement_vector * Time.deltaTime * speed);
			} else {
				if (audioSource.isPlaying)
					audioSource.Stop ();
				anim.SetBool ("iswalking", false);
			}

			// reset x and y
			x = 0;
			y = 0;
		} else {
			if (audioSource.isPlaying)
				audioSource.Stop ();
		}

	}

	// stops movement when seen by satpam
	void OnTriggerEnter2D (Collider2D coll) {

		// display popup on player contact and receive key press


	}

	public void ResetPlayerSprite() {
		anim.SetBool ("iswalking", false);
		anim.SetFloat ("input_x", 0);
		anim.SetFloat ("input_y", -1);
	}

}
