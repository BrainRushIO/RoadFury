using UnityEngine;
using System.Collections;

public class RoadPiece : MonoBehaviour {

	public Transform[] pickUpSpawnPoints;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "roadKiller") {
			RoadManager.s_instance.SpawnNewPiece();
			Destroy(gameObject);
		}
	}



}
