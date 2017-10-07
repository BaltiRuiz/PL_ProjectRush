
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfViewZX : MonoBehaviour {

	public float viewRadius;

	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	public float meshResolution;
	public int edgeResolveIterations;
	public float edgeDstThreshold;

	public float maskCutawayDst = .1f;

	public delegate void SightFunction(List<Transform> visibleTargets);
	public event SightFunction OnSight;

	void Start()
    {
		StartCoroutine("FindTargetsWithDelay", .2f);
	}

	IEnumerator FindTargetsWithDelay(float delay)
    {
		while (true)
        {
			yield return new WaitForSeconds (delay);

			FindVisibleTargets ();
		}
	}

	void Update()
    {
        if (visibleTargets.Count > 0)
        {
            if (OnSight != null)
                OnSight(visibleTargets);
        }
		
		foreach (Transform target in visibleTargets)
        {
			Debug.DrawLine(transform.position, target.position);
		}
	}

	void FindVisibleTargets()
    {
		visibleTargets.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
			Transform target = targetsInViewRadius [i].transform;

			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
            {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    visibleTargets.Add(target);
			}
		}
	}
}