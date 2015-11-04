using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
	private static PlayerStats _playerStats;

	// Year completion event
	public delegate void YearCompletion();
	public static event YearCompletion OnYearCompleted;
	
	public float yearTimer = 0f;
	public float secondsPerYear = 16f;

	public int age = 15;
	public float money = 1000f;
	public float cashFlow = -200f;
	public float happiness = 0.5f;
	public float happinessDecreaseRate = 0.01f;

	public List<Business> playerBusinesses = new List<Business>();
	public List<Loan> playerLoans = new List<Loan> ();
	public List<Investment> playerInvestments = new List<Investment>();
	public List<RealEstate> playerRealEstate = new List<RealEstate>();
	public Family playerFamily;

	public static PlayerStats s_instance { get {return _playerStats;} }

	private const int MAX_INVESTMENTS = 4;
	private const int MAX_BUSINESSES = 4;
	private const int MAX_REALESTATE = 4;
	public const float IRA_INIT_COST = 100f;
	public const float STOCK_INIT_COST = 50000f;
	public const float MUTUAL_INIT_COST = 5000f;




	void Awake() {
		if( _playerStats == null )
			_playerStats = this;
		else if( _playerStats != this ) {
			Destroy(this.gameObject);
			Debug.LogWarning("Deleted duplicate playerstats");
		}
	}

	void Start() {
		Loan temp1 = new Loan ();
		temp1.InitializeLoan (0, Loan.LoanType.Business);
		Loan temp2 = new Loan ();
		temp2.InitializeLoan (0, Loan.LoanType.RealEstate);
		Loan temp3 = new Loan ();
		temp3.InitializeLoan (0, Loan.LoanType.School);
		playerLoans.Add (temp1);
		playerLoans.Add (temp2);
		playerLoans.Add (temp3);
	}

	void Update () {
		if( Input.GetKeyDown( KeyCode.Return ) )
			GUIManager.s_instance.DisplayNotification( "Notice!", "Eric pressed the Return key!" );

		if( GameManager.s_instance.currentGameState == GameState.Playing ) {
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
				GUIManager.s_instance.DisplayBday(age);
				yearTimer = 0f;

				if( OnYearCompleted != null ) 
					OnYearCompleted();
				else
					Debug.LogWarning( "OnYearCompleted() is null. (No script subscribed to event)" );
			}
		}
	}

	#region Loan
	//adds to preexisting Loan values
	public void AddLoanCost(float thisLoanAmount, Loan.LoanType type) {
		foreach (Loan x in playerLoans) {
			if (x.thisLoanType == type) {
				x.loanAmount+=thisLoanAmount;
				print (x.loanAmount);
			}
		}
	}

	public void IncreaseLoanPaymentRate (int thisIndex) {
		playerLoans[thisIndex].annualPaymentPercentage *= 2f;
	}

	public void PayLoanAmount( int index, float amount ) {
		if( money >= playerLoans[index].loanAmount )
			playerLoans[index].PayLoanAmount( amount );
		else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "You don't have enough money to do that" );
		}
	}

	public void PayOffLoan(int index) {
		// If player has enough money pay off loan and remove from list
		if( money >= playerLoans[index].loanAmount ) {
			Loan selectedLoan = playerLoans[index];
			selectedLoan.PayLoanAmount( selectedLoan.loanAmount );
//			playerLoans.Remove( selectedLoan );
			// TODO Update list on GUI
		} else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "You don't have enough money to pay the loan off." );
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
	public void AddBusiness ( int businessType ) {
		float businessCost = Business.BusinessPrices[businessType];

		if (businessCost < money && playerBusinesses.Count < MAX_BUSINESSES && businessCost != 0) {
			Business newBusiness = new Business ();
			newBusiness.SetBusinessType (businessType);
			AddLoanCost (Business.BusinessPrices [businessType], Loan.LoanType.Business);
			playerBusinesses.Add (newBusiness);
			GUIManager.s_instance.DisplayNotification("Notice!", "Business Loan Added.");
		} else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "Insufficient Funds." );

		}
	}
	
	public void SellBusiness (int index) {
		Business selectedBusiness = playerBusinesses[index];
		money += selectedBusiness.valuation;
		playerBusinesses.Remove( selectedBusiness );
	}

	public void WorkOvertime(int businessIndex) {
		// TODO GUI notification
		Debug.LogWarning( "You lost happiness" );
		happiness *= 0.9f;
		playerBusinesses[businessIndex].revenueStream *= 1.1f;
	}
	#endregion

	#region RealEstate
	public bool AddRealEstate( int realEstateTier ) {
		float realEstateCost = RealEstate.RealEstatePrices[realEstateTier];
		if (playerRealEstate.Count >= MAX_REALESTATE) {
			Debug.LogWarning("MAX REALESTATE REACHED");
			return false;
		}
		if (realEstateCost <= money) {
			Debug.Log ("REAL ESTATE COST " + realEstateCost);
			AddLoanCost(realEstateCost, Loan.LoanType.RealEstate);
			RealEstate newRealEstate = new RealEstate();
			newRealEstate.SetTier( (RealEstate.RealEstateTier)realEstateTier );
			playerRealEstate.Add( newRealEstate );
			GUIManager.s_instance.DisplayNotification("Notice!", "Real Estate Loan Added.");

			return true;
		} else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "Insufficient Funds." );
			return false;
		}
	}
	public void SellRealEstate(int realEstateIndex) {
		money += playerRealEstate[realEstateIndex].realEstateValue;
		playerRealEstate.RemoveAt( realEstateIndex );
	}
	#endregion

	#region Investment
	public void AddInvestment(Investment.InvestmentType thisType) {
		if( playerInvestments.Count < MAX_INVESTMENTS ) {


		} else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "Investment limit reached." );
			return;
		}

		float cost;
		if (thisType == Investment.InvestmentType.IRA) {
			cost = IRA_INIT_COST;
		}
		else if (thisType == Investment.InvestmentType.Mutual){
			cost = MUTUAL_INIT_COST;
		}
		else {
			cost = STOCK_INIT_COST;
		}
		if (cost > money) {
			GUIManager.s_instance.DisplayNotification ("Notice!", "Insufficient funds.");
		} else {
			Investment newInvestment = new Investment();
			newInvestment.thisInvestmentType = thisType;
			newInvestment.SetMonetaryValue(cost);
			money -= cost;
			playerInvestments.Add( newInvestment );
		}

	}

	public bool AddMoneyToInvestment( int index, float amount ) {
		if ( money >= amount ) {
			// If this is an IRA and we have invested too much money this year
			if( playerInvestments[index].thisInvestmentType == Investment.InvestmentType.IRA && playerInvestments[index].moneyAddedThisYear+amount >= Investment.MAX_MONEY_ADDED_PER_YEAR_TO_IRA ) {
				GUIManager.s_instance.DisplayNotification( "Notice!", "IRA Limit 5K/YR" );
				return false;
			}

			playerInvestments[index].AddMoreMoney( amount );
			return true;
		} else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "Insufficient funds." );
			return false;
		}
	}

	public void LiquidateInvestment( int index, float percentage ) {
		print ("liquidating " + index + " " + percentage);
		playerInvestments[index].Liquidate( percentage );
	}

	public void HandleInvestmentModification (int index, int lastIndex) {

	}
	#endregion
}