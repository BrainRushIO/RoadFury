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
	public float happinessDecreaseRate = 0.001f;

	public List<Business> playerBusinesses;
	public List<Loan> playerLoans;
	public List<Investment> playerInvestments;
	public List<RealEstate> playerRealEstate;
	public Family playerFamily;

	public static PlayerStats s_instance { get {return _playerStats;} }

	private const int MAX_INVESTMENTS = 4;
	private const int MAX_BUSINESSES = 7;

	void Awake() {
		if( _playerStats == null )
			_playerStats = this;
		else if( _playerStats != this ) {
			Destroy(this.gameObject);
			Debug.LogWarning("Deleted duplicate playerstats");
		}
	}

	void Start() {
//		playerFamily = new Family();
	}

	void Update () {
		// Add cashFlow to money
		money += cashFlow * (1/secondsPerYear) * Time.deltaTime;

		// Happiness calculation / check
		happiness -= happinessDecreaseRate*Time.deltaTime;
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
		Loan newLoan = new Loan();
		newLoan.loanName = loanName;
		newLoan.SetInitialLoanAmount( loanAmount );
		playerLoans.Add( newLoan );
	}

	public void IncreaseLoanPaymentRate (int thisIndex) {
		playerLoans[thisIndex].annualPaymentPercentage *= 2f;
	}

	public void PayLoanAmount( int index, float amount ) {
		if( money >= playerLoans[index].loanAmount )
			playerLoans[index].PayLoanAmount( amount );
		else {
			// TODO GUI notification
			Debug.Log( "Not enough money." );
		}
	}

	public void PayOffLoan(int index) {
		// If player has enough money pay off loan and remove from list
		if( money >= playerLoans[index].loanAmount ) {
			Loan selectedLoan = playerLoans[index];
			selectedLoan.PayLoanAmount( selectedLoan.loanAmount );
			playerLoans.Remove( selectedLoan );
			Destroy( selectedLoan );
			// TODO Update list on GUI
		} else {
			// TODO Add GUI notification
			Debug.Log( "You don't have enough money to pay the loan off." );
		}
	}
	#endregion

	#region Family
	public void GetMarried() {
		playerFamily.GetMarried();
	}

	public void AddKid() {
		playerFamily.AddKid();
	}
	#endregion

	#region Business
	public bool CanStartNewBusiness(int businessType) {
		float businessCost = Business.BusinessPrices[businessType];
		if (businessCost < money && playerBusinesses.Count < MAX_BUSINESSES && businessCost!=0)
			return true;
		else
			Debug.Log("Cannot start business");
		return false;
	}

	public void AddBusiness ( int businessType ) {
		Business newBusiness = new Business();
		newBusiness.initialInvestment = Business.BusinessPrices[businessType];
		money -= Business.BusinessPrices[businessType];
		playerBusinesses.Add( newBusiness );
	}
	
	public void SellBusiness (int index) {
		Business selectedBusiness = playerBusinesses[index];
		money += selectedBusiness.valuation;
		playerBusinesses.Remove( selectedBusiness );
		Destroy( selectedBusiness );
	}

	public void WorkOvertime(int businessIndex) {
		// TODO Decide conversion rate between Happiness/Money
	}
	#endregion

	#region RealEstate
	public bool CanBuyNewRealEstate( int realEstateTier ) {
		float realEstateCost = RealEstate.RealEstatePrices[realEstateTier];
		
		if (realEstateCost <= money) {
			money -= realEstateCost;
			RealEstate newRealEstate = new RealEstate();
			newRealEstate.thisRealEstateTier = (RealEstate.RealEstateTier)realEstateTier;
			playerRealEstate.Add( newRealEstate );
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
			playerInvestments.Add( newInvestment );
		} else {
			Debug.LogWarning( "You've maxed out the amount of investments you can have." );
		}
	}

	public bool AddMoneyToInvestment( int index, float amount ) {
		if ( money >= amount ) {
			// If this is an IRA and we have invested too much money this year
			if( playerInvestments[index].thisInvestmentType == Investment.InvestmentType.IRA && playerInvestments[index].moneyAddedThisYear+amount <= Investment.MAX_MONEY_ADDED_PER_YEAR_TO_IRA ) {
				//TODO Add GUI notification
				Debug.LogWarning( "You're exceeding the amount of money you can add to this investment per year. ($" + Investment.MAX_MONEY_ADDED_PER_YEAR_TO_IRA +")" );
				return false;
			}

			playerInvestments[index].AddMoreMoney( amount );
			return true;
		} else {
			// TODO Add GUI notification.
			Debug.LogWarning("You lack the money to do this.");
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