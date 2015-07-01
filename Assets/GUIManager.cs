using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	public Color positive, negative, neutral;
	public GameObject message;
	public Transform costSpawn, multiplierSpawn, messageSpawn, happinessSpawn;

	public static GUIManager s_instance;
	
	
	void Awake () {
		if (s_instance == null) {
			s_instance = this;
		}
		else {
			if (s_instance!=this) {
				Destroy(gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnCost (int costValue) {
		GameObject temp = Instantiate (message);
		if (costValue > 0) {
			message.GetComponent<Text> ().text = "-$" + costValue.ToString ();
			message.GetComponent<Text> ().color = negative;
		} else {
			message.GetComponent<Text> ().text = "$" + costValue.ToString ();
			message.GetComponent<Text> ().color = positive;
		}
		temp.transform.SetParent (costSpawn);
		temp.transform.localPosition = Vector3.zero;

	}

	public void SpawnMultiplier (float multiplierValue) {
		GameObject temp = Instantiate (message);
		if (multiplierValue > 1) {
			message.GetComponent<Text> ().text = "Burn Rate X" + multiplierValue.ToString ();
			message.GetComponent<Text> ().color = negative;
		} else {
			message.GetComponent<Text> ().text = "Burn Rate X" + multiplierValue.ToString ();
			message.GetComponent<Text> ().color = positive;
		}
		temp.transform.SetParent (multiplierSpawn);
		temp.transform.localPosition = Vector3.zero;
	}

	public void SpawnMessage (string messageString) {
		print ("SPAWN MESSAGE");
		GameObject temp = Instantiate (message);
		message.GetComponent<Text> ().text = messageString;
		message.GetComponent<Text> ().color = neutral;
		temp.transform.SetParent (messageSpawn);
		temp.transform.localPosition = Vector3.zero;
	}

	public void SpawnHappiness(float happy){
		GameObject temp = Instantiate (message);
		if (happy>0) {
			message.GetComponent<Text> ().text = "Happiness++";
			message.GetComponent<Text> ().color = positive;
		} else {
			message.GetComponent<Text> ().text = "Happiness--";
			message.GetComponent<Text> ().color = negative;
		}
	}


}
