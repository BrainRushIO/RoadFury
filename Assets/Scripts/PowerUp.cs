using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {

	public enum PowerUpType{Girlfriend, Pet, None};
	public PowerUpType thisPowerUpType = PowerUpType.None;
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

//		if (other.gameObject.tag == "Player") {
//			if (cost != 0) {
//				GUIManager.s_instance.SpawnCost(cost);
//			}
//			if (happiness != 0) {
//				GUIManager.s_instance.SpawnHappiness(happiness);
//			}
//
//			if (burnRate != 0) {
//				GUIManager.s_instance.SpawnBurnRate(burnRate);
//
//			}
//			GUIManager.s_instance.SpawnMessage(guiMessage);
//
//			if (thisPowerUpType == PowerUpType.Girlfriend) {
//				transform.SetParent(GameObject.FindGameObjectWithTag("Wife").transform);
//				transform.localPosition = Vector3.zero;
//				print ("GF");
//			}
//			if (thisPowerUpType == PowerUpType.Pet) {
//				transform.SetParent(GameObject.FindGameObjectWithTag("Pet").transform);
//				transform.localPosition = Vector3.zero;
//				transform.localScale = Vector3.one;
//				transform.localRotation = transform.parent.rotation;
//				print ("PET");
//			}
//		}


		if (other.gameObject.tag == "roadKiller") {
		
				Destroy(gameObject);
		}
	}

}
