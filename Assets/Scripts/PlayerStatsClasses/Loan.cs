using UnityEngine;
using System.Collections;

public class Loan : MonoBehaviour {
	
	public float loanAmount;
	public float initLoanAmount;
	public float interestRate = 1.05f;
	public float annualPaymentPercentage = .1f;
	public string loanName;

	//subscribe to year event
	void OnEnable() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
	}

	void OnDisable() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	private void AnnualUpdate() {
		loanAmount = (loanAmount *= interestRate) - annualPaymentPercentage*initLoanAmount;
		PlayerStats.s_instance.money -= annualPaymentPercentage*initLoanAmount;
	}
}
