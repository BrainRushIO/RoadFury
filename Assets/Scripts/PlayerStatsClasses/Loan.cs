using UnityEngine;
using System.Collections;

public class Loan {
	
	public float loanAmount;
	public float initLoanAmount;
	public float interestRate = 1.05f;
	public float annualPaymentPercentage = 0.1f;
	public string loanName;

	//subscribe to year event
	public Loan() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
	}

	~Loan() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	public void SetInitialLoanAmount( float newAmount ) {
		initLoanAmount = newAmount;
		loanAmount = initLoanAmount;
	}

	public void PayLoanAmount( float amount ) {
		loanAmount -= amount;
		PlayerStats.s_instance.money -= amount;
	}

	private void AnnualUpdate() {
		loanAmount = (loanAmount * interestRate) - annualPaymentPercentage*initLoanAmount;
		PlayerStats.s_instance.money -= annualPaymentPercentage*initLoanAmount;
	}
}
