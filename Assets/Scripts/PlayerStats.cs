using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
	private static PlayerStats _playerStats;

	public float secondsPerYear = 15f;

	public int age;
	public float money;
	public float cashFlow;
	public float happiness;
	public float happinessDecreateRate;

	public List<Business> playerBusinesses;
	public List<Loan> playerLoans;
	// a year is 15 seconds
	// cashFlow is per year
	// happinessDecreaseRate is applied at the end of the year
	// happiness is between 0 - 1

	public static PlayerStats s_instance { get {return _playerStats;} }

	void Awake() {
		if( _playerStats == null )
			_playerStats = this;
		else if( _playerStats != this ) {
			Destroy(this.gameObject);
			Debug.LogWarning("Deleted duplicate playerstats");
		}
	}

	void Start () {
		age = 16;
		money = 1000f;
		cashFlow = -200f;
		happiness = 0.5f;
		happinessDecreateRate = 0.1f;
	}

	void Update () {
		// Add cashFlow to money
		// Always subtract happiness
		// If happiness <= 0 then game over
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
