﻿using UnityEngine;
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
	public GameObject TutorialGUIs;
	int tutorialIterator = 0;
	float slideDuration = 3f;
	float sliderTimer;


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

			}
			break;

		case GameState.Tutorial :
			if (tutorialIsOver) {
				currentGameState = GameState.Playing;
			}
			break;

		case GameState.Playing :

			if (switchToCutScene) {
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
			if (switchToGame) {
				currentGameState = GameState.Playing;
			}
			break;
		}
		//Test purposes
		if (Input.GetKey(KeyCode.M)){
			switchToInventory = true;
			switchToGame = false;
		}
		if (Input.GetKey(KeyCode.N)){
			switchToGame = true;
			switchToInventory = false;
		}
		
	}
	
	public void SetNewBurnRate(float newBurnRate){
		loseADollarRate = ageAYearRate / (newBurnRate);
	}
	
	void StartTutorial () {

	}

	public void StartGame () {
		userPressedStart = true;
		MainMenuGUI.SetActive (false);
		MainMenuText.SetActive (false);
		TutorialGUIs.SetActive (true);
	}

	public void SwitchToCutscene () {
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
