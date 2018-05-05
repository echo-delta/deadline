using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public bool playerIsHiding = false;
	public CanvasGroup popUpMsg;
	public Text popUpTxt;
	public bool allowPlayerMovement = true;

	// Use this for initialization
	void Start () {

		popUpTxt = popUpMsg.GetComponentInChildren<Text> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
