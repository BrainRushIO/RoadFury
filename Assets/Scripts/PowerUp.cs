﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {


	public Text thisText;
	public string nameOfObj, guiMessage;
	public float happiness, burnRate;
	public int cost;
	public AudioSource playOnCollision;

	void OnTriggerEnter (Collider other) {
		if (playOnCollision != null) {
			playOnCollision.Play();
		}
		if (other.gameObject.tag == "Player") {
			if (cost != 0) {
				GUIManager.s_instance.SpawnCost (cost);
			}
			if (happiness != 0) {
				GUIManager.s_instance.SpawnHappiness (happiness);
			}

			if (burnRate != 0) {
				GUIManager.s_instance.SpawnBurnRate (burnRate);

			}
			GUIManager.s_instance.SpawnMessage (guiMessage);
		}
	}

}
