using UnityEngine;
using System.Collections;

public class RealEstate {
	public static int MAX_REAL_ESTATE_YOU_CAN_OWN = 4;
	public static float[] RealEstatePrices = new float[3] { 50000f, 500000f, 5000000f };

	public string realEstateName;
	public float realEstateValue;
	public float interestRate = 0f;
	public enum RealEstateTier { Economy, Standard, Luxury };
	public RealEstateTier thisRealEstateTier;
	
	public RealEstate() {
		interestRate = Random.Range( 0.1f, 5.0f );
	}

	~RealEstate() {
		PlayerStats.OnYearCompleted += AnnualUpdate;
	}

	void OnDisable() {
		PlayerStats.OnYearCompleted -= AnnualUpdate;
	}

	private void AnnualUpdate() {
		realEstateValue *= interestRate;
	}

	public void SetTier( RealEstateTier tier ) {
		thisRealEstateTier = tier;
		realEstateValue = RealEstatePrices[(int)tier];
	}
}
