using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satpam : MonoBehaviour {

	public float rotation_speed = 1; 		// Satpam rotation speed

	private const float multiplier = 10;	// Initial multiplier

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.forward * Time.deltaTime * rotation_speed * multiplier);

	}
}
