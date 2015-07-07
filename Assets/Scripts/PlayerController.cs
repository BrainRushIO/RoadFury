using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	float strafeSpeed = .1f;
	float playerBounds = 4f;
	int age = 16, money = 1000;
	public Text moneyText, ageText, burnRateText;
	public Slider happiness;

	float burnRateValue = -200;
	float loseADollarRate = 0;
	float loseADollarTimer = 0f;
	float ageAYearRate = 10f;
	float ageAYearTimer = 0f;

	float attrition = 0.0001f;
	//TODO add attrition rate increases depending on if player gets wife or gf or not

	void Start () {
		SetNewBurnRate(-200);
	}

	// Update is called once per frame
	void Update () {

		//Display
		ageText.text = "Age: " + age.ToString ();
		moneyText.text = "Money: $" + money.ToString ();
		burnRateText.text = "Burn Rate: $" + Mathf.CeilToInt (burnRateValue).ToString ();
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
		if (loseADollarTimer > Mathf.Abs(loseADollarRate) && loseADollarRate < 0) {
			loseADollarTimer = 0;
			money--;
		}
		else if (loseADollarTimer > Mathf.Abs(loseADollarRate) && loseADollarRate > 0) {
			loseADollarTimer = 0;
			money++;
		}


	}

	public void SetNewBurnRate(float newBurnRate){
		loseADollarRate = ageAYearRate / (newBurnRate);
		print ("loseADollarRate" + loseADollarRate); 
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "powerUp") {
			PowerUp temp = other.gameObject.GetComponent<PowerUp>();
			if (temp.cost!=0f) {
				money+=temp.cost;
			}

			if (temp.burnRate!=0f) {
				burnRateValue+=temp.burnRate;
				SetNewBurnRate(burnRateValue);
			}

			if (temp.happiness!=0f) {
				happiness.value+=temp.happiness;
			}
			Destroy(other.gameObject);
		}
	}
}
