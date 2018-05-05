using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour {

	public Transform target;

	private LevelManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			StartCoroutine (manager.Warp (target.transform.position));
		}
	}

}
