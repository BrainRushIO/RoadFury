using UnityEngine;
using System.Collections;

public class Family : MonoBehaviour {

	public GameObject wife, kid1, kid2, dog;
	public bool isMarried;
	public bool isDivorced;

	private float familyCost;		// per person at some value TODO: Set some value

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Family cost
		//if married
		// money --
		// if( divorced )
		// Playerstats.money -= numKids * childSupport
	}

	public void GetMarried() {
		PlayerStats.s_instance.happiness++; // TODO:Eric set this to a value
	}

	public void GetDivorced() {
		PlayerStats.s_instance.money /= 2f;
	}
}
