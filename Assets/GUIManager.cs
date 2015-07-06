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
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);
	


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
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);

	}
	public void SpawnMessage (string messageString) {
		GameObject temp = Instantiate (message);
		message.GetComponent<Text> ().text = messageString;
		message.GetComponent<Text> ().color = neutral;
		temp.transform.SetParent (messageSpawn);
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);

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
		temp.transform.SetParent (happinessSpawn);
		temp.transform.localScale = Vector3.one;

//		temp.transform.localPosition = transform.parent.localPosition;

	}


}
