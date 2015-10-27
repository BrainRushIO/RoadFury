using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PitStopGUIManager : MonoBehaviour {

	
	public GameObject pitStopCanvas;
	public enum PitStopState {Main, Family, Business, Loans, SelectLoan, Investment, RealEstate};
	public PitStopState currentPitStopState = PitStopState.Main;
	public List<Text> allTextObjects;

	public int lastIndexClicked; //used to refer to index in list of player Loan/Business/etc...

	public void HandlePitStopClick(int index) {
		if (PitStopState == PitStopState.Loans ||
		    PitStopState == PitStopState.Investment ||
		    PitStopState == PitStopState.RealEstate ||
		    PitStopState == PitStopState.Business) lastIndexClicked = index;

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
			
			
			
			
			
			

			break;


		}


	}


}
