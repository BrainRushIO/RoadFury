using UnityEngine;
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

	public GameObject camera1;
	public GameObject tutorialCam;
	public GameObject pitStopGUI;
	public GameObject inGameGUI;
	public GameObject MainMenuGUI;
	public GameObject faderObj;
	public Animator[] allAnimators;
	int textIterator = 0;
	float slideDuration = 3f;
	float slideTimer;

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
		MainMenuGUI.SetActive (false);
		Camera.main.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		GameObject.FindGameObjectWithTag ("CamPos").GetComponent<Cinematographer> ().quaternions [0] = Camera.main.transform;
		GameObject.FindGameObjectWithTag ("CamPos").GetComponent<Cinematographer> ().RollCamera ();
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetTrigger ("run");
		Camera.main.GetComponent<HoverFollowCam> ().enabled = true;
		//				inGameGUI.SetActive (true);
		inGameGUI.GetComponent<Animator> ().SetTrigger ("show");
	}

	public void SwitchToCutscene () {
		slideTimer = 0;
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
			PitstopFlashEnter ();
		}

		if (currentGameState == GameState.Notification) {
			switchToPitstop = true;

		}
			
			
	}
	public void SwitchToGame () {
		if (currentGameState == GameState.PitStop) {
			// Player
			playerController.SetAtRespawnPos();
			PitstopFlashExit();
			switchToGame = true;
		}

		if (currentGameState == GameState.Pause) {
			GUIManager.s_instance.ClosePauseMenu();
			GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().StopPlayback();
			switchToGame = true;
		} else if ( currentGameState == GameState.Intro ) {
			playerController.CheckGroundOrientation();
		} else if ( currentGameState == GameState.Cutscene ) {
			inGameGUI.GetComponent<Animator> ().SetTrigger("show");
			switchToGame = true;
		}
	}

	public void SwitchToNotification() {
		switchToNotification = true;
		if( previousGameState != GameState.Notification )
			previousGameState = currentGameState;
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
