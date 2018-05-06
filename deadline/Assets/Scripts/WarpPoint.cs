using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WarpPoint : MonoBehaviour {

	public Transform target;
	public string nextLevel;
	public GameObject satpam;
	public GameObject nextSatpam;

	private LevelManager manager;
	private bool endgame = false;

	private const string ENDGAME = "All level finished! Press space to return to main menu.";

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (endgame) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				SceneManager.LoadScene ("mainmenu");
				endgame = false;
			}
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			if (target) {
				StartCoroutine (manager.Warp (target.transform.position));
				satpam.SetActive (false);
				nextSatpam.SetActive (true);
			} else if (nextLevel.Length != 0) {
				SceneManager.LoadScene (nextLevel);
			}else {
				endgame = true;
				Time.timeScale = 0;
				AudioListener.pause = true;
				manager.allowPlayerMovement = false;
				manager.popUpMsg.alpha = 1;
				manager.popUpTxt.text = ENDGAME; 
				manager.DisableInGameUI ();
			}
		}
	}

}
