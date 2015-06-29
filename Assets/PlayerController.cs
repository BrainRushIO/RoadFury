using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	float strafeSpeed = .04f;
	float playerBounds = 4f;
	int age = 16, money = 1000;
	public Text moneyText, ageText;
	public Slider happiness;

	float loseADollarRate = 0.05f;
	float loseADollarTimer = 0f;
	float ageAYearRate = 10f;
	float ageAYearTimer = 0f;

	float attrition = 0.0001f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Display
		ageText.text = "Age: " + age.ToString ();
		moneyText.text = "Money: $" + money.ToString ();
		happiness.value -= attrition;
		float horizontal = Input.GetAxis ("Horizontal");

		//bound player
		if (Mathf.Abs (transform.position.x + (horizontal * strafeSpeed)) < playerBounds) {
			transform.Translate(horizontal * strafeSpeed, 0, 0);
		}

		//timers
		ageAYearTimer += Time.deltaTime;
		if (ageAYearTimer > ageAYearRate) {
			ageAYearTimer = 0;
			age++;
		}

		loseADollarTimer += Time.deltaTime;
		if (loseADollarTimer > loseADollarRate) {
			loseADollarTimer = 0;
			money--;
		}


	}
}
