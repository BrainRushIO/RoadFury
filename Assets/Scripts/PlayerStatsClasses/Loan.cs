using UnityEngine;
using System.Collections;

public class Loan : MonoBehaviour {

	int indexID;
	float loanAmount;
	float interestRate;
	float annualPayment;

	void Start () {
	
	}

	void Update () {
	
	}

	private void AnnualUpdate() {
		loanAmount = (loanAmount *= interestRate) - annualPayment;
		PlayerStats.s_instance.money -= annualPayment;
	}

	private void PayOffLoan() {
		PlayerStats.s_instance.money -= loanAmount;
		// TODO: PlayerStats.removeLoan( indedID );
	}

	private void IncreaseAnnualPayment() {
		annualPayment *= 2f;
	}
}
