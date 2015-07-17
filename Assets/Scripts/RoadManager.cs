using UnityEngine;
using System.Collections;

public class RoadManager : MonoBehaviour {

	public GameObject roadPiecePrefab;	
	public GameObject lastPieceSpawned;
	float distanceBetweenPieces = 10;
	public float currentSpeed = 3;


	public static RoadManager s_instance;


	void Awake () {
		if (s_instance == null) {
			s_instance = this;
		}
		else {
			if (s_instance!=this) {
				Destroy(gameObject);
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate (0, 0, -currentSpeed, Space.Self);
	}

	public void SpawnNewPiece () {
		GameObject newPiece = Instantiate (roadPiecePrefab);
		newPiece.transform.SetParent (RoadManager.s_instance.transform, true);
		newPiece.transform.localPosition = new Vector3 (0, 0, lastPieceSpawned.transform.localPosition.z + distanceBetweenPieces);
		lastPieceSpawned = newPiece;
	}
}
