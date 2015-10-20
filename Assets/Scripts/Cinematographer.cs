using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cinematographer : MonoBehaviour {

	/*

	Cinematographer takes a list of gameobjects, vector3s, floats
	
	and uses these to move a camera as it transitions between looking at different objects

	it also keeps text or UI panels that are associated with each 


	 */

	Quaternion lerpStart, lerpEnd;
	public List<Transform> quaternions;
	public List<GameObject> textUIObjects;
	public List<float> timeAtEachObject;
	int currentIndex = 0;
	public bool hasStarted;
	float pauseTimer;
	Vector3 lerpPositionStart, lerpPositionEnd;
	//lerp
	float lerpTimer;
	public float lerpDuration = 3f;
	bool isLerping;

	public void RollCamera () {
		pauseTimer = Time.time;
		Camera.main.transform.localRotation = quaternions[currentIndex].localRotation;
		Camera.main.transform.localPosition = quaternions[currentIndex].localPosition;
		hasStarted = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (hasStarted) {
			if (Time.time - pauseTimer > timeAtEachObject[currentIndex]) {
				if (currentIndex < quaternions.Count - 1) {
					GotoNextPosition();
				}
				else if (!isLerping) {
//					textUIObjects[currentIndex].SetActive(false);
					hasStarted = false;
					GameManager.s_instance.SwitchToGame();
					currentIndex =0;
				}

			}


			if (isLerping) {
				float fraction = (Time.time - lerpTimer	)/lerpDuration;
				Camera.main.transform.localRotation = Quaternion.Lerp(lerpStart, lerpEnd, fraction);
				Camera.main.transform.localPosition = Vector3.Lerp (lerpPositionStart, lerpPositionEnd, fraction * fraction * (3.0f - 2.0f * fraction));
				if (fraction > .9999f) {
					Camera.main.transform.localRotation = Quaternion.Lerp(lerpStart, lerpEnd, 1f);
					Camera.main.transform.localPosition = Vector3.Lerp (lerpPositionStart, lerpPositionEnd, 1f);
					isLerping = false;
				}
			}

		}
	}

	void GotoNextPosition () {
		pauseTimer = Time.time;
		lerpTimer = Time.time;
//		textUIObjects[currentIndex].SetActive(false);
		currentIndex++;
//		textUIObjects[currentIndex].SetActive(true);
		lerpStart = Camera.main.transform.localRotation;
		lerpPositionStart = Camera.main.transform.localPosition;
		lerpEnd = quaternions[currentIndex].localRotation;
		lerpPositionEnd = quaternions[currentIndex].localPosition;
		isLerping = true;

	}
}
