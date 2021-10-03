using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPooledObject {
	public float speed = 20f;
	public float damage = 25f;
	public bool isEnemyBullet = false;
	public Rigidbody2D rb;
	public TrailRenderer tr;
	public ParticleSystem ps;

	public void Init() {
		rb.velocity = transform.right * speed;
		tr.transform.SetParent(transform);
		tr.transform.localPosition = Vector3.zero;
		tr.transform.rotation = Quaternion.identity;
		tr.Clear();
		ps.Clear();
	}


	private void OnTriggerEnter2D(Collider2D other) {
		if (other.name != "Player" || isEnemyBullet) {

			other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
			other.SendMessage("MarkPlayerKill", SendMessageOptions.DontRequireReceiver);

			ps.Play();
			tr.transform.SetParent(null);
			gameObject.SetActive(false);
		}
	}

}
