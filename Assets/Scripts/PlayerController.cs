using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	float strafeSpeed = .1f;
	float playerBounds = 4f;


	float attrition = 0.0001f;
	//TODO add attrition rate increases depending on if player gets wife or gf or not

	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");

		//bound player
		if (Mathf.Abs (transform.position.x + (horizontal * strafeSpeed)) < playerBounds) {
			transform.Translate(horizontal * strafeSpeed, 0, 0);
		}

	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "powerUp") {
			PowerUp temp = other.gameObject.GetComponent<PowerUp>();
			if (temp.cost!=0f) {
				GameManager.s_instance.money+=temp.cost;
			}

			if (temp.burnRate!=0f) {
				GameManager.s_instance.burnRateValue+=temp.burnRate;
				GameManager.s_instance.SetNewBurnRate(GameManager.s_instance.burnRateValue);
			}

			if (temp.happiness!=0f) {
				GameManager.s_instance.happiness.value+=temp.happiness;
			}
			Destroy(other.gameObject);
		}
	}
}
