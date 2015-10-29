using UnityEngine;
using System.Collections;

public class NumberToString {

	public static string Convert( float number ) {
		if( number >= 1000000000f ) {
			number /= 1000000000f;
			return System.Math.Round( number, 1 ).ToString() + "B"; 
		} else if( number >= 1000000f ) {
			number /= 1000000f;
			return System.Math.Round( number, 1 ).ToString() + "M";
		} else if( number >= 1000f ) {
			number /= 1000f;
			return System.Math.Round( number, 1 ).ToString() + "K";
		} else {
			return System.Math.Round( number, 0 ).ToString();
		}
	}
}
