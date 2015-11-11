using UnityEngine;
using System.Collections;

public class Family : MonoBehaviour {

	public GameObject wife, kid1, kid2;

	public bool isMarried;
//	private float marriageMoneyCost;			// TODO Set marriage values
//	private float marriageHappinessReward;

	public int numKids = 0;
	public float kidMoneyCost;			// TODO Set child values
	public float kidHappinessReward;

	private float familyCost;

	public void GetMarried() {
		isMarried = true;
//		PlayerStats.s_instance.happiness += marriageHappinessReward;
//		PlayerStats.s_instance.cashFlow -= marriageMoneyCost;

		// TODO Activate wife model
	}

	public void AddKid() {
		numKids++;
		PlayerStats.s_instance.happiness += kidHappinessReward;
		PlayerStats.s_instance.cashFlow -= kidMoneyCost;

		// TODO Activate child models
	}
}
