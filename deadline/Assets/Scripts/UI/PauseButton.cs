using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	public GameObject pauseMenu;	// the pause menu

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void displayPauseMenu() {
		AudioListener.pause = true;
		Time.timeScale = 0;
		pauseMenu.SetActive (true);

	}

}
