
using UnityEngine;

public class LightSettings : MonoBehaviour {

	void Update () {
		switch (Settings.getDifficulty())
        {
            case 0: case 1: this.GetComponent<Light>().enabled = true; break;

            case 2: case 3: this.GetComponent<Light>().enabled = false; break;
        }
	}
}