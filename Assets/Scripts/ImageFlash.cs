using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageFlash : MonoBehaviour {
	bool isFadingBack;
	float startTime, fadeTime = 3;
	Color currentColor, flashColor;
	bool isText = false;

	void Start () {
		if (GetComponent<Image>()!=null) 
			currentColor = GetComponent<Image>().color;
		else {
			currentColor = GetComponent<Text>().color;
			isText = true;
		}
	}

	void Update () {
		if (isFadingBack) {
			float timePassed = (Time.time - startTime);
			float fracJourney = timePassed / fadeTime;

			if(isText) {
				GetComponent<Text>().color = Color.Lerp (flashColor, currentColor, fracJourney);
			} else {
				GetComponent<Image>().color = Color.Lerp (flashColor, currentColor, fracJourney);
			}
		}
	}

	public void Flash (Color toColor) {
		flashColor = toColor;
		startTime = Time.time;
		isFadingBack = true;
	}
}
