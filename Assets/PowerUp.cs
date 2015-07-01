using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {

	public Text thisText;
	public string nameOfObj, guiMessage;
	public float cost, happiness, multiplier;

	// Use this for initialization
	void Start () {
		thisText.text = nameOfObj;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "Player") {
			print ("HIT POWERUP");
			if (cost != 0) {
				GUIManager.s_instance.SpawnCost((int)cost);
			}
			if (happiness != 0) {
				GUIManager.s_instance.SpawnHappiness(happiness);
			}

			if (multiplier != 0) {
				GUIManager.s_instance.SpawnMultiplier(multiplier);

			}
			GUIManager.s_instance.SpawnMessage(guiMessage);
		}


		if (other.gameObject.tag == "roadKiller") {
			Destroy(gameObject);
		}
	}

}
