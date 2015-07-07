using UnityEngine;
using System.Collections;

public class TextPopUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("Suicide");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0f, .1f, 0f);
	}

	IEnumerator Suicide() {
		yield return new WaitForSeconds(3f);
		Destroy (gameObject);
	}
}
