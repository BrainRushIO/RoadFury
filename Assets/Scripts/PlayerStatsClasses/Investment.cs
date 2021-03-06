﻿using UnityEngine;
using System.Collections;

public class Investment {

	public static float MAX_MONEY_ADDED_PER_YEAR_TO_IRA = 5000f;
	private const float YEARS_BEFORE_IRA_LIQUIDATION = 5f;
	private const int MAX_LIQUIDATIONS_PER_YEAR = 5;
	private static int investmentNumber = 0;

	public float annualGrowthRate = .1f;	// TODO set these values randomly
	public float monetaryValue;
	public int initializationYear;
	public string investmentName;
	public float moneyAddedThisYear = 0f;
	public int liquidationsThisYear = 0;

	public enum InvestmentType {Stock, Mutual, IRA};
	public InvestmentType thisInvestmentType;
	
	public Investment() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
		investmentName = "Your Investment #" + investmentNumber.ToString();
		investmentNumber++;
	}

	~Investment() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	public void SetMonetaryValue( float value ) {
		monetaryValue = value;
	}

	void AnnualUpdate() {
		// TODO Fill in these cases
		switch( thisInvestmentType ) {
		case InvestmentType.Stock:
			break;
		case InvestmentType.Mutual:
			break;
		case InvestmentType.IRA:
			moneyAddedThisYear = 0f;
			break;
		}
		monetaryValue *= annualGrowthRate;
		liquidationsThisYear = 0;
	}

	public void AddMoreMoney( float amount ) {
		monetaryValue += amount;
		PlayerStats.s_instance.money -= amount;
		Debug.Log ("TOTAL MONEY OF " + investmentName + " is " + monetaryValue);
	}
	/// <summary>
	/// Liquidate by the specified percentage.
	/// </summary>
	/// <param name="percentage">Percentage to liquidate From 0.01 to 1.0</param>
	public void Liquidate( float percentage ) {
		if( liquidationsThisYear < MAX_LIQUIDATIONS_PER_YEAR ) {
			Debug.Log  ("Monetary value " + monetaryValue);
			if( thisInvestmentType == InvestmentType.IRA && initializationYear < initializationYear + YEARS_BEFORE_IRA_LIQUIDATION ) {
				GUIManager.s_instance.DisplayNotification( "Notice!", "You have to wait 5 years before you can liquidate an IRA" );
				return;
			}

			//	Mathf.Clamp01( percentage );
			float modifyAmount = monetaryValue*percentage;
			Debug.Log ("modify amount " + modifyAmount);
			PlayerStats.s_instance.money += modifyAmount;
			monetaryValue -= modifyAmount;

			if( percentage == 1f ) {
				Debug.Log( "Removing "+investmentName+" from investments." );
				PlayerStats.s_instance.playerInvestments.Remove( this );
			}
			liquidationsThisYear++;
		} else {
			GUIManager.s_instance.DisplayNotification( "Notice!", "You can't liquidate more than"+MAX_LIQUIDATIONS_PER_YEAR+" times in one year." );
		}
	}
}
