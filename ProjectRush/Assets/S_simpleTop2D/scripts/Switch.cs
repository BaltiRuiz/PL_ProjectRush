
using UnityEngine;

public class Switch : MonoBehaviour {

    public Light spotLight1;

    public Light spotLight2;

    void Start()
    {
        spotLight1.enabled = false;

        spotLight2.enabled = false;
    }
}