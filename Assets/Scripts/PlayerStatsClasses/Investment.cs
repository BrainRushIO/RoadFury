using UnityEngine;
using System.Collections;

public class Investment : MonoBehaviour {
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
}
