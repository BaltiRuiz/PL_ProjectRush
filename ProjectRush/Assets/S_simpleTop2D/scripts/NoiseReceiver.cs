
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseReceiver : MonoBehaviour {

	public float airDegradation = 0.3f;
	public float obstaculeDegradation;

    public float audibleRadius;
	public float audibleDecibels = 3.0f;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<Transform> audibleTargets = new List<Transform>();

	public delegate void NoiseFunction(List<Transform> audibleTargets);
	public event NoiseFunction OnEar;

	void Awake()
    {
		obstaculeDegradation = airDegradation * 2.5f;
		audibleRadius = 150.0f / airDegradation;
	}

	void Start()
    {
		StartCoroutine("FindTargetsWithDelay", .2f);
	}	

	void Update()
    {
        if (audibleTargets.Count > 0)
        {
            if (OnEar != null)
                OnEar(audibleTargets);
        }

		foreach (Transform target in audibleTargets)
        {
				Debug.DrawLine(transform.position, target.position);
		}
	}

	IEnumerator FindTargetsWithDelay(float delay)
    {
		while (true)
        {
			yield return new WaitForSeconds (delay);

			FindAudibleTargets ();
		}
	}

	void FindAudibleTargets()
    {
		audibleTargets.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, audibleRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
			Transform target = targetsInViewRadius [i].transform;

			NoiseSource noiseSource = target.GetComponent<NoiseSource>();

			if (noiseSource.isNoisy)
            {
				Vector3 dirToTarget = (target.position - transform.position).normalized;

				float dstToTarget = Vector3.Distance (transform.position, target.position);
				float noise = noiseSource.decibels - dstToTarget * airDegradation;

				if (noise >= audibleDecibels)
                {
					RaycastHit[] hits = Physics.RaycastAll (transform.position, dirToTarget, dstToTarget, obstacleMask);

					noise -= hits.Length * obstaculeDegradation;

                    if (noise >= audibleDecibels)
                        audibleTargets.Add(target);
				}
			}
		}
	}
}