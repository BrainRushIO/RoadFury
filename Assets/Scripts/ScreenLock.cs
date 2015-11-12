using UnityEngine;
using System.Collections;

public class ScreenLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
	}
}
