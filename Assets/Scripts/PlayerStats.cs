using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
	private static PlayerStats _playerStats;

	// Year completion event
	public delegate void YearCompletion();
	public static event YearCompletion OnYearCompleted;
	
	public float yearTimer = 0f;
	public float secondsPerYear = 15f;

	public int age = 16;
	public float money = 1000f;
	public float cashFlow = -200f;
	public float happiness = 0.5f;
	public float happinessDecreateRate = 0.1f;

	public List<Business> playerBusinesses;
	public List<Loan> playerLoans;
	public List<Investment> playerInvestments;
	public List<RealEstate> playerRealEstate;
	public Family playerFamily;

	public static PlayerStats s_instance { get {return _playerStats;} }

	private const int MAX_INVESTMENTS = 4;		// TODO: Maybe modify this.

	void Awake() {
		if( _playerStats == null )
			_playerStats = this;
		else if( _playerStats != this ) {
			Destroy(this.gameObject);
			Debug.LogWarning("Deleted duplicate playerstats");
		}
	}

	void Start() {
		playerFamily = new Family();
	}

	void Update () {
		// Add cashFlow to money
		money += cashFlow * (1/secondsPerYear) * Time.deltaTime;

		// Happiness calculation / check
		happiness -= happinessDecreateRate*Time.deltaTime;
		if( happiness <= 0f ) {
			// TODO: GameOver
			Debug.Log( "Happiness fell bellow 0, you are dead." );
		}

		// Year calculation
		yearTimer += Time.deltaTime;
		if( yearTimer >= secondsPerYear ) {
			age++;
			yearTimer = 0f;

			if( OnYearCompleted != null )
				OnYearCompleted();
			else
				Debug.LogWarning( "OnYearCompleted() is null. (No script subscribed to event)" );
		}
	}

	public void SetCashFlow() {
		// TODO:Look at all things that could affect this and do it
	}

	#region Loan
	public void AddLoan(string loanName, float loanAmount) {

	}

	public void IncreaseLoanPaymentRate (int thisIndex) {
		playerLoans[thisIndex].annualPaymentPercentage *= 2f;
	}

	public void PayOffLoan(int index) {
		//check if enough money
		//pay off loan
	}
	#endregion

	public void AddBusiness (float initInvestment) {
		money -= initInvestment;
		if (initInvestment == 10000f) {

		} else if (initInvestment == 100000f) {

		} else if (initInvestment == 1000000f) {

		}
	}

	public void SellBusiness (int index) {

	}

	#region Family
	public void GetMarried() {
		playerFamily.GetMarried();
	}

	public void AddKid() {
		playerFamily.AddKid();
	}
	#endregion

	#region Business
	public void WorkOvertime(int businessIndex) {

	}

	public bool CanStartNewBusiness(int businessType) {
		float businessCost = 0;
		if (businessType == 0) {
			businessCost = 10000f;
		}
		else if (businessType == 1) {
			businessCost = 100000f;
		}
		else if (businessType == 2) {
			businessCost = 1000000f;
		}

		if (businessCost < money && playerBusinesses.Count < 7 && businessCost!=0) {
			AddBusiness (businessCost);
			return true;
		} else {
			Debug.Log("Cannot start business");
			return false;
		}
	}
	#endregion

	#region RealEstate
	public bool CanBuyNewRealEstate( int realEstateTier ) {
		float realEstateCost = 1000000000;
		if ( realEstateTier== 0) {
			realEstateCost = 10000f;
		}
		else if (realEstateTier == 1) {
			realEstateCost = 100000f;
		}
		else if (realEstateTier == 2) {
			realEstateCost = 1000000f;
		}
		
		if (realEstateCost <= money) {
			money -= realEstateCost;
			//TODO add new real estate to real estate list
			return true;
		} else {
			Debug.LogWarning ("Not enough money to buy real estate.");
			return false;
		}
	}
	#endregion

	#region Investment
	public void AddInvestment(Investment.InvestmentType thisType) {
		if( playerInvestments.Count < MAX_INVESTMENTS ) {
			Investment newInvestment = new Investment();
			newInvestment.thisInvestmentType = thisType;
		} else {
			Debug.LogWarning( "You've maxed out the amount of investments you can have." );
		}
	}

	public bool AddMoneyToInvestment( int index, float amount ) {
		if( playerInvestments[index].moneyAddedThisYear+amount <= Investment.MaxMoneyAddedPerYear || money >= amount ) {
			playerInvestments[index].AddMoreMoney( amount );
			return true;
		} else {
			Debug.LogWarning( "You're exceeding the amount of money you can add to this investment per year. ($" + Investment.MaxMoneyAddedPerYear +") or you lack the money to do this.");
			return false;
		}
	}

	public void LiquidateInvestment( int index, float percentage ) {
		playerInvestments[index].Liquidate( percentage );
	}

	public void HandleInvestmentModification (int index, int lastIndex) {

	}
	#endregion
}
