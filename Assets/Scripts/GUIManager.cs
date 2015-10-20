using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	public Color positive, negative, neutral;
	public GameObject OnRoadGUI, InventoryGUI;
	public GameObject message;
	public Transform costSpawn, multiplierSpawn, messageSpawn;
	public Slider happinessBar; 
	public Text burnRate, moneyText;

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
		moneyText.text = "Money: $" + GameManager.s_instance.money.ToString ();
		burnRate.text = "Cash Flow: $" + Mathf.CeilToInt (GameManager.s_instance.costOfLiving).ToString ();
		happinessBar.value -= GameManager.s_instance.attrition;
	}

	public void SpawnCost (int costValue) {
		GameObject temp = Instantiate (message);
		if (costValue > 0) {
			temp.GetComponent<Text> ().text = "$" + costValue.ToString ();
			moneyText.GetComponent<ImageFlash>().Flash(positive);
		} else {
			temp.GetComponent<Text> ().text = "$" + costValue.ToString ();
			moneyText.GetComponent<ImageFlash>().Flash(negative);

		}
		temp.transform.SetParent (costSpawn);
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);
	


	}

	public void SpawnBurnRate (float burnRateValue) {
		if (burnRateValue > 0) {
			burnRate.GetComponent<ImageFlash>().Flash(positive);
		} else {
			burnRate.GetComponent<ImageFlash>().Flash(negative);

		}
	

	}
	public void SpawnMessage (string messageString) {
		GameObject temp = Instantiate (message);
		temp.GetComponent<Text> ().text = messageString;
		temp.GetComponent<Text> ().color = neutral;
		temp.transform.SetParent (messageSpawn);
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);

	}

	public void SpawnHappiness(float happy){
		if (happy>0) {
			happinessBar.GetComponentInChildren<ImageFlash>().Flash(positive);
		} else {
			happinessBar.GetComponentInChildren<ImageFlash>().Flash(negative);
		}
	

	}

	public void SwitchFromGameToInventoryGUI() {
		OnRoadGUI.SetActive(false);
		InventoryGUI.SetActive(true);
	}

	public void SwitchFromInventoryToGameGUI() {
		OnRoadGUI.SetActive(true);
		InventoryGUI.SetActive(false);
	}


}
