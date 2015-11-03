using UnityEngine;
using System.Collections;

public class NumberToString {

	public static string Convert( float number ) {
		float absNumber = Mathf.Abs( number );

		if( absNumber >= 1000000000f ) {
			absNumber /= 1000000000f;
			return System.Math.Round( absNumber*Mathf.Sign(number), 1 ).ToString() + "B"; 
		} else if( absNumber >= 1000000f ) {
			absNumber /= 1000000f;
			return System.Math.Round( absNumber*Mathf.Sign(number), 1 ).ToString() + "M";
		} else if( absNumber >= 1000f ) {
			absNumber /= 1000f;
			return System.Math.Round( absNumber*Mathf.Sign(number), 1 ).ToString() + "K";
		} else {
			return System.Math.Round( absNumber*Mathf.Sign(number), 0 ).ToString();
		}
	}
}
