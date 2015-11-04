using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
	
	public Color positive, negative, neutral;
	public GameObject message;
	public Transform costSpawn, multiplierSpawn, messageSpawn;
	public Slider happinessBar; 
	public Text burnRate, moneyText;
	public Text notificationTitle, notificationDesc, birthdayText, tutorialTitle, tutorialDesc;
	public Animator notificationAnimator, birthdayAnimator, tutorialAnimator;


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
	

	// Update is called once per frame
	void Update () {
		moneyText.text = "$" + NumberToString.Convert( PlayerStats.s_instance.money );
		burnRate.text = "$" + Mathf.CeilToInt (PlayerStats.s_instance.cashFlow).ToString () + "/year";
		happinessBar.value = PlayerStats.s_instance.happiness*100f;

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

	public void DisplayBday(int age) {
		birthdayAnimator.SetBool ("hide", false);
		birthdayText.text = "You turned " + age.ToString () + ".";
		birthdayAnimator.SetBool ("show", true);
		StartCoroutine ("CloseBdayCounter");
	}

	IEnumerator CloseBdayCounter() {
		yield return new WaitForSeconds (2f);
		CloseBday ();
	}

	public void CloseBday() {
		birthdayAnimator.SetBool( "show", false );
		birthdayAnimator.SetBool( "hide", true );
	}

	public void DisplayTutorial(string index) {
		switch (index) {
		case "0" : 
			DisplayNotification("Controls", "Touch the screen to steer", true);
			break;
		}
	}

	public void DisplayNotification( string title, string details, bool tutorial=false ) {
		if (tutorial) {
			StartCoroutine ("CloseTutorialCorout");
			tutorialAnimator.SetBool ("hide", false);
			tutorialTitle.text = title;
			tutorialDesc.text = details;
			tutorialAnimator.SetBool ("show", true);
		} else {
			GameManager.s_instance.SwitchToNotification ();
			notificationAnimator.SetBool ("hide", false);
			notificationTitle.text = title;
			notificationDesc.text = details;
			notificationAnimator.SetBool ("show", true);
		}
	}

	IEnumerator CloseTutorialCorout () {
		yield return new WaitForSeconds (3f);
		CloseTutorial ();
	}

	public void CloseTutorial() {
		tutorialAnimator.SetBool( "show", false );
		tutorialAnimator.SetBool( "hide", true );
	}

	public void CloseNotification() {
		notificationAnimator.SetBool( "show", false );
		notificationAnimator.SetBool( "hide", true );

		switch( GameManager.s_instance.previousGameState )
		{
		case GameState.Playing:
			GameManager.s_instance.SwitchToGame();
			break;
		case GameState.PitStop:
			GameManager.s_instance.SwitchToPitStop();
			break;
		}
	}

	public void SpawnHappiness(float happy){
		if (happy>0) {
			happinessBar.GetComponentInChildren<ImageFlash>().Flash(positive);
		} else {
			happinessBar.GetComponentInChildren<ImageFlash>().Flash(negative);
		}
	}
}
