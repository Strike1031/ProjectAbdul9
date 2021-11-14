using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public List<GameObject> targets;
	public Vector3 offset;

	private Vector3 targetPos;

	// Use this for initialization
	void Start()
	{
		targetPos = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate()
	{

        if (targets == null || targets.Count == 0)
			return;

		Vector3 lookPost = Vector3.zero;
		for (int i = 0; i < targets.Count; i++)
		{
			lookPost += targets[i].transform.position;
		}
		lookPost = lookPost / (float)targets.Count;
		
		Vector3 targetDirection = (lookPost - transform.position);

		interpVelocity = targetDirection.magnitude * 5f;

		targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

		transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.7f);
	}
}
