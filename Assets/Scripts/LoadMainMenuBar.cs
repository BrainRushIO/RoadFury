using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadMainMenuBar : MonoBehaviour {

	AsyncOperation async;
	Slider thisSlider;

	void Start () {
		StartCoroutine ("LoadLevel");
	}

	IEnumerator LoadLevel() {
		thisSlider = GetComponent<Slider> ();
		async = Application.LoadLevelAsync (1);
		yield return async;
	}
	
	// Update is called once per frame
	void Update () {
		if (async != null) {
			print (async.progress);
			thisSlider.value = async.progress;
		}
	}
}
