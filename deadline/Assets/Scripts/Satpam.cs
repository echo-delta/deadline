using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SatpamBehavior;

public class Satpam : MonoBehaviour {

	public SatpamMovement[] moveset;		// moveset
	public float distanceTreshold = 0;	// max distance delta
	public float angleTreshold = 1;			// max angle delta
	public float moveSpeed = 1;				// movement speed
	public float rotateSpeed = 50;			// rotation speed

	private int idx;						// current moveset index
	private float currentAngle;				// current angle rotation
	private Vector3 target;					// movement target
	private Quaternion angle;				// rotation angle
	private bool targetSet;					// true if target not reached
	private bool angleSet;					// true if angle not reached
	private bool onDelay;					// true if on delay

	int counter = 0;
	// Use this for initialization
	void Start () {

		// initialize
		idx = 0;
		targetSet = false;
		angleSet = false;
		onDelay = false;

	}

	// Update is called once per frame
	void Update () {

		if (!onDelay) {
			if (idx < moveset.Length) {
				switch (moveset [idx].act) {
				case "up":
					{
						if (!targetSet) {
							target = new Vector3 (transform.position.x, transform.position.y + moveset [idx].distance,
								transform.position.z);
							targetSet = true;
						} 

						if (Vector3.Distance (transform.position, target) <= distanceTreshold) {
							transform.position = target;
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
						}	
						break;
					}

				case "down":
					{
						if (!targetSet) {
							target = new Vector3 (transform.position.x, transform.position.y - moveset [idx].distance,
								transform.position.z);
							targetSet = true;
						} 

						if (Vector3.Distance (transform.position, target) <= distanceTreshold) {
							transform.position = target;
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
						}	
						break;
					}

				case "right":
					{
						if (!targetSet) {
							target = new Vector3 (transform.position.x + moveset [idx].distance, transform.position.y,
								transform.position.z);
							targetSet = true;
						} 

						if (Vector3.Distance (transform.position, target) <= distanceTreshold) {
							transform.position = target;
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
						}	
						break;
					}

				case "left":
					{
						if (!targetSet) {
							target = new Vector3 (transform.position.x - moveset [idx].distance, transform.position.y,
								transform.position.z);
							targetSet = true;
						} 

						if (Vector3.Distance (transform.position, target) <= distanceTreshold) {
							transform.position = target;
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
						}	
						break;
					}

				case "rotate":
					{
						if (!angleSet) {
							angle = Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y,
								transform.eulerAngles.z + moveset [idx].angle);
							angleSet = true;
						} 

						if (Quaternion.Angle (transform.rotation, angle) <= angleTreshold) {
							angleSet = false;
							transform.rotation = angle;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.rotation = Quaternion.RotateTowards (transform.rotation,
								angle, rotateSpeed * Time.deltaTime);
							
							counter++;
						}	
						break;
					}

				}

				if (idx == moveset.Length) {
					idx = 0;
				}
		
			}
		}

	}

	IEnumerator Wait(float sec) {
		onDelay = true;
		yield return new WaitForSeconds(sec);
		onDelay = false;
	}

}
