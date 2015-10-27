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

			break;


		}


	}


}
