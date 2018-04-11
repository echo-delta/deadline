using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SatpamBehavior;

public class Satpam : MonoBehaviour {

	public SatpamMovement[] moveset;		// moveset
	public float distanceTreshold = 0;		// max distance delta
	public float angleTreshold = 1;			// max angle delta
	public float moveSpeed = 1;				// movement speed
	public float rotateSpeed = 50;			// rotation speed
	public float fovComplexity = 10;
	public float fovAngle = 90;
	public float fovRad = 1;
	public float lineSizeStart = 0.05f;
	public float lineSizeEnd = 0;
	public CanvasGroup popUpMsg;

	private int idx;						// current moveset index
	private float currentAngle;				// current angle rotation
	private Vector3 target;					// movement target
	private Quaternion angle;				// rotation angle
	private bool targetSet;					// true if target not reached
	private bool angleSet;					// true if angle not reached
	private bool onDelay;					// true if on delay
	private GameObject sprite;				// the sprite
	private SatpamSprite movement;			// the movement
	private Quaternion initRot;				// sprite initial rotation
	private LineRenderer sightRight, sightLeft;
	private GameObject player;
	private Text popUpTxt;

	private const string GAME_LOST = "You got caught! Press space to restart.";

	int counter = 0;
	// Use this for initialization
	void Start () {

		// initialize
		idx = 0;
		targetSet = false;
		angleSet = false;
		onDelay = false;

		sprite = transform.GetChild (2).gameObject;
		movement = sprite.GetComponent<SatpamSprite>();
		initRot = sprite.transform.rotation;
		updateSpriteRotation ();

		sightRight = transform.GetChild (0).gameObject.GetComponent<LineRenderer> ();
		sightRight.startWidth = lineSizeStart;
		sightRight.endWidth = lineSizeEnd;
		sightLeft = transform.GetChild (1).gameObject.GetComponent<LineRenderer> ();
		sightLeft.startWidth = lineSizeStart;
		sightLeft.endWidth = lineSizeEnd;

		player = GameObject.FindGameObjectWithTag ("Player");

		popUpTxt = popUpMsg.GetComponentInChildren<Text> ();
	}

	void LateUpdate() {
		DrawFOV ();
	}

	// Update is called once per frame
	void Update () {

		checkPlayer ();

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
							movement.setMove (0, 1, false);
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
							movement.setMove (0, 1, true);
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
							movement.setMove (0, 1, false);
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
							movement.setMove (0, -1, true);
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
							movement.setMove (1, 0, false);
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
							movement.setMove (1, 0, true);
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
							movement.setMove (-1, 0, false);
							targetSet = false;
							StartCoroutine (Wait (moveset [idx].delay));
							idx++;
						} else {
							transform.position = Vector3.MoveTowards (transform.position,
								target, moveSpeed * Time.deltaTime);
							movement.setMove (-1, 0, true);
						}	
						break;
					}

				case "rotate":
					{
						sprite.transform.rotation = initRot;
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

							updateSpriteRotation ();
							
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

	void updateSpriteRotation() {
		float angle = transform.eulerAngles.z % 360;
		if (angle <= 45 || angle > 315) {
			movement.setMove (0, 1, false);
		} else if (angle <= 135 && angle > 45) {
			movement.setMove (-1, 0, false);
		} else if (angle <= 225 && angle > 135) {
			movement.setMove (0, -1, false);
		} else {
			movement.setMove (1, 0, false);
		}
	}

	void DrawFOV() {
		
		float curAngle;
		RaycastHit2D hit;
		Vector3 hitPoint;
	
		curAngle = transform.eulerAngles.z - fovAngle / 2;
		hit = Physics2D.Raycast(transform.position, DirFromAngle(curAngle, true), fovRad);

		if (hit) {
			Debug.Log ("sight1 hit " + hit.collider.gameObject.name);
			hitPoint = new Vector3 (hit.point.x, hit.point.y, 0);
			sightRight.SetPosition (0, transform.position);
			sightRight.SetPosition (1, hitPoint);

		} else {
			Debug.Log ("sight1 hit nothing");
			sightRight.SetPosition (0, transform.position);
			sightRight.SetPosition (1, transform.position + DirFromAngle(curAngle,true));
		}
		curAngle = transform.eulerAngles.z + fovAngle / 2;
		hit = Physics2D.Raycast(transform.position, DirFromAngle(curAngle, true), fovRad);
		if (hit) {
			Debug.Log ("sight2 hit " + hit.collider.gameObject.name);
			hitPoint = new Vector3 (hit.point.x, hit.point.y, 0);
			sightLeft.SetPosition (0, transform.position);
			sightLeft.SetPosition (1, hitPoint);
		} else {
			Debug.Log ("sight2 hit nothing");
			sightLeft.SetPosition (0, transform.position);
			sightLeft.SetPosition (1, transform.position + DirFromAngle(curAngle,true));	
		}

	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.z;
		}
		return new Vector3(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) * fovRad,
			Mathf.Cos(angleInDegrees * Mathf.Deg2Rad) * fovRad, 0);
	}

	void checkPlayer() {
		float angle = 0;
		if (Vector3.Distance (transform.position, player.transform.position) < fovRad) {
			Vector3 direction = player.transform.position - transform.position;
			angle = Vector3.Angle (direction, transform.up);

			if (angle < fovAngle / 2) {
				Debug.Log (Vector3.Distance (transform.position, player.transform.position) + " " + angle);

				RaycastHit2D raycastHit = Physics2D.Raycast (transform.position, 
					player.transform.position - transform.position);
				if (raycastHit) {
					if (raycastHit.collider.name == "Player") {
						Debug.Log ("hit player");
						popUpMsg.alpha = 1;
						popUpTxt.text = GAME_LOST;
						Time.timeScale = 0;				
					} else {
						Debug.Log ("hit " + raycastHit.collider.name);
					}

				} else {
					Debug.Log ("hit nothing");
				}
			}

		}
	}
		
}
