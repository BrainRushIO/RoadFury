using UnityEngine;
using System.Collections;

public class NotificationAnimEvent : MonoBehaviour {

	public void TriggerNotificationState( bool triggerOn ) {
		GameManager.s_instance.SwitchToNotification();
	}
}
