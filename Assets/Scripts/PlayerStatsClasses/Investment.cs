using UnityEngine;
using System.Collections;

public class Investment : MonoBehaviour {

	public float annualGrowthRate;	// TODO set these values
	float monetaryValue;
	int initializationYear;
	public string investmentName;
	public enum InvestmentType {Stock, Mutual, IRA};
	public InvestmentType thisInvestmentType;
	void OnEnable() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
	}

	void OnDisable() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	public void SetMonetaryValue( float value ) {
		monetaryValue = value;
	}

	void AnnualUpdate() {
		monetaryValue *= annualGrowthRate;
	}

	void AddMoreMoney( float percentage ) {
		float percentToValue = PlayerStats.s_instance.money * percentage;
		monetaryValue += percentToValue;
		PlayerStats.s_instance.money -= percentToValue;
	}

	public void Liquidate() {
		PlayerStats.s_instance.money = monetaryValue;
		PlayerStats.s_instance.playerInvestments.Remove( this );
		Destroy( this );
	}
}
