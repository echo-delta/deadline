using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatpamSprite: MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Awake () {

		// initialize variables
		anim = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void setMove(int x, int y, bool iswalking) {
		anim.SetFloat ("input_x", x);
		anim.SetFloat ("input_y", y);
		anim.SetBool ("iswalking", iswalking);
	}

	void OnCollisionEnter2D (Collision2D coll) {

		if (coll.gameObject.name == "Player") {
			transform.parent.GetComponent<Satpam> ().TouchEvent ();
		}

	}

}
