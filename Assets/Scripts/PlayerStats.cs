using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
	private static PlayerStats _playerStats;

	// Year completion event
	public delegate void YearCompletion();
	public static event YearCompletion OnYearCompleted;
	private float yearTimer = 0f;
	public float yearCompletionTime = 15f;

	public float secondsPerYear = 15f;

	public int age = 16;
	public float money = 1000f;
	public float cashFlow = -200f;
	public float happiness = 0.5f;
	public float happinessDecreateRate = 0.1f;

	public List<Business> playerBusinesses;
	public List<Loan> playerLoans;

	private float moneyAtBeginningOfYear;

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
		moneyAtBeginningOfYear = money;
	}

	void Update () {
		// Add cashFlow to money
		money = Mathf.Lerp( moneyAtBeginningOfYear, moneyAtBeginningOfYear+cashFlow, yearTimer/yearCompletionTime );

		// Happiness calculation / check
		happiness -= happinessDecreateRate*Time.deltaTime;
		if( happiness <= 0f )
			//TODO: GameOver

		yearTimer += Time.deltaTime;
		if( yearTimer >= yearCompletionTime ) {
			yearTimer = 0f;
			OnYearCompleted();
		}
	}

	public void SetCashFlow() {
		// TODO:Look at all things that could affect this and do it
	}

	public void AddLoan(string loanName, float loanAmount) {

	}

	public void IncreaseLoanPaymentRate (Loan thisLoan) {
		thisLoan.annualPayment *= 2f;
	}

	public void PayOffLoan(int index) {

	}

	public void AddBusiness () {

	}

	public void SellBusiness (int index) {

	}


}
