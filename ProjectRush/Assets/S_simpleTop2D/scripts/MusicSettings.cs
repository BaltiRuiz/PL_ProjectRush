
using UnityEngine;

public class MusicSettings : MonoBehaviour {

	void Update () {
        this.GetComponent<AudioSource>().volume = Settings.getMusicVolume();
	}
}