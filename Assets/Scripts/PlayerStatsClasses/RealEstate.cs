using UnityEngine;
using System.Collections;

public class RealEstate : MonoBehaviour {
	public static int MAX_REAL_ESTATE_YOU_CAN_OWN = 4;
	public static float[] RealEstatePrices = new float[3] { 50000f, 500000f, 5000000f };

	public string realEstateName;
	public float realEstateValue; //cost brackets 50k-100k, 100k-500k, 500k+
	public float interestRate = 0f;
	public enum RealEstateTier { Economy, Standard, Luxury };
	public RealEstateTier thisRealEstateTier;
	
	void Start() {
		interestRate = Random.Range( 0.1f, 5.0f );
	}
}
