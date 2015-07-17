using UnityEngine;
using System.Collections;

public class RotateSkyBox : MonoBehaviour {

	Skybox thisSkyBox;

	// Use this for initialization
	void Start () {
		thisSkyBox = GetComponent<Skybox>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
