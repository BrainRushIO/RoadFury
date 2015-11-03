using UnityEngine;
using System.Collections;

public class LerpToGuyCart : MonoBehaviour {

	public enum CartDirection { LEFT, RIGHT };
	public CartDirection cartDirection;
	public Transform lerpToPos;
	public Collider oppositeSideCollider;
	public Animator cart;

	void OnTriggerEnter( Collider col ) {
		if( col.tag == "Player" ) {
			//turn off other collider so make sure logic works correctly
			oppositeSideCollider.enabled = false;

			int direction = 0;
			if( cartDirection == CartDirection.LEFT )
				direction = -1;
			else
				direction = 1;
			col.GetComponent<PlayerController>().LerpToGuyCart( lerpToPos, cart, cartDirection );
		}
	}
}
