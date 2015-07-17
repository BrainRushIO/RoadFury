using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {Playing, DecisionMode, InventoryMode};
public enum AgeState {YoungAdult, Adult, OldAdult, SeniorCitizen};
public enum CareerState {BusBoy, FryCook, Manager, StoreManager, RegionalManager, OperationsDirector, VPofOperations, COO, CEO, Retired};

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
	
	float attrition = 0.0001f;
	//TODO add attrition rate increases depending on if player gets wife or gf or not
	
	void Start () {
		SetNewBurnRate(-200);
	}
	
	// Update is called once per frame
	void Update () {
		
		//Display
		ageText.text = "Age: " + age.ToString ();
		moneyText.text = "Money: $" + money.ToString ();
		burnRateText.text = "Cash Flow: $" + Mathf.CeilToInt (burnRateValue).ToString ();
		happiness.value -= attrition;
		
		//timers
		ageAYearTimer += Time.deltaTime;
		if (ageAYearTimer > ageAYearRate) {
			ageAYearTimer = 0;
			age++;
		}
		
		loseADollarTimer += Time.deltaTime;
		if (loseADollarTimer > Mathf.Abs(loseADollarRate) && loseADollarRate < 0) {
			loseADollarTimer = 0;
			money--;
		}
		else if (loseADollarTimer > Mathf.Abs(loseADollarRate) && loseADollarRate > 0) {
			loseADollarTimer = 0;
			money++;
		}
		
		
	}
	
	public void SetNewBurnRate(float newBurnRate){
		loseADollarRate = ageAYearRate / (newBurnRate);
		print ("loseADollarRate" + loseADollarRate); 
	}

}
