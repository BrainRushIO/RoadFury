using UnityEngine;
using System.Collections;

public class HoverFollowCam : MonoBehaviour
{
	Transform camPos;
	int layerMask;
	float camDistanceToCamPos;
	float smoothRate = 8f;
	public enum CameraMode {normalMode};
	public CameraMode thisCameraMode = CameraMode.normalMode;
	
	//switches
	bool isAdjustingToCamPos;
	
	void Start() {
		camPos = GameObject.FindGameObjectWithTag("CamPos").transform;
	}
	
	void Update()
	{
		switch (thisCameraMode) {
		case CameraMode.normalMode :
			if (GameManager.s_instance.currentGameState == GameState.Playing) {
				transform.localPosition -= (transform.localPosition - camPos.localPosition) * smoothRate *Time.deltaTime;
			}
			break;
		}
	}
}
