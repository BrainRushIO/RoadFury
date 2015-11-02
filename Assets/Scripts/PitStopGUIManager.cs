using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PitStopGUIManager : MonoBehaviour {

	
	public GameObject pitStopCanvas;
	public enum PitStopState {Main, Business, SelectBusiness, StartNewBusiness, Loans, SelectLoan, Investment, SelectInvestment, RealEstate, SelectRealEstate};
	public PitStopState currentPitStopState = PitStopState.Main;
	public List<Text> allTextObjects;


	void Start () {
		DisplayCurrentMenu ();
	}

	void Update() {
		print (currentPitStopState);
	}

	public int lastIndexClicked; //used to refer to index in list of player Loan/Business/etc...

	public void HandlePitStopClick(int index) {
		print ("handle click");
		if (currentPitStopState == PitStopState.Loans ||
		    currentPitStopState == PitStopState.Investment ||
		    currentPitStopState == PitStopState.RealEstate ||
		    currentPitStopState == PitStopState.Business) lastIndexClicked = index;

		switch (currentPitStopState) {
		case PitStopState.Main :
			switch (index) {
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
			
			case 7 :
				//goback to road
				GameManager.s_instance.SwitchToGame();
				break;
			}
			break;
			#region Loans
		case PitStopState.Loans :
			if (lastIndexClicked < 7 && PlayerStats.s_instance.playerLoans.Count > lastIndexClicked) {
				currentPitStopState = PitStopState.SelectLoan;
			}
			else if (index == 7){
				currentPitStopState = PitStopState.Main;
			}
			break;

		case PitStopState.SelectLoan :
			if (index == 2) {
				PlayerStats.s_instance.PayOffLoan(lastIndexClicked);
			}
			else if (index == 3) {
				PlayerStats.s_instance.IncreaseLoanPaymentRate(lastIndexClicked);
			}
			else if (index == 7) {
				print ("BACK TO LOAN");
				currentPitStopState = PitStopState.Loans;
			}

			break;
			#endregion
			#region Investment
		case PitStopState.Investment :
			if (lastIndexClicked < 4 && PlayerStats.s_instance.playerInvestments.Count > lastIndexClicked) {
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
			} else if (lastIndexClicked == 7) { 
				currentPitStopState = PitStopState.Main;
			}
			break;
		case PitStopState.SelectInvestment :
			if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Stock) {
				if (index == 2) {
					PlayerStats.s_instance.AddMoneyToInvestment(lastIndexClicked, 10000);
				}
				else if (index == 3) {
					PlayerStats.s_instance.AddMoneyToInvestment(lastIndexClicked, 500000);
				}
				else if (index == 4) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, 1f);
					currentPitStopState = PitStopState.Investment;
				}
				else if (index == 5) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, .5f);
				}
				else if (index == 6) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, .1f);

				} else if (index == 7) { 
					currentPitStopState = PitStopState.Investment;
				}
			}
			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Mutual) {
				if (index == 2) {
					PlayerStats.s_instance.AddMoneyToInvestment(lastIndexClicked, 1000);
				}
				else if (index == 3) {
					PlayerStats.s_instance.AddMoneyToInvestment(lastIndexClicked, 50000);
				}
				else if (index == 4) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, 1f);
					currentPitStopState = PitStopState.Investment;
				}
				else if (index == 5) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, .5f);
					//TODO make sure ppl cant spam this shit
				}
				else if (index == 6) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, .1f);

				} else if (index == 7) { 
					currentPitStopState = PitStopState.Main;
				}
			}

			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.IRA) {
				if (index == 2) {
					PlayerStats.s_instance.AddMoneyToInvestment(lastIndexClicked, 100f);
				}
				else if (index == 3) {
					PlayerStats.s_instance.AddMoneyToInvestment(lastIndexClicked, 1000f);
				}
				else if (index == 6) {
					PlayerStats.s_instance.LiquidateInvestment(lastIndexClicked, 1f);
					currentPitStopState = PitStopState.Investment;
				} else if (index == 7) { 
					currentPitStopState = PitStopState.Main;
				}
			}
			break;
			#endregion
			#region Business
		case PitStopState.Business :
			print (lastIndexClicked + " LIC");
			if (lastIndexClicked < 4 && PlayerStats.s_instance.playerBusinesses[lastIndexClicked]!=null ) {
				currentPitStopState = PitStopState.SelectBusiness;
			}
			else if (lastIndexClicked > 3 && lastIndexClicked < 7) {
				PlayerStats.s_instance.AddBusiness(lastIndexClicked - 4);
			}
			else if (lastIndexClicked == 7) {
				currentPitStopState = PitStopState.Main;
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
			#endregion
			#region RealEstate
		case PitStopState.RealEstate :
			if (lastIndexClicked < 4 && PlayerStats.s_instance.playerRealEstate[lastIndexClicked]!=null) {
				currentPitStopState = PitStopState.SelectRealEstate;
			}
			else if (index == 4) {
				PlayerStats.s_instance.AddRealEstate(0);
			}
			else if (index == 5) {
				PlayerStats.s_instance.AddRealEstate(1);
			}
			else if (index == 6) {
				PlayerStats.s_instance.AddRealEstate(2);
			}
			else if (index == 7) {
				currentPitStopState = PitStopState.Main;
			}
			break;
		case PitStopState.SelectRealEstate :
			if (index == 6) {
				PlayerStats.s_instance.SellRealEstate(lastIndexClicked);
				currentPitStopState = PitStopState.RealEstate;
			}
			else if (index == 7) {
				currentPitStopState = PitStopState.RealEstate;
			}
			break;
			#endregion
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
			allTextObjects[7].text = "Back to the Road";
			break;
			#region LoanOptions
		case PitStopState.Loans :
			int playerLoansCount = PlayerStats.s_instance.playerLoans.Count;
			print (playerLoansCount + " Player Loan Count");
			for (int i = 0; i < playerLoansCount; i++) {
				allTextObjects[i].text = PlayerStats.s_instance.playerLoans[i].loanName;
			}
			allTextObjects[7].text = "Back";
			break;
		case PitStopState.SelectLoan :
			allTextObjects[0].text = PlayerStats.s_instance.playerLoans[lastIndexClicked].loanName;
			allTextObjects[1].text = "Interest Rate: " + PlayerStats.s_instance.playerLoans[lastIndexClicked].interestRate;
			allTextObjects[2].text = "Amount Left: " + PlayerStats.s_instance.playerLoans[lastIndexClicked].loanAmount;
			allTextObjects[5].text = "Annual Payment: " + PlayerStats.s_instance.playerLoans[lastIndexClicked].annualPaymentPercentage*100+"%";
			allTextObjects[6].text = "Pay Off Loans";
			allTextObjects[7].text = "Back";
			break;
			#endregion
			#region InvestmentOptions
		case PitStopState.Investment :
			int playerInvestmentsCount = PlayerStats.s_instance.playerInvestments.Count;
			for (int i = 0; i < playerInvestmentsCount; i++) {
				allTextObjects[i].text = PlayerStats.s_instance.playerInvestments[i].investmentName;
			}		
			allTextObjects[4].text = "Purchase Stocks $50K";
			allTextObjects[5].text = "Purchase Mutual Fund $5K";
			allTextObjects[6].text = "Setup IRA $100";
			allTextObjects[7].text = "Back";
			break;

		case PitStopState.SelectInvestment :
			if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.IRA) {
				allTextObjects[2].text = "Add $100";
				allTextObjects[3].text = "Add $1K";
				allTextObjects[6].text = "Liquidate IRA";
			}
			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Stock) {
				allTextObjects[2].text = "Add $10K";
				allTextObjects[3].text = "Add $500K";
				allTextObjects[4].text = "Liquidate All Stock";
				allTextObjects[5].text = "Liquidate 50% Stock";
				allTextObjects[6].text = "Liquidate 10% Stock";

			}
			else if (PlayerStats.s_instance.playerInvestments[lastIndexClicked].thisInvestmentType == Investment.InvestmentType.Mutual) {
				allTextObjects[2].text = "Add $1K";
				allTextObjects[3].text = "Add $50K";
				allTextObjects[4].text = "Liquidate Mutual Fund";
				allTextObjects[5].text = "Liquidate 50% of Mutual Fund";
				allTextObjects[6].text = "Liquidate 10% of Mutual Fund";
			}
			allTextObjects[0].text = PlayerStats.s_instance.playerInvestments[lastIndexClicked].investmentName;
			allTextObjects[1].text = "$"+ NumberToString.Convert (PlayerStats.s_instance.playerInvestments[lastIndexClicked].monetaryValue) + 
				" Growing " + PlayerStats.s_instance.playerInvestments[lastIndexClicked].annualGrowthRate*100 + "% per Year";
			allTextObjects[7].text = "Back";

			break;
			#endregion
			#region BusinessOptions
		case PitStopState.Business :
			int playerBusinessCount = PlayerStats.s_instance.playerBusinesses.Count;
			for (int i = 0; i < playerBusinessCount; i++) {
				print (playerBusinessCount + "player business count");
				allTextObjects[i].text = PlayerStats.s_instance.playerBusinesses[i].businessName;
			}
			allTextObjects[4].text = "Start Business for 10k";
			allTextObjects[5].text = "Start Business for 100k";
			allTextObjects[6].text = "Start Business for 1M";
			allTextObjects[7].text = "Back";

			break;
			
		case PitStopState.SelectBusiness :
			allTextObjects[0].text = PlayerStats.s_instance.playerBusinesses[lastIndexClicked].businessName;
			allTextObjects[1].text = "Revenue: " + PlayerStats.s_instance.playerBusinesses[lastIndexClicked].revenueStream;
			allTextObjects[2].text = "Work Overtime";
			allTextObjects[3].text = "Sell Business for " + PlayerStats.s_instance.playerBusinesses[lastIndexClicked].valuation;
			allTextObjects[7].text = "Back";

			break;

			#endregion
			#region RealEstateOptions
		case PitStopState.RealEstate :
			int playerRealEstateCount = PlayerStats.s_instance.playerRealEstate.Count;
			for (int i = 0; i < playerRealEstateCount; i++) {
				allTextObjects[i].text = PlayerStats.s_instance.playerRealEstate[i].realEstateName;
			}
			allTextObjects[4].text = "Buy House for 50K";
			allTextObjects[5].text = "Buy House for 500K";
			allTextObjects[6].text = "Buy House for 5M";
			allTextObjects[7].text = "Back";
		
			break;
		case PitStopState.SelectRealEstate :
			allTextObjects[0].text = PlayerStats.s_instance.playerRealEstate[lastIndexClicked].realEstateName;
			allTextObjects[1].text = "Liquid Value: $" + PlayerStats.s_instance.playerRealEstate[lastIndexClicked].realEstateValue;

			allTextObjects[6].text = "Sell for: $" + PlayerStats.s_instance.playerRealEstate[lastIndexClicked].realEstateValue;;
			allTextObjects[7].text = "Back";
			
			break;
			#endregion
		}

	}


}
