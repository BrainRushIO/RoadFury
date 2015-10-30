using UnityEngine;
using System.Collections;

public class NotificationAnimEvent : MonoBehaviour {

	public void TriggerAnimation( bool triggerOn ) {
		GameManager.s_instance.TriggerNotificationState( triggerOn );
	}
}
