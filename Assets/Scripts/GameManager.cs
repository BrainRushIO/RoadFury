﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {Pause, MainMenu, Intro, Playing, Cutscene, Notification, PitStop, InventoryMode, GameOver, Win};
public enum AgeState {YoungAdult, Adult, OldAdult, SeniorCitizen};

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

	//switches
	bool switchToNotification;
	bool switchToInventory;
	bool switchToPitstop;
	bool switchToGame;
	bool userPressedStart = false;
	bool tutorialIsOver = false;
	bool switchToCutScene;
	//lerps
	float cameraRotateStartTime;
	float cameraRotateDuration = .5f;
	bool isCamRotateUp, isCamRotateDown;

	public GameObject tutorialCam;
	public GameObject pitStopGUI;
	public GameObject inGameGUI;
	public GameObject MainMenuGUI, MainMenuText;
	int textIterator = 0;
	float slideDuration = 3f;
	float slideTimer;

	public GameObject currentGUIseries;
	private GameState previousGameState;

	//TODO add attrition rate increases depending on if player gets wife or gf or not
	
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
	}



	//Swipe up to switchToInventory
	//Swipe down to switchToRoad

	// Update is called once per frame
	void Update () {
		switch (currentGameState) {

		case GameState.MainMenu : 
			if (userPressedStart) {
				currentGameState = GameState.Intro;
			}
			break;

		case GameState.Intro :
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
				switchToCutScene = false;
				slideTimer = 0;
				currentGameState = GameState.Cutscene;
			}
			if (switchToPitstop) {
				switchToPitstop = false;
				pitStopGUI.GetComponent<Animator>().SetTrigger("pitstop");
				currentGameState = GameState.PitStop;
			}

			break;

		case GameState.Cutscene : 
			RunCutSceneText();
			if (switchToGame) {
				currentGameState = GameState.Playing;
			}
			break;
		case GameState.PitStop : 
			if (switchToGame) {
				switchToGame = false;
				currentGameState = GameState.Playing;
				pitStopGUI.GetComponent<Animator>().SetTrigger("pitstop");
			}
			if (switchToNotification) {
				currentGameState = GameState.Notification;
			}
			break;
		case GameState.Notification : 
			if (switchToGame) {
				currentGameState = GameState.Playing;
			}
			if (switchToPitstop) {
				currentGameState = GameState.PitStop;
			}
			break;
		
	}
	
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
				if (currentGameState == GameState.Intro) {
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
		inGameGUI.GetComponent<Animator> ().SetTrigger("hide");
		//inGameGUI.SetActive (false);
		textIterator = 0;
		switchToGame = false;
		switchToCutScene = true;
		Camera.main.GetComponent<Cinematographer> ().RollCamera();
		Camera.main.GetComponent<HoverFollowCam>().enabled = false;

	}

	public void SwitchToPitStop () {
		if (currentGameState == GameState.Playing) {
			switchToPitstop = true;
			inGameGUI.GetComponent<Animator> ().SetTrigger ("pitstop");
		}

		if (currentGameState == GameState.Notification) {
			switchToPitstop = true;

		}
			
			
	}
	public void SwitchToGame () {
		if (currentGameState == GameState.Cutscene) {
			Camera.main.GetComponent<HoverFollowCam> ().enabled = true;
			if (currentGameState != GameState.Intro) {
				currentGUIseries.SetActive (false);
				//inGameGUI.SetActive (true);
				inGameGUI.GetComponent<Animator> ().SetTrigger ("show");
			}
			switchToGame = true;

			switchToCutScene = false;
		} else if (currentGameState == GameState.PitStop) {
			switchToGame = true;
			inGameGUI.GetComponent<Animator> ().SetTrigger("pitstop");
		}
	}

	public void SwitchToNotification() {
		switchToNotification = true;

	}
}
