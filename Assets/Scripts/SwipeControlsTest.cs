using UnityEngine;
using System.Collections;
using InControl;

public class SwipeControlsTest : MonoBehaviour {

	Renderer myRenderer;
	InputDevice myInputDevice;

	void Start() {
		myRenderer = GetComponent<Renderer>();
	}

	void Update() {
		// Grab current input device
		myInputDevice = InputManager.ActiveDevice;
		
		if( myInputDevice.DPadUp.IsPressed )
			myRenderer.material.color = Color.yellow;
		else if( myInputDevice.DPadDown.IsPressed )
			myRenderer.material.color = Color.green;
		else if( myInputDevice.DPadLeft.IsPressed )
			myRenderer.material.color = Color.blue;
		else if( myInputDevice.DPadRight.IsPressed )
			myRenderer.material.color = Color.red;
	}
}
