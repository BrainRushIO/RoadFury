using UnityEngine;
using System.Collections;

public class Investment : MonoBehaviour {
	private static int currentIndex = 0;

	float growthPerYear;
	float monetaryValue;


	void Start () {
	
	}

	void Update () {
	
	}

	void UpdateAnnual() {
		monetaryValue *= growthPerYear;
	}

	void AddMoreMoney() {

	}

	public static int GenerateIndex() {
		// Returns index and adds one for the next generation
		currentIndex++;
		return currentIndex-1;
	}
}
