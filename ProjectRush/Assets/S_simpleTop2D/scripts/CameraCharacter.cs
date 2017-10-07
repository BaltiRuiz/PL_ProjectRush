
using UnityEngine;

public class CameraCharacter : MonoBehaviour {

    public Transform player;

    public Transform wholeLevelPosition;

    private float height;

	void Start () {
        height = transform.position.y;
	}
	
	void Update () {
        switch (Settings.getDifficulty())
        {
            case 0: case 2: this.transform.position = wholeLevelPosition.position; this.transform.rotation = wholeLevelPosition.rotation; break;

            case 1: case 3: transform.position = new Vector3(player.position.x, height, player.position.z); break;
        }
    }
}