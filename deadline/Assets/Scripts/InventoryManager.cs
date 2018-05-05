using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public GameObject inventory;
	public Sprite emptySprite;
	public AudioClip grab;

	private int itemCount = 0;
	private Image[] items;
	private int[] itemIds;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		items = new Image[4];
		itemIds = new int[4];
		for (int i = 0; i < 4; i++) {
			items[i] = inventory.transform.GetChild (i).GetComponent<Image>();
			itemIds [i] = -1;
		}
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.L)) {
			RemoveItem (0);
		}

	}

	public void AddItem(int itemId, Sprite itemSprite) {

		if (itemCount < 4) {
			audioSource.PlayOneShot (grab);
			items [itemCount].sprite = itemSprite;
			itemIds [itemCount] = itemId;
			itemCount++;
		}


	}

	public void RemoveItem(int itemId) {
		int i = 0;
		while (i < itemCount && itemIds [i] != itemId) {
			i++;
		}

		if (itemIds [i] == itemId) {
			for (int j = i; j < itemCount; j++) {
				
				if (j == itemCount - 1) {
					items [j].sprite = emptySprite;
					itemIds [j] = -1;
				} else {
					itemIds [j] = itemIds [j + 1];
					items [j].sprite = items [j + 1].sprite;
				}

			}
			itemCount--;
		}

	}

	public bool HaveItem(int itemId) {
		int i = 0;
		while (i < itemCount && itemIds [i] != itemId) {
			i++;
		}
		if (itemIds [i] == itemId) {
			return true;
		} else {
			return false;
		}
	}

}
