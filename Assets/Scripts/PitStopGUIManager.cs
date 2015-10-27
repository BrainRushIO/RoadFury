using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PitStopGUIManager : MonoBehaviour {

	
	public GameObject pitStopCanvas;
	public enum PitStopState {Main, Family, Business, Loan, Investment, RealEstate};
	public PitStopState currentPitStopState = PitStopState.Main;
	public List<Text> allTextObjects;

	public void SwitchToSubMenu () {
		
	}
	
	public void SwitchToDecision () {
		
	}
	
	public void SwitchToMenu () {
		
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
		case PitStopState.Loan :
			allTextObjects[0].text = "Loans";
			allTextObjects[1].text = "Investments";
			allTextObjects[2].text = "Businesses";
			allTextObjects[3].text = "Real Estate";
			allTextObjects[4].text = "Family";



			
			
			

			break;


		}


	}


}
