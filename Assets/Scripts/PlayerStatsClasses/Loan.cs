using UnityEngine;
using System.Collections;

public class Loan {

	//Loans have business, school and Real Estate types
	public enum LoanType {Business, School, RealEstate};
	public LoanType thisLoanType;
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


	//only called once at beginning could be refactored who cares for now
	public void InitializeLoan( float newAmount, LoanType type ) {
		initLoanAmount = newAmount;
		loanAmount = initLoanAmount;
		if (type == LoanType.Business) {
			loanName = "Business Loans";
		}
		else if (type == LoanType.School) {
			loanName = "School Loans";
		}
		else if (type == LoanType.RealEstate) {
			loanName = "Real Estate Loans";
		}
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
