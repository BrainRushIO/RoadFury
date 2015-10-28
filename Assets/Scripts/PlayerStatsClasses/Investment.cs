using UnityEngine;
using System.Collections;

public class Investment : MonoBehaviour {

	public static float MaxMoneyAddedPerYear = 5000f;

	public float annualGrowthRate;	// TODO set these values
	public float monetaryValue;
	public int initializationYear;
	public string investmentName;
	public float moneyAddedThisYear = 0f;

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
		moneyAddedThisYear = 0f;
	}

	public void AddMoreMoney( float amount ) {
		monetaryValue += amount;
		PlayerStats.s_instance.money -= amount;
	}
	/// <summary>
	/// Liquidate by the specified percentage.
	/// </summary>
	/// <param name="percentage">Percentage to liquidate From 0.01 to 1.0</param>
	public void Liquidate( float percentage ) {
		Mathf.Clamp01( percentage );
		float modifyAmount = monetaryValue*percentage;
		PlayerStats.s_instance.money += modifyAmount;
		monetaryValue -= modifyAmount;

		if( percentage == 1f ) {
			Debug.Log( "Removing "+investmentName+" from investments." );
			PlayerStats.s_instance.playerInvestments.Remove( this );
			Destroy( this );
		}
	}
}
