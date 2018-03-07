using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform target;			// target to be tracked
	public float speed = 1;				// tracking speed
	public float distance = 10;			// camera distance to target
	public float zoom = 1;				// camera zoom

	private Camera cam;					// camera component
	private Vector3 vector_distance;	// vector made from distance

	// Use this for initialization
	void Start () {

		// initialize variables
		cam = GetComponent<Camera> ();
		vector_distance = new Vector3 (0, 0, -distance);

		// move camera to target
		transform.position = target.position + vector_distance;

	}
	
	// Update is called once per frame
	void Update () {

		// manage camera size
		cam.orthographicSize = (Screen.height ) / (100f * zoom);

		// track target
		if (target) {
			transform.position = Vector3.Lerp (transform.position, 
				target.position, speed) + vector_distance;
		}

	}
}
