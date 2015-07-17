using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {

	public Text thisText;
	public string nameOfObj, guiMessage;
	public float happiness, burnRate;
	public int cost;
	// Use this for initialization
	void Start () {
		thisText.text = nameOfObj;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "Player") {
			if (cost != 0) {
				GUIManager.s_instance.SpawnCost(cost);
			}
			if (happiness != 0) {
				GUIManager.s_instance.SpawnHappiness(happiness);
			}

			if (burnRate != 0) {
				GUIManager.s_instance.SpawnBurnRate(burnRate);

			}
			GUIManager.s_instance.SpawnMessage(guiMessage);
		}


		if (other.gameObject.tag == "roadKiller") {
			Destroy(gameObject);
		}
	}

}
