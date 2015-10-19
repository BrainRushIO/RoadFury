using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	float strafeSpeed = .1f, moveSpeed = .1f;
	float playerBounds = 4f;
	public GameObject currentRoadSection;
	bool isOnHorizontalRoad = false;
	float attrition = 0.0001f;
	private Animator anim;

	bool isRotateLerp;
	float rotateLerpTimer;
	float rotateLerpDuration = 1f;
	Quaternion startLerp, endLerp;

	//TODO add attrition rate increases depending on if player gets wife or gf or not
	void Start(){
		anim = GetComponent<Animator> ();
	
	}

	void StartRotateLerp() {
		startLerp = transform.rotation;
		rotateLerpTimer = Time.time;
		isRotateLerp = true;
	}


	void Update () {
		if (isRotateLerp) {
			float perc = (Time.time - rotateLerpTimer) / rotateLerpDuration;
			transform.rotation = Quaternion.Lerp(startLerp, endLerp, perc);
			if (perc > .99) {
				perc = 1;
				Quaternion.Lerp(startLerp, endLerp,perc);
				isRotateLerp = false;
			}
		}
		if (GameManager.s_instance.currentGameState == GameState.Cutscene) {
			transform.Translate (Vector3.forward*moveSpeed);
		}
		if (GameManager.s_instance.currentGameState == GameState.Playing) {
			float horizontal = Input.GetAxis ("Horizontal");
	
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);
				if (touch.position.x > Screen.width / 2) {
					//go right
					horizontal = 1f;


				} else if (touch.position.x < Screen.width / 2) {
					horizontal = -1f;
			
				}
			}

			anim.SetFloat ("Turn", horizontal * .7f);
			transform.Translate (Vector3.forward*moveSpeed);


			//bound player
			if (!isOnHorizontalRoad) {
				if (Mathf.Abs (transform.position.x + (horizontal * strafeSpeed)) < playerBounds + currentRoadSection.transform.position.x) {
					transform.Translate (horizontal * strafeSpeed, 0, 0);
				}
			} else {
				if (Mathf.Abs (transform.position.z - currentRoadSection.transform.position.z - (horizontal * strafeSpeed)) < playerBounds) {
					transform.Translate (horizontal * strafeSpeed, 0, 0);
				}

			}
		} else {
			anim.SetFloat ("Turn", 0);
		}

	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "powerUp") {
			PowerUp temp = other.gameObject.GetComponent<PowerUp> ();
			if (temp.cost != 0f) {
				GameManager.s_instance.money += temp.cost;
			}

			if (temp.burnRate != 0f) {
				GameManager.s_instance.burnRateValue += temp.burnRate;
				GameManager.s_instance.SetNewBurnRate (GameManager.s_instance.burnRateValue);
			}

			if (temp.happiness != 0f) {
				GameManager.s_instance.happiness.value += temp.happiness;
			}
			if (other.gameObject.GetComponent<PowerUp> ().thisPowerUpType == PowerUp.PowerUpType.None) {
				Destroy (other.gameObject);
			}
		} else if (other.tag == "branch") {
			GameManager.s_instance.currentGUIseries = other.GetComponent<RoadBranch>().GUIObject;
			GameManager.s_instance.currentGUIseries.SetActive(true);
			GameManager.s_instance.SwitchToCutscene();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "branch") {
			//rotating player 90 degrees depending on what it says
			endLerp = transform.rotation * Quaternion.Euler(0,other.GetComponent<RoadBranch>().degreeToTurnBy,0);
			StartRotateLerp();
			currentRoadSection = other.GetComponent<RoadBranch>().nextRoadBranch;
			isOnHorizontalRoad = !isOnHorizontalRoad;

		}
	}
}
