using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	public float hp = 100;
	public float scaleTime = 1;
	public Vector3 scale = Vector3.one;
	public ParticleSystem ps;

	private Vector3 currentScaleVelocity;





	private void OnCollisionEnter2D(Collision2D other) {
		if (other.relativeVelocity.magnitude > 10 && other.gameObject.name != "Player") {
			hp -= (other.relativeVelocity.magnitude - 10) * 10;
		}
	}

	private void Update() {

		if (transform.localScale != scale) {
			Scale();
		}

		if (hp <= 0) {

			ps.transform.SetParent(null);
			ps.Play();
			Destroy(gameObject);
		}
	}

	public void TakeDamage(float damage) {
		hp -= damage;
		SendMessage("Flash", SendMessageOptions.DontRequireReceiver);
	}


	void Scale() {
		transform.localScale = Vector3.SmoothDamp(transform.localScale, scale, ref currentScaleVelocity, scaleTime);

		if (Vector3.Distance(transform.localScale, scale) < 0.1f) {
			transform.localScale = scale;
		}
	}

}
