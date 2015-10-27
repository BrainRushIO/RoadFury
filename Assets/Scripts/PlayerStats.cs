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
	public Family playerFamily;

	public static PlayerStats s_instance { get {return _playerStats;} }

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

	public void AddLoan(string loanName, float loanAmount) {

	}

	public void IncreaseLoanPaymentRate (int thisIndex) {
		playerLoans[thisIndex].annualPaymentPercentage *= 2f;
	}

	public void PayOffLoan(int index) {
		//check if enough money
		//pay off loan
	}

	public void AddBusiness () {

	}

	public void SellBusiness (int index) {

	}

	public void GetMarried() {
		playerFamily.GetMarried();
	}

	public void AddKid() {
		playerFamily.AddKid();
	}
}
