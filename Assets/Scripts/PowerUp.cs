using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PowerUp : MonoBehaviour {
	public Text thisText;
	public string nameOfObj, guiMessage;
	public float happiness, incomeModifier;
	public int cost;
	public AudioSource playOnCollision;

	void OnTriggerEnter (Collider other) {
		if (playOnCollision != null) {
			playOnCollision.Play();
		}
		if (other.gameObject.tag == "Player") {
			GUIManager.s_instance.SpawnMessage (guiMessage);
			if (cost != 0)
				GUIManager.s_instance.SpawnCost (cost);

			if (happiness != 0)
				GUIManager.s_instance.SpawnHappiness (happiness);

			if (incomeModifier != 0)
				GUIManager.s_instance.SpawnBurnRate (incomeModifier);
		}
	}

}
