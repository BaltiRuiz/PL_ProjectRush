
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;

	FieldOfViewZX fov;

	NoiseReceiver noiseReceiver;

	void Start()
    {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void OnEnable()
    {
		fov = GetComponent<FieldOfViewZX>();
		noiseReceiver = GetComponent<NoiseReceiver>();

		fov.OnSight += Chase;
		noiseReceiver.OnEar += Chase;
	}

	void Disable()
    {
		fov.OnSight -= Chase;

		noiseReceiver.OnEar -= Chase;
	}

	void Chase(List<Transform> targets)
    {
        int player_in_targets = playerInTargets(targets);

        if (player_in_targets == -1)
        {
            agent.SetDestination(targets[0].position);

            transform.LookAt(targets[0].position);
        }

        else
        {
            agent.SetDestination(targets[player_in_targets].position);

            transform.LookAt(targets[player_in_targets].position);
        }
	}

    private int playerInTargets(List<Transform> targets)
    {
        foreach (Transform target in targets)
        {
            if (target.gameObject.name == "player")
                return targets.IndexOf(target);
        }

        return -1;
    }
}