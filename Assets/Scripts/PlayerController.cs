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

	
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			if (touch.position.x > Screen.width/2) {
				//go right
				horizontal = 1f;
			}
			else if (touch.position.x < Screen.width/2) {
				horizontal = -1f;
			}
		}

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
			if (other.gameObject.GetComponent<PowerUp>().thisPowerUpType == PowerUp.PowerUpType.None){
				Destroy(other.gameObject);
			}
		}
	}
}
