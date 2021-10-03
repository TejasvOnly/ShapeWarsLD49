using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

	public GameObject block;
	public float speed = 10;
	public Rigidbody2D rb;

	void Start() {
		rb.velocity = transform.right * speed;

	}

	private void OnCollisionEnter2D(Collision2D other) {



		if (other.gameObject.name != "Player") {

			ContactPoint2D contactPoint = other.GetContact(0);
			Vector2 targetPosition = contactPoint.point + contactPoint.normal * 0.75f;

			Instantiate(block, targetPosition, Quaternion.LookRotation(Vector3.forward, contactPoint.normal));
			Destroy(gameObject);


		}
	}

}
