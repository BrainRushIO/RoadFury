using UnityEngine;
using System.Collections;

public class SoundtrackManager : MonoBehaviour {
	
	public AudioSource click, click2, happy, sad, crash, sick, pitstop, newYear; //soundtrack files
	public static SoundtrackManager s_instance;
	
	void Awake(){
		if (s_instance==null) {
			s_instance = this;
		}
		else if (s_instance!=this) {
			Destroy(gameObject);
		}
	}
	
	IEnumerator FadeOutAudioSource(AudioSource x) { //call from elsewhere
		while (x.volume > 0.0f) {					//where x is sound track file
			x.volume -= 0.01f;
			yield return new WaitForSeconds(0.03f);
		}
		x.Stop ();
	}
	
	public void PlayAudioSource(AudioSource x) { //call from elsewhere
		x.volume = 1;
		x.Play ();
	}
}