using UnityEngine;
using System.Collections;

public class Loan : MonoBehaviour {
	private static int currentIndex = 0;

	public int indexID;
	public float loanAmount;
	public float interestRate;
	public float annualPayment;
	public string loanName;

	void Awake() {
		indexID = Loan.GenerateIndex();
	}

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

	public static int GenerateIndex() {
		// Returns index and adds one for the next generation
		currentIndex++;
		return currentIndex-1;
	}
}
