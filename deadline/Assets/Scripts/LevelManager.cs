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
	public CanvasGroup inGameMenu;
	public CanvasGroup inGameUI;

	//private bool warpSequence = false;

	// Use this for initialization
	void Start () {

		popUpTxt = popUpMsg.GetComponentInChildren<Text> ();
		fader.interactable = false;
		fader.gameObject.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

	}

	public IEnumerator Warp(Vector3 target) {
		allowPlayerMovement = false;
		//warpSequence = true;
		fader.gameObject.SetActive (true);
		while (fader.alpha < 1) {
			fader.alpha += Time.deltaTime / 2 * fadeSpeed;
			yield return null;
		}
		player.transform.position = new Vector3(target.x, target.y, player.transform.position.z);
		player.GetComponent<PlayerMovement> ().ResetPlayerSprite ();
		Camera.main.transform.position = target;
		while (fader.alpha > 0) {
			fader.alpha -= Time.deltaTime / 2 * fadeSpeed;
			yield return null;
		}
		fader.gameObject.SetActive (false);
		allowPlayerMovement = true;
	}

	public void DisableInGameUI() {
		inGameUI.interactable = false;
		inGameUI.alpha = 0;
	}

	public void EnableInGameUI() {
		inGameUI.interactable = true;
		inGameUI.alpha = 1;
	}

}
