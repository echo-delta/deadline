using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void resumeGame() {

		Time.timeScale = 1;
		gameObject.transform.parent.gameObject.SetActive (false);

	}

}
