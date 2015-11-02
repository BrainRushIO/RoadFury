using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Movement and bounds
	private float strafeSpeed = 4.5f, moveSpeed = 8f;
	private float playerBounds = 4f;
	private float pitStopBoundsOffset = 4.5f;
	private bool pitstopEntranceAvailable = false;
	private bool isOnHorizontalRoad = false;
	private Animator anim;
	public GameObject currentRoadSection;
	public Transform respawnPos;

	// Rotation
	private bool isRotateLerp;
	private float rotateLerpTimer;
	private float rotateLerpDuration = 0.8f;
	private Quaternion startLerp, endLerp;

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
			transform.Translate (Vector3.forward*moveSpeed*Time.deltaTime);
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

			anim.SetFloat ("Turn", horizontal * .6f);
			transform.Translate (Vector3.forward*moveSpeed*Time.deltaTime);

			// Calculate if we are entering a pitstop
			float tempPitstopBoundsOffset = 0f;
			if( pitstopEntranceAvailable ) {
				tempPitstopBoundsOffset = pitStopBoundsOffset;
			}

			//bound player
			if (!isOnHorizontalRoad) {
				if (Mathf.Abs (transform.position.x + (horizontal * strafeSpeed*Time.deltaTime)) < playerBounds + tempPitstopBoundsOffset + currentRoadSection.transform.position.x) {
					transform.Translate (horizontal * strafeSpeed*Time.deltaTime, 0, 0);
				}
			} else {
				if (Mathf.Abs (transform.position.z - currentRoadSection.transform.position.z - (horizontal * strafeSpeed*Time.deltaTime)) < playerBounds + tempPitstopBoundsOffset) {
					transform.Translate (horizontal * strafeSpeed*Time.deltaTime, 0, 0);
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
				PlayerStats.s_instance.money += temp.cost;
			}
			if (temp.burnRate != 0f) {
				PlayerStats.s_instance.cashFlow += temp.burnRate;
			}
			if (temp.happiness != 0f) {
				PlayerStats.s_instance.happiness += temp.happiness / 100f;
			}
			if (other.gameObject.GetComponent<PowerUp> ().thisPowerUpType == PowerUp.PowerUpType.None) {
				Destroy (other.gameObject);
			}
		} else if (other.tag == "branch") {
			GameManager.s_instance.currentGUIseries = other.GetComponent<RoadBranch> ().GUIObject;
			GameManager.s_instance.currentGUIseries.SetActive (true);
			GameManager.s_instance.SwitchToCutscene ();
			print ("HIT BRANCH");
		} else if (other.tag == "pitstop") {
			GameManager.s_instance.SwitchToPitStop();
			pitstopEntranceAvailable = false;
			respawnPos = other.GetComponent<PitStopRespawn>().respawnPosition;
		} else if (other.tag == "pitstopRoad" ) {
			pitstopEntranceAvailable = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "branch") {
			//rotating player 90 degrees depending on what it says
			endLerp = transform.rotation * Quaternion.Euler(0,other.GetComponent<RoadBranch>().degreeToTurnBy,0);
			StartRotateLerp();
			currentRoadSection = other.GetComponent<RoadBranch>().nextRoadBranch;
			isOnHorizontalRoad = !isOnHorizontalRoad;
			if(other.GetComponent<RoadBranch>().GUIObject.name == "Retire"){
				anim.SetTrigger ("Retired");
				transform.Translate(0,0,0);
			}
		} else if( other.tag == "pitstopRoad" ) {
			pitstopEntranceAvailable = false;
		}
	}

	public void SetAtRespawnPos() {
		if( respawnPos == null ) {
			Debug.LogError( "No respawn position set for this trigger." );
			return;
		}

		transform.position = respawnPos.position;
	}
}
