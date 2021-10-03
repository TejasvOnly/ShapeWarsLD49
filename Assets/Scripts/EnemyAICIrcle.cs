using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAICIrcle : MonoBehaviour, IPooledObject {

	public Transform target;

	private float hp = 40;
	public int blockDrop = 2;
	public float health = 40;
	public float blastRadius = 2f;
	public float blastForce = 10f;
	public float blastDamageMin = 10f;
	public float blastDamageMax = 30f;
	public float timeBeforeExplosion = 2f;

	public float speed = 5f;

	public Rigidbody2D rb;
	public ParticleSystem ps;
	public ParticleSystem dps;
	private float timer;
	private bool canExplode;
	private bool killedByPlayer;
	private bool forceExplode = false;


	public void Init() {
		rb.velocity = Vector2.zero;
		SendMessage("StopFlash", SendMessageOptions.DontRequireReceiver);
		hp = health;
		timer = 0;
		forceExplode = false;
		killedByPlayer = false;
		ps.transform.SetParent(transform);
		ps.transform.localPosition = Vector3.zero;
		ps.transform.rotation = Quaternion.identity;
		ps.Clear();
		dps.transform.SetParent(transform);
		dps.transform.localPosition = Vector3.zero;
		dps.transform.rotation = Quaternion.identity;
		dps.Clear();
	}
	private void Start() {
		target = GameObject.Find("Player").transform;
	}


	void Update() {

		if (CanExplode() || forceExplode) {
			Explode();
		}

		ApproachTarget();

		if (hp <= 0) {
			Die();
		}
	}

	void ApproachTarget() {
		Vector2 direction = target.position.x > transform.position.x ? Vector2.right : Vector2.left;

		rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
	}

	bool CanExplode() {
		if (Vector2.Distance(target.position, transform.position) < blastRadius / 2) {
			canExplode = true;
		}
		if (Vector2.Distance(target.position, transform.position) > blastRadius && !forceExplode && canExplode) {
			canExplode = false;
			timer = 0;
			SendMessage("StopFlash", SendMessageOptions.DontRequireReceiver);

		}
		return canExplode;
	}

	void Explode() {
		if (timer >= timeBeforeExplosion) {
			Vector2 smashPoint = transform.position;
			Collider2D[] victims = Physics2D.OverlapCircleAll(smashPoint, blastRadius);

			foreach (Collider2D victim in victims) {
				victim.attachedRigidbody.AddForce(((Vector2)victim.transform.position - smashPoint) * blastForce);
				victim.SendMessage("TakeDamage", Mathf.Lerp(blastDamageMax, blastDamageMin, Vector2.Distance(transform.position, victim.ClosestPoint(transform.position)) / blastRadius), SendMessageOptions.DontRequireReceiver);
			}
			ps.Play();
			Die();
		}
		timer += Time.deltaTime;
		SendMessage("StartLoop", SendMessageOptions.DontRequireReceiver);

	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Obstacle") {
			forceExplode = true;
		}
	}

	public void TakeDamage(float damage) {
		hp -= damage;
		SendMessage("Flash", SendMessageOptions.DontRequireReceiver);
	}

	public void MarkPlayerKill() {
		killedByPlayer = true;
	}

	void Die() {
		ps.transform.SetParent(null);
		dps.Play();

		dps.transform.SetParent(null);
		if (killedByPlayer) {
			target.SendMessage("AddBlocks", blockDrop, SendMessageOptions.DontRequireReceiver);
			target.SendMessage("AddHealth", health / 4, SendMessageOptions.DontRequireReceiver);
		}
		gameObject.SetActive(false);
	}
}
