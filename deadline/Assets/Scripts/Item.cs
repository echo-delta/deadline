using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int itemId;

	private InventoryManager manager;

	// Use this for initialization
	void Start () {

		manager = GameObject.Find ("InventoryManager").GetComponent<InventoryManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D coll) {

		if (coll.gameObject.tag == "Player") {
			manager.AddItem (itemId, GetComponent<SpriteRenderer>().sprite);
			Destroy (gameObject);
		}
	}

}
