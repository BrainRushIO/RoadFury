using UnityEngine;
using System.Collections;

public class Loan : MonoBehaviour {
	
	public float loanAmount;
	public float initLoanAmount;
	public float interestRate = 1.05f;
	public float annualPaymentPercentage = 0.1f;
	public string loanName;

	//subscribe to year event
	void OnEnable() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
	}

	void OnDisable() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	public void SetInitialLoanAmount( float newAmount ) {
		initLoanAmount = newAmount;
	}

	public void SetAnnualPaymentPercentage( float newPercentage ) {
		annualPaymentPercentage = newPercentage;
	}

	private void AnnualUpdate() {
		loanAmount = (loanAmount * interestRate) - annualPaymentPercentage*initLoanAmount;
		PlayerStats.s_instance.money -= annualPaymentPercentage*initLoanAmount;
	}
}
