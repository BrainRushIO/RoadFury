using UnityEngine;
using System.Collections;

public class Loan : MonoBehaviour {

	public int indexID;
	public float loanAmount;
	public float interestRate;
	public float annualPayment;
	public string loanName;

	//subscribe to year event

	private void AnnualUpdate() {
		loanAmount = (loanAmount *= interestRate) - annualPayment;
		PlayerStats.s_instance.money -= annualPayment;
	}
	
}
