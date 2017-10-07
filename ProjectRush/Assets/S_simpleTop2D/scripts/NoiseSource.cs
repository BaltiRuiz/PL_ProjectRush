
using UnityEngine;

public class NoiseSource : MonoBehaviour 
{
	[HideInInspector]
	public bool isNoisy = false;

	[HideInInspector]
	public float decibels;

	Action myAction;

	void OnEnable()
    {
		myAction = GetComponent<Action>();

		myAction.OnAction += MakeNoise;
	}

	void OnDisable()
    {
		myAction.OnAction -= MakeNoise;
	}

    void MakeNoise(int action)
    {
		switch (action) 
		{
		    case (int) ActionIs.Stop:
			    isNoisy = false;
			break;

            case (int)ActionIs.Sneaky:
                isNoisy = true;

                decibels = 3.5f;
            break;

            case (int) ActionIs.Walk:
			    isNoisy = true;

			    decibels = 7.0f;
			break;

            case (int)ActionIs.Run:
                isNoisy = true;

                decibels = 14.0f;
                break;

            case (int) ActionIs.Ring:
                isNoisy = true;

                decibels = 20.0f;
            break;
		}
	}
}