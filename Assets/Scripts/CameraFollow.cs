using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float interpVelocity;
	public float minDistance;
	public float followDistance;

	public float smoothTime;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	Vector3 velocityF;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
		targetPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector3 targetDirection = (target.transform.position - posNoZ);

			interpVelocity = targetDirection.magnitude * 5f;

			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

			transform.position = Vector3.SmoothDamp( transform.position, targetPos + offset, ref velocityF, smoothTime, 0.9f,  0.25f);

		}
    }
}
