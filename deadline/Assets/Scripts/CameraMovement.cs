using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform target;			// target to be tracked
	public float speed = 5;				// tracking speed
	public float distance = 20;			// camera distance to target
	//public float zoom = 1;				// camera zoom

	//private Camera cam;					// camera component
	private float defZ;						// default z value

	// Use this for initialization
	void Start () {

		// initialize variables
		//cam = GetComponent<Camera> ();

		// move camera to target
		transform.position = target.position + new Vector3 (0, 0, -distance);
		defZ = transform.position.z;

		// manage camera size
		//float currentAspect = (float) Screen.width / (float) Screen.height;

		//cam.orthographicSize = (Screen.height )/ currentAspect / (100f * zoom);

	}
	
	// Update is called once per frame
	void Update () {

		// track target
		if (target) {
			transform.position = Vector3.Lerp (transform.position, 
				target.position, speed * Time.deltaTime);
			transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				defZ);
		}

	}
}
