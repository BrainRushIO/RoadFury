using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {Pause, MainMenu, Intro, Playing, Cutscene, Notification, PitStop, InventoryMode, GameOver, Win};
public enum AgeState {YoungAdult, Adult, OldAdult, SeniorCitizen};

/*
This class managed game states, switches between game states
 */

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

	// References
	PlayerController playerController;

	//switches
	bool switchToNotification;
	bool switchToInventory;
	bool switchToPitstop;
	bool switchToGame;
	bool userPressedStart = false;
	bool switchToCutScene;
	bool switchToPaused;
	//lerps
	float cameraRotateStartTime;
	bool isCamRotateUp, isCamRotateDown;

	public GameObject tutorialCam;
	public Animator[] allAnimators;

	public GameObject currentGUIseries;
	public GameState previousGameState;

	//TODO add attrition rate increases depending on if player gets wife or gf or not
	
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
		allAnimators = GameObject.FindObjectsOfType<Animator> ();
		print (allAnimators.Length + "total animators");
		playerController = GameObject.FindObjectOfType<PlayerController>();
		if( playerController == null )
			Debug.LogError( "GameManager didn't find a reference to a PlayerController in the scene." );
	}



	//Swipe up to switchToInventory
	//Swipe down to switchToRoad

	// Update is called once per frame
	void Update () {
//		print (currentGameState);
		switch (currentGameState) {

		case GameState.MainMenu : 
			if (userPressedStart) {
				userPressedStart = false;
				currentGameState = GameState.Playing;
			}
			break;

		case GameState.Pause :
			if (switchToGame) {
				switchToGame = false;
				currentGameState = GameState.Playing;
			}

			break;
		case GameState.Playing :
			if (switchToCutScene) {
				switchToCutScene = false;
				currentGameState = GameState.Cutscene;
			}
			if (switchToPitstop) {
				switchToPitstop = false;
				currentGameState = GameState.PitStop;
			}

			if (switchToPaused) {
				switchToPaused = false;
				currentGameState = GameState.Pause;
			}

			break;

		case GameState.Cutscene : 
			if (switchToGame) {
				switchToGame = false;
				currentGameState = GameState.Playing;
			}
			break;
		case GameState.PitStop : 
			if (switchToGame) {
				switchToGame = false;
				currentGameState = GameState.Playing;
			}
			if (switchToNotification) {
				switchToNotification = false;
				currentGameState = GameState.Notification;
			}
			break;
		case GameState.Notification : 
			if (switchToGame) {
				switchToGame = false;
				currentGameState = GameState.Playing;
			}
			if (switchToPitstop) {
				switchToPitstop = false;
				currentGameState = GameState.PitStop;
			}
			break;
		
	}
	
}


	public void StartGame () {
		userPressedStart = true;
		GUIManager.s_instance.MainMenuGUI.SetActive (false);
		Camera.main.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		GameObject.FindGameObjectWithTag ("CamPos").GetComponent<Cinematographer> ().quaternions [0] = Camera.main.transform;
		GameObject.FindGameObjectWithTag ("CamPos").GetComponent<Cinematographer> ().RollCamera (.2f);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetTrigger ("run");
		Camera.main.GetComponent<HoverFollowCam> ().enabled = true;
		//				inGameGUI.SetActive (true);
		GUIManager.s_instance.inGameGUI.GetComponent<Animator> ().SetTrigger ("show");
	}

	public void SwitchToCutscene () {
		GUIManager.s_instance.inGameGUI.GetComponent<Animator> ().SetTrigger("hide");
		switchToGame = false;
		switchToCutScene = true;
		Camera.main.GetComponent<Cinematographer> ().RollCamera();
		Camera.main.GetComponent<HoverFollowCam>().enabled = false;

	}

	public void SwitchToPitStop () {
		if (currentGameState == GameState.Playing) {
			switchToPitstop = true;
			GUIManager.s_instance.PitstopFlashEnter ();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().StartPlayback();
			PitStopGUIManager.s_instance.DisplayCurrentMenu(); //fixes the update issue where money incorrectly displayed
		}

		if (currentGameState == GameState.Notification) {
			switchToPitstop = true;
		}
			
			
	}
	public void SwitchToGame () {
		if (currentGameState == GameState.PitStop) {
			// Player
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().StopPlayback();

			playerController.SetAtRespawnPos();
			GUIManager.s_instance.PitstopFlashExit();
			switchToGame = true;
		}

		if (currentGameState == GameState.Pause) {
			GUIManager.s_instance.ClosePauseMenu();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().StopPlayback();
			switchToGame = true;
		} else if ( currentGameState == GameState.Intro ) {
			playerController.CheckGroundOrientation();
		} else if ( currentGameState == GameState.Cutscene ) {
			GUIManager.s_instance.inGameGUI.GetComponent<Animator> ().SetTrigger("show");
			switchToGame = true;
		}
	}

	public void SwitchToNotification() {
		switchToNotification = true;
		if( previousGameState != GameState.Notification )
			previousGameState = currentGameState;
	}


	public void SwitchToPaused () {
		switchToPaused = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().StartPlayback();
	}
	public void SwitchToPauseMenu () {
		if (GameManager.s_instance.currentGameState == GameState.Playing) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().StartPlayback();
			switchToPaused = true;
			GUIManager.s_instance.DisplayPauseMenu ();
		}
	}

	public void LoadMainMenu() {
		Application.LoadLevel (1);
	}
}
