using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	float strafeSpeed = .04f;
	float playerBounds = 4f;
	int age = 16, money = 1000;
	public Text moneyText, ageText, burnRateText;
	public Slider happiness;

	float loseADollarRate = 0.05f;
	float loseADollarTimer = 0f;
	float ageAYearRate = 10f;
	float ageAYearTimer = 0f;

	float attrition = 0.0001f;
	//TODO add attrition rate increases depending on if player gets wife or gf or not

	// Update is called once per frame
	void Update () {

		//Display
		ageText.text = "Age: " + age.ToString ();
		moneyText.text = "Money: $" + money.ToString ();
		burnRateText.text = "Burn Rate: -$" + Mathf.CeilToInt (ageAYearRate / loseADollarRate).ToString ();
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

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "powerUp") {
			PowerUp temp = other.gameObject.GetComponent<PowerUp>();
			if (temp.cost!=0f) {
				money-=(int)temp.cost;
			}

			if (temp.multiplier!=0f) {
				loseADollarRate = loseADollarRate/temp.multiplier;
			}

			if (temp.happiness!=0f) {
				happiness.value+=temp.happiness;
			}
			Destroy(other.gameObject);
		}
	}
}
