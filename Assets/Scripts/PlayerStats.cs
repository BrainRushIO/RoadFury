using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public float secondsPerYear = 15f;

	private int age;
	private float money;
	private float cashFlow;
	private float happiness;
	private float happinessDecreateRate;

	// a year is 15 seconds
	// cashFlow is per year
	// happinessDecreaseRate is applied at the end of the year
	// happiness is between 0 - 1

	void Start () {
		age = 16;
		money = 1000f;
		cashFlow = -200f;
		happiness = 0.5f;
		happinessDecreateRate = 0.1f;
	}

	void Update () {
		
	}
}
