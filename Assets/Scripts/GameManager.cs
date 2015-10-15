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
	Quaternion cameraOnRoadRotation, cameraOnInventoryRotation;
	public GameObject houseAndFamily;

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

	float attrition = 0.0001f;
	//TODO add attrition rate increases depending on if player gets wife or gf or not
	
	void Start () {
//		houseAndFamily.SetActive(false);
		SetNewBurnRate(-200);
		cameraOnRoadRotation = Camera.main.transform.localRotation;
		cameraOnInventoryRotation = Quaternion.Euler(300f,0,0);
	}



	//Swipe up to switchToInventory
	//Swipe down to switchToRoad

	// Update is called once per frame
	void Update () {
		print (currentGameState);
		switch (currentGameState) {

		case GameState.IntroScreen :

			break;

		case GameState.MainMenu : 
			if (userPressedStart) {

			}
			break;

		case GameState.Tutorial :

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

			if (switchToInventory) {
//				houseAndFamily.SetActive(true);
				GUIManager.s_instance.SwitchFromGameToInventoryGUI();
				switchToInventory = false;
				PanCameraToInventory();
				currentGameState = GameState.InventoryMode;
			}
			break;

		case GameState.Cutscene : 
			if (switchToGame) {
				currentGameState = GameState.Playing;
			}
			break;
		case GameState.InventoryMode :
			if (isCamRotateUp) {
				float cameraRotatePercentage = (Time.time - cameraRotateStartTime) / cameraRotateDuration;
				Camera.main.transform.localRotation = Quaternion.Lerp(cameraOnRoadRotation, cameraOnInventoryRotation, cameraRotatePercentage);
				if (cameraRotatePercentage > .97f){
					isCamRotateUp = false;
				}
			}

			if (isCamRotateDown) {
				float cameraRotatePercentage = (Time.time - cameraRotateStartTime) / cameraRotateDuration;
				Camera.main.transform.localRotation = Quaternion.Lerp(cameraOnInventoryRotation, cameraOnRoadRotation, cameraRotatePercentage);
				if (cameraRotatePercentage > .97f){
					isCamRotateDown = false;
					currentGameState = GameState.Playing;
				}
			}

			if (switchToGame) {
//				houseAndFamily.SetActive(false);
				GUIManager.s_instance.SwitchFromInventoryToGameGUI();
				switchToGame = false;
				PanCameraToRoad();
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
		print ("loseADollarRate" + loseADollarRate); 
	}

	void PanCameraToInventory(){
		isCamRotateUp = true;
		cameraRotateStartTime = Time.time;
	}

	void PanCameraToRoad(){
		isCamRotateDown = true;
		cameraRotateStartTime = Time.time;
	}

	public void StartGame () {
		currentGameState = GameState.Tutorial;
	}

	public void SwitchToCutscene () {
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Cinematographer> ().RollCamera();
		switchToGame = false;
		switchToCutScene = true;
	}

	public void SwitchToGame () {
		switchToGame = true;
		switchToCutScene = false;
	}

}
