using UnityEngine;
using System.Collections;

public class Family : MonoBehaviour {

	public GameObject wife, kid1, kid2, dog;
	public bool isMarried;
	public bool isDivorced;

	private float familyCost;		// per person at some value TODO: Set some value
	
	void Update () {
		// Family cost

		//if married
		// money --
	}

	public void GetMarried() {
		PlayerStats.s_instance.happiness++; // TODO:Eric set this to a value
	}
}
