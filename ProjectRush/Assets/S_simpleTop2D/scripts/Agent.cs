
using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour 
{
	public Transform target1;

    public Transform target2;

    private Transform nextTarget;

    private bool waiting;

	UnityEngine.AI.NavMeshAgent agent;

	void Start () 
	{
        waiting = false;

        nextTarget = target1;

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		agent.SetDestination(nextTarget.position);
	}

    IEnumerator WaitAndSetDestination()
    {
        yield return new WaitForSeconds(3.0f);

        agent.SetDestination(nextTarget.position);

        waiting = false;
    }

    void Update()
    {
        if (!waiting)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (nextTarget == target1)
                {
                    nextTarget = target2;

                    waiting = true;

                    StartCoroutine(WaitAndSetDestination());
                }

                else if (nextTarget == target2)
                {
                    nextTarget = target1;

                    waiting = true;

                    StartCoroutine(WaitAndSetDestination());
                }
            }
        }
    }
}