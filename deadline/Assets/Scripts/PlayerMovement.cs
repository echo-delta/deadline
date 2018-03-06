using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 1;				// speed of movement

	private Rigidbody2D rBody;			// rigidbody for physics
	private int x, y;					// used for movement vector
	private Vector2 movement_vector;	// used for determining movement

	// Use this for initialization
	void Start () {

		// initialize variables
		rBody = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {

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
			rBody.MovePosition (rBody.position + 
				movement_vector * Time.deltaTime * speed);
		}

		// reset x and y
		x = 0;
		y = 0;

	}

	// stops movement when seen by satpam
	void OnTriggerEnter2D (Collider2D coll) {

		// display popup on player contact and receive key press
		if (coll.gameObject.tag == "Sight") {
			speed = 0;
		}

	}

}
