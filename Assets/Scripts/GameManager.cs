using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {IntroScreen, MainMenu, Tutorial, Playing, Cutscene, DecisionMode, InventoryMode, GameOver};
public enum AgeState {YoungAdult, Adult, OldAdult, SeniorCitizen};
public enum CareerState {BusBoy, FryCook, Manager, StoreManager, RegionalManager, OperationsDirector, VPofOperations, COO, CEO, Retired};
public enum CareerType {Medical, BusinessFinance, Engineering, Entertainment, GovernmentLegal}

public class GameManager : MonoBehaviour {

	public GameState currentGameState = GameState.Playing;
	public static GameManager s_instance;

	void Awake(){
		if (s_instance==null) {
			s_instance = this;
		}
		else if (s_instance!=this) {
			Destroy(gameObject);
		}
	}

	public float strafeSpeed = .1f;
	public float playerBounds = 4f;
	public int age = 16, money = 1000;
	public Text moneyText, ageText, burnRateText;
	public Slider happiness;
	
	public float burnRateValue = -200;
	public float loseADollarRate = 0;
	public float loseADollarTimer = 0f;
	public float ageAYearRate = 10f;
	public float ageAYearTimer = 0f;

	//switches
	bool switchToInventory;
	bool switchToGame;
	bool userPressedStart = false;
	bool tutorialIsOver = false;
	bool switchToCutScene;
	//lerps
	float cameraRotateStartTime;
	float cameraRotateDuration = .5f;
	bool isCamRotateUp, isCamRotateDown;

	public GameObject tutorialCam;
	public GameObject MainMenuGUI, MainMenuText;
	int textIterator = 0;
	float slideDuration = 3f;
	float slideTimer;

	public GameObject currentGUIseries;


	float attrition = 0.0001f;
	//TODO add attrition rate increases depending on if player gets wife or gf or not
	
	void Start () {
		SetNewBurnRate(-200);
	}



	//Swipe up to switchToInventory
	//Swipe down to switchToRoad

	// Update is called once per frame
	void Update () {
		switch (currentGameState) {

		case GameState.MainMenu : 
			if (userPressedStart) {
				currentGameState = GameState.Tutorial;
			}
			break;

		case GameState.Tutorial :
			if (Input.GetKeyDown(KeyCode.Space)) {
				EndTutorial();
			}
			if (tutorialIsOver) {
				currentGameState = GameState.Playing;
			}
			else {
				RunCutSceneText();
			}
			break;

		case GameState.Playing :

			if (switchToCutScene) {
				slideTimer = 0;
				currentGameState = GameState.Cutscene;
			}

			//Display
//			ageText.text = "Age: " + age.ToString ();
//			moneyText.text = "Money: $" + money.ToString ();
//			burnRateText.text = "Cash Flow: $" + Mathf.CeilToInt (burnRateValue).ToString ();
//			happiness.value -= attrition;
			
//			//timers
//			ageAYearTimer += Time.deltaTime;
//			if (ageAYearTimer > ageAYearRate) {
//				ageAYearTimer = 0;
//				age++;
//			}
//			
//			loseADollarTimer += Time.deltaTime;
//			if (loseADollarTimer > Mathf.Abs(loseADollarRate) && loseADollarRate < 0) {
//				loseADollarTimer = 0;
//				money--;
//			}
//			else if (loseADollarTimer > Mathf.Abs(loseADollarRate) && loseADollarRate > 0) {
//				loseADollarTimer = 0;
//				money++;
//			}
//			if (happiness.value <= 0) {
//				currentGameState = GameState.GameOver;
//			}

			break;

		case GameState.Cutscene : 
			RunCutSceneText();
			if (switchToGame) {
				currentGameState = GameState.Playing;
			}
			break;
		}

	}
	
	public void SetNewBurnRate(float newBurnRate){
		loseADollarRate = ageAYearRate / (newBurnRate);
	}

	public void StartGame () {
		userPressedStart = true;
		MainMenuGUI.SetActive (false);
		MainMenuText.SetActive (false);
		currentGUIseries.SetActive (true);
		Camera.main.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		tutorialCam.GetComponent<Cinematographer> ().quaternions [0] = Camera.main.transform;
		tutorialCam.GetComponent<Cinematographer> ().RollCamera ();
	}

	public void EndTutorial () {
		tutorialIsOver = true;
		currentGUIseries.SetActive (false);
		GameObject.FindGameObjectWithTag ("CamPos").GetComponent<Cinematographer> ().RollCamera ();
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetTrigger ("run");

	}

	void RunCutSceneText () {
		slideTimer += Time.deltaTime;
		if (slideTimer > slideDuration) {
			if (textIterator == currentGUIseries.transform.childCount-1) {
				slideTimer = 0;
				if (currentGameState == GameState.Tutorial) {
					EndTutorial();
				}
				else {
					currentGUIseries.SetActive (false);
				}
			}
			else if (textIterator < currentGUIseries.transform.childCount - 1) {
				currentGUIseries.transform.GetChild(textIterator).gameObject.SetActive(false);
				textIterator++;
				currentGUIseries.transform.GetChild(textIterator).gameObject.SetActive(true);
				slideTimer = 0;
			}
		}
	}

	public void SwitchToCutscene () {
		textIterator = 0;
		Camera.main.GetComponent<Cinematographer> ().RollCamera();
		switchToGame = false;
		switchToCutScene = true;
		Camera.main.GetComponent<HoverFollowCam>().enabled = false;

	}

	public void SwitchToGame () {
		Camera.main.GetComponent<HoverFollowCam>().enabled = true;
		switchToGame = true;
		switchToCutScene = false;
	}

}
