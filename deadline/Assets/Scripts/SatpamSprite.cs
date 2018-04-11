using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatpamSprite: MonoBehaviour {

	private int x, y;					// used for movement vector
	private Animator anim;

	// Use this for initialization
	void Start () {

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

}
