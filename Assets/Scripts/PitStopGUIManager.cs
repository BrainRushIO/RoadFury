using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PitStopGUIManager : MonoBehaviour {

	
	public GameObject pitStopCanvas;
	public enum PitStopState {Main, Family, Business, Loans, SelectLoan, Investment, RealEstate};
	public PitStopState currentPitStopState = PitStopState.Main;
	public List<Text> allTextObjects;

	public int lastIndexClicked;

	public void HandlePitStopClick(int index) {

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


			break;
			
			
			
			
			
			

			break;


		}


	}


}
