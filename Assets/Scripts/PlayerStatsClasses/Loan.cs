using UnityEngine;
using System.Collections;

public class Loan : MonoBehaviour {
	
	public float loanAmount;
	public float interestRate;
	public float annualPayment;
	public string loanName;

	//subscribe to year event
	void OnEnable() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
	}

	void OnDisable() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	private void AnnualUpdate() {
		loanAmount = (loanAmount *= interestRate) - annualPayment;
		PlayerStats.s_instance.money -= annualPayment;
	}
}
