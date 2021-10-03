using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	public float interpVelocity;
	public float smoothTime;
	public float followDistance;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	private Vector3 currentVelocity;
	void Start() {
		targetPos = transform.position;
	}

	void FixedUpdate() {
		if (target) {
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector3 targetDirection = (target.transform.position - posNoZ);

			interpVelocity = targetDirection.magnitude * 5f;

			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

			transform.position = Vector3.SmoothDamp(transform.position, targetPos + offset, ref currentVelocity, smoothTime);

		}
	}
}