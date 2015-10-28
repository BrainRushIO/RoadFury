using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PitStopGUIManager : MonoBehaviour {

	
	public GameObject pitStopCanvas;
	public enum PitStopState {Main, Family, Business, SelectBusiness, StartNewBusiness, Loans, SelectLoan, Investment, SelectInvestment, RealEstate};
	public PitStopState currentPitStopState = PitStopState.Main;
	public List<Text> allTextObjects;

	public int lastIndexClicked; //used to refer to index in list of player Loan/Business/etc...

	public void HandlePitStopClick(int index) {
		if (currentPitStopState == PitStopState.Loans ||
		    currentPitStopState == PitStopState.Investment ||
		    currentPitStopState == PitStopState.RealEstate ||
		    currentPitStopState == PitStopState.Business) lastIndexClicked = index;

		switch (currentPitStopState) {
		case PitStopState.Main :
			switch (lastIndexClicked) {
			case 0 :
				currentPitStopState = PitStopState.Loans;
				break;
			case 1 :
				currentPitStopState = PitStopState.Investment;
				break;
			case 2 :
				currentPitStopState = PitStopState.Business;
				break;
			case 3 :
				currentPitStopState = PitStopState.RealEstate;
				break;
			case 4 :
				currentPitStopState = PitStopState.Family;
				break;
			case 7 :
				//goback to road
				break;
			}
			break;
		case PitStopState.Loans :
			if (lastIndexClicked < 7 && PlayerStats.s_instance.playerLoans[lastIndexClicked]!=null) {
				currentPitStopState = PitStopState.SelectLoan;
			}
			else {
				currentPitStopState = PitStopState.Main;
			}
			break;

		case PitStopState.SelectLoan :
			if (lastIndexClicked == 2) {
				PlayerStats.s_instance.PayOffLoan(lastIndexClicked);
			}
			else if (lastIndexClicked == 3) {
				PlayerStats.s_instance.IncreaseLoanPaymentRate(lastIndexClicked);
			}
			else if (lastIndexClicked == 7) {
				currentPitStopState = PitStopState.Loans;
			}

			break;
		case PitStopState.Investment :
			if (lastIndexClicked < 4 && PlayerStats.s_instance.playerInvestments[lastIndexClicked]!=null) {
				currentPitStopState = PitStopState.SelectInvestment;
			}
			else if (lastIndexClicked == 4) {
				PlayerStats.s_instance.AddInvestment(Investment.InvestmentType.Stock);
			}

			else if (lastIndexClicked == 5) {
				PlayerStats.s_instance.AddInvestment(Investment.InvestmentType.Mutual);
			}

			else if (lastIndexClicked == 6) {
				PlayerStats.s_instance.AddInvestment(Investment.InvestmentType.IRA);
			} else {
				currentPitStopState = PitStopState.Main;
			}
			break;
		case PitStopState.SelectInvestment :
			if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Stock) {
				if (index == 2) {
					
				}
			}
			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Mutual) {
				
			}

			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.IRA) {
				
			}
			break;

		case PitStopState.Business :
			if (lastIndexClicked < 6 && PlayerStats.s_instance.playerBusinesses[lastIndexClicked]!=null) {
				currentPitStopState = PitStopState.SelectBusiness;
			}
			else if (lastIndexClicked == 6) {
				currentPitStopState = PitStopState.StartNewBusiness;
			}
			else if (lastIndexClicked == 7) {
				currentPitStopState = PitStopState.Business;
			}
			else {
				Debug.LogWarning("Index out of bounds in PitStopGUIManager");
			}
			break;
		case PitStopState.SelectBusiness :
			if (index == 2) {
				PlayerStats.s_instance.WorkOvertime(lastIndexClicked);
			}
			else if (index == 3) {
				PlayerStats.s_instance.SellBusiness(lastIndexClicked);
				currentPitStopState = PitStopState.Business;
			}
			else if (index == 7) {
				currentPitStopState = PitStopState.Business;
			}
			break;
		case PitStopState.StartNewBusiness :
			if (index < 3) {
				if (PlayerStats.s_instance.CanStartNewBusiness(index)){
					currentPitStopState = PitStopState.Business;
				}
			}
			else if (index == 7) {
				currentPitStopState = PitStopState.Business;
			}
			break;
		}
		DisplayCurrentMenu();
	}

	public void DisplayCurrentMenu() {
		foreach (Text x in allTextObjects) {
			x.text = "";
		}

		switch (currentPitStopState) {
		case PitStopState.Main :
			allTextObjects[0].text = "Loans";
			allTextObjects[1].text = "Investments";
			allTextObjects[2].text = "Businesses";
			allTextObjects[3].text = "Real Estate";
			allTextObjects[4].text = "Family";
			allTextObjects[7].text = "Back to the Road";
			break;
		case PitStopState.Loans :
			int playerLoansCount = PlayerStats.s_instance.playerLoans.Count;
			for (int i = 0; i < playerLoansCount; i++) {
				allTextObjects[i].text = PlayerStats.s_instance.playerLoans[i].loanName;
			}
			allTextObjects[7].text = "Back";
			break;
		case PitStopState.SelectLoan :
			allTextObjects[0].text = PlayerStats.s_instance.playerLoans[lastIndexClicked].loanName;
			allTextObjects[1].text = "Annual Payment: " + PlayerStats.s_instance.playerLoans[lastIndexClicked].annualPaymentPercentage*100+"%";
			allTextObjects[2].text = "Pay Off Loan";
			allTextObjects[3].text = "Double Annual Payment";
			allTextObjects[7].text = "Back";
			break;

		case PitStopState.Investment :
			int playerInvestmentsCount = PlayerStats.s_instance.playerInvestments.Count;
			for (int i = 0; i < playerInvestmentsCount; i++) {
				allTextObjects[i].text = PlayerStats.s_instance.playerInvestments[i].investmentName;
			}		
			allTextObjects[4].text = "Purchase Stocks $50,000";
			allTextObjects[5].text = "Purchase Mutual Fund $5,000";
			allTextObjects[6].text = "Setup IRA $100";
			allTextObjects[7].text = "Back";
			break;

		case PitStopState.SelectInvestment :
			if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.IRA) {

				allTextObjects[2].text = "Add $100";
				allTextObjects[3].text = "Add $1000";
				allTextObjects[4].text = "Max Out ($5000 limit per Year)";
				allTextObjects[5].text = "Close IRA";
			}
			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Stock) {
				allTextObjects[2].text = "Add $10,000";
				allTextObjects[3].text = "Add $500,000";
				allTextObjects[4].text = "Liquidate All Stocks";
				allTextObjects[5].text = "Liquidate 50% Stocks";
				allTextObjects[6].text = "Liquidate 10% Stocks";

			}
			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Mutual) {
				allTextObjects[2].text = "Add $1,000";
				allTextObjects[3].text = "Add $50,000";
				allTextObjects[4].text = "Liquidate Mutual Fund";
				allTextObjects[5].text = "Liquidate 50% of Mutual Fund";
				allTextObjects[6].text = "Liquidate 10% of Mutual Fund";
			}
			allTextObjects[0].text = PlayerStats.s_instance.playerInvestments[lastIndexClicked].investmentName;
			allTextObjects[1].text = "Annual Growth Rate: " + PlayerStats.s_instance.playerInvestments[lastIndexClicked].annualGrowthRate*100 + "%";
			allTextObjects[7].text = "Back";

			break;

		case PitStopState.Business :
			int playerBusinessCount = PlayerStats.s_instance.playerBusinesses.Count;
			for (int i = 0; i < playerBusinessCount; i++) {
				allTextObjects[i].text = PlayerStats.s_instance.playerBusinesses[i].businessName;
			}
			allTextObjects[6].text = "Start a new Business";
			allTextObjects[7].text = "Back";

			break;
			
		case PitStopState.SelectBusiness :
			allTextObjects[0].text = PlayerStats.s_instance.playerBusinesses[lastIndexClicked].businessName;
			allTextObjects[1].text = "Revenue: " + PlayerStats.s_instance.playerBusinesses[lastIndexClicked].revenueStream;
			allTextObjects[2].text = "Work Overtime";
			allTextObjects[3].text = "Sell Business for " + PlayerStats.s_instance.playerBusinesses[lastIndexClicked].valuation;
			allTextObjects[7].text = "Back";

			break;

		case PitStopState.StartNewBusiness :
			allTextObjects[0].text = "Invest 10K";
			allTextObjects[1].text = "Invest 100K";
			allTextObjects[2].text = "Invest 1M";
			allTextObjects[7].text = "Back";

			break;
		}


	}


}
