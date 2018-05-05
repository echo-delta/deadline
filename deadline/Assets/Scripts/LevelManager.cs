using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float fadeSpeed = 1;

	public bool playerIsHiding = false;
	public CanvasGroup popUpMsg;
	public Text popUpTxt;
	public bool allowPlayerMovement = true;
	public CanvasGroup fader;
	public GameObject lockpickPuzzle;
	public GameObject player;

	private bool fading = false;
	private bool warpSequence = false;

	// Use this for initialization
	void Start () {

		popUpTxt = popUpMsg.GetComponentInChildren<Text> ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Warp(Vector3 target) {
		allowPlayerMovement = false;
		warpSequence = true;
		while (fader.alpha < 1) {
			fader.alpha += Time.deltaTime / 2 * fadeSpeed;
			yield return null;
		}
		player.transform.position = target;
		player.GetComponent<PlayerMovement> ().ResetPlayerSprite ();
		Camera.main.transform.position = target;
		while (fader.alpha > 0) {
			fader.alpha -= Time.deltaTime / 2 * fadeSpeed;
			yield return null;
		}
		allowPlayerMovement = true;
	}

}
