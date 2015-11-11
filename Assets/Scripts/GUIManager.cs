using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class handles all changes to the GUI - except for the pitstopgui which has its own class.
/// </summary>
public class GUIManager : MonoBehaviour {
	
	public Color positive, negative, neutral;
	public GameObject message;
	public Transform[] notificationSpawnAreas;
	public Transform pauseMenuTexts;
	public Slider happinessBar; 
	public Text incomeText, moneyText;
	public Text notificationTitle, notificationDesc, birthdayText, tutorialTitle, tutorialDesc;
	public Animator notificationAnimator, birthdayAnimator, tutorialAnimator, pauseMenuAnimator;

	public GameObject camera1;
	public GameObject pitStopGUI;
	public GameObject inGameGUI;
	public GameObject MainMenuGUI;
	public GameObject faderObj;
	public Text debug;

	private int currentSpawnAreaIndex = 0; // used in conjunction with notificationSpawnAreas above.

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

	void Start() {
		if( notificationSpawnAreas.Length < 1 )
			Debug.LogError( "Game Manager doesn't have reference to spawn areas for notifications." );
	}
	
	void Update () {
		moneyText.text = "$" + NumberToString.Convert( PlayerStats.s_instance.money );
		incomeText.text = "$" + Mathf.CeilToInt (PlayerStats.s_instance.income).ToString () + "/year";
		happinessBar.value = PlayerStats.s_instance.happiness*100f;
	}

	void LateUpdate() {
		currentSpawnAreaIndex = 0;
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
		temp.transform.SetParent ( notificationSpawnAreas[GetNextAvailableNotificationArea()] );
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);
	}

	public void SpawnBurnRate (float incomeValue) {
		GameObject temp = Instantiate (message);
		temp.GetComponent<Text> ().text = "Income $" + incomeValue;

		temp.transform.SetParent ( notificationSpawnAreas[GetNextAvailableNotificationArea()] );
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);
		if (incomeValue > 0) {
			temp.GetComponent<Text> ().color = positive;
			incomeText.GetComponent<ImageFlash>().Flash(positive);
		} else {
			temp.GetComponent<Text> ().color = negative;
			incomeText.GetComponent<ImageFlash>().Flash(negative);
		}
	}
	public void SpawnMessage (string messageString) {
		GameObject temp = Instantiate (message);
		temp.GetComponent<Text> ().text = messageString;
		temp.GetComponent<Text> ().color = neutral;
		temp.transform.SetParent ( notificationSpawnAreas[GetNextAvailableNotificationArea()] );
		temp.transform.localScale = Vector3.one;
		temp.transform.localPosition = new Vector3(0,0,0);

	}

	public void DisplayPauseMenu() {
			pauseMenuAnimator.SetTrigger ("pitstop");
			pauseMenuTexts.transform.GetChild (0).GetComponent<Text> ().text = "Age: " + PlayerStats.s_instance.age;
			pauseMenuTexts.transform.GetChild (1).GetComponent<Text> ().text = "Total Money: $" + NumberToString.Convert (PlayerStats.s_instance.money);
			pauseMenuTexts.transform.GetChild (2).GetComponent<Text> ().text = "Income: $" + NumberToString.Convert (PlayerStats.s_instance.income);
			pauseMenuTexts.transform.GetChild (3).GetComponent<Text> ().text = "Happiness: " + Mathf.CeilToInt(PlayerStats.s_instance.happiness*100) + "/100";
	}

	public void ClosePauseMenu() {
		if (pauseMenuAnimator.GetCurrentAnimatorStateInfo (0).IsName("PitStop_Onscreen")) {
			pauseMenuAnimator.SetTrigger ("pitstop");
		}
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


	public void DisplayNotification( string title, string details, bool tutorial=false ) {
		if (tutorial) {
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

	public void CloseTutorial() {
		tutorialAnimator.SetBool( "show", false );
		tutorialAnimator.SetBool( "hide", true );
		GameManager.s_instance.SwitchToGame ();
	}

	public void SpawnHappiness(float happy){
		if (happy>0) {
			happinessBar.GetComponentInChildren<ImageFlash>().Flash(positive);
		} else {
			happinessBar.GetComponentInChildren<ImageFlash>().Flash(negative);
		}
	}

	public void PitstopFlashEnter() {
		StartCoroutine("FlashIn");
	}
	
	public void PitstopFlashExit() {
		StartCoroutine("FlashOut");
	}
	
	private IEnumerator FlashIn() {
		print ("FLASH IN");
		faderObj.GetComponent<Fader>().StartFadeIn();
		yield return new WaitForSeconds(1f);
		// UI
		inGameGUI.GetComponent<Animator> ().SetTrigger("pitstop");
		pitStopGUI.GetComponent<Animator>().SetTrigger("pitstop");
		//switch camera
		camera1.GetComponent<Camera> ().enabled = false;
		GameObject.FindGameObjectWithTag ("Camera2").GetComponent<Camera>().enabled = true;
		faderObj.GetComponent<Fader>().StartFadeOut();
	}
	private IEnumerator FlashOut() {
		faderObj.GetComponent<Fader>().StartFadeIn();
		yield return new WaitForSeconds(1f);
		inGameGUI.GetComponent<Animator> ().SetTrigger ("pitstop");
		pitStopGUI.GetComponent<Animator> ().SetTrigger ("pitstop");
		//switch camera
		GameObject.FindGameObjectWithTag ("Camera2").GetComponent<Camera>().enabled = false;
		camera1.GetComponent<Camera> ().enabled = true;
		faderObj.GetComponent<Fader>().StartFadeOut();
	}

	public void DisplayTutorial(string index) {
		switch (index) {
		case "0" : 
			DisplayNotification("Controls", "Touch the screen to steer", true);
			break;
		case "1" : 
			DisplayNotification("Power-Ups", "Power-Ups affect your happiness and money", true);
			break;
		case "2" : 
			DisplayNotification("Age", "Objective: Retire at 65", true);
			break;
		case "3" : 
			DisplayNotification("Happiness", "If you run out of happiness - GAME OVER", true);
			break;
		case "4" : 
			DisplayNotification("Cash Flow", "You are losing money, get a job!", true);
			break;
		case "5" : 
			DisplayNotification("Obstacles", "Be careful, some Power-Ups hurt you", true);
			break;
		case "6" :
			DisplayNotification("Pit Stop", "Goto a Pit-Stop to manage your Financial Assets", true);
			break;
		case "7" :
			DisplayNotification("Life Decision", "Left: Goto College \n Right: Join the Workforce", true);
			break;
		case "8" :
			DisplayNotification("Loans", "You took out a college loan - manage it in the Pit Stop", true);
			break;
		}
	}

	private int GetNextAvailableNotificationArea() {
		int currVal = currentSpawnAreaIndex;
		currentSpawnAreaIndex++;
		if( currentSpawnAreaIndex >= notificationSpawnAreas.Length )
			currentSpawnAreaIndex = 0;

		return currVal;
	}
}
