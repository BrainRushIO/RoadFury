using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadPiece : MonoBehaviour {

	public List<GameObject> smallPickUpPrefabs, mediumPickUpPrefabs, bigPickUpPrefabs;
	public Transform[] pickUpSpawnPoints;

	void Start () {
		int randomSpawnPosition = Random.Range (0, pickUpSpawnPoints.Length - 1);
		int randomSpawnValue = Random.Range (1, 100);
		if (randomSpawnValue < 50) {
			GameObject temp = Instantiate(smallPickUpPrefabs[Random.Range(0, smallPickUpPrefabs.Count-1)]);
			temp.transform.SetParent(pickUpSpawnPoints[randomSpawnPosition]);
			temp.transform.localPosition =  new Vector3(0,0,0);
		} else if (randomSpawnValue >= 51 && randomSpawnValue < 60) {
			GameObject temp = Instantiate(mediumPickUpPrefabs[Random.Range(0, mediumPickUpPrefabs.Count-1)]);
			temp.transform.SetParent(pickUpSpawnPoints[randomSpawnPosition]);
			temp.transform.localPosition =  new Vector3(0,0,10);
		} else if (randomSpawnValue >= 60 && randomSpawnValue < 63) {
			GameObject temp = Instantiate(bigPickUpPrefabs[Random.Range(0, bigPickUpPrefabs.Count-1)]);
			temp.transform.SetParent(pickUpSpawnPoints[randomSpawnPosition]);
			temp.transform.localPosition =  new Vector3(0,0,10);
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "roadKiller") {
			RoadManager.s_instance.SpawnNewPiece();
			Destroy(gameObject);
		}
	}



}
