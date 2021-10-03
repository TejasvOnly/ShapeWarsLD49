using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITriangle : MonoBehaviour, IPooledObject {

	public Transform target;
	public Transform firePoint;

	private float hp;
	public int blockDrop = 2;
	public float health = 60;

	public float fireSpeed = 1f;

	public float engageRadius = 10f;
	public float speed = 5f;

	public Rigidbody2D rb;
	public ParticleSystem ps;
	private float timer;
	private bool killedByPlayer;
	private ObjectPooler op;


	public void Init() {
		rb.velocity = Vector2.zero;
		hp = health;
		timer = 0;
		killedByPlayer = false;
		ps.transform.SetParent(transform);
		ps.transform.localPosition = Vector3.zero;
		ps.transform.rotation = Quaternion.identity;
		ps.Clear();
	}
	private void Start() {
		target = GameObject.Find("Player").transform;
		op = ObjectPooler.Instance;
		Init();
	}


	private void Update() {
		if (hp <= 0) {
			Die();
		}
		ApproachTarget();
		Shoot();

		if (timer > fireSpeed) {
			timer = fireSpeed;
		} else {
			timer += Time.deltaTime;
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
		ps.Play();
		if (killedByPlayer) {
			target.SendMessage("AddBlocks", blockDrop, SendMessageOptions.DontRequireReceiver);
			target.SendMessage("AddHealth", health / 4, SendMessageOptions.DontRequireReceiver);
		}
		gameObject.SetActive(false);
	}

	void ApproachTarget() {
		Vector3 vectorToTarget = target.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed * 4);

		if (Vector2.Distance(transform.position, target.position + Vector3.up * (engageRadius / 2)) > engageRadius) {
			rb.velocity = transform.right * speed;
		}
	}

	void Dodge() {

	}
	void Shoot() {
		if (Vector2.Distance(transform.position, target.position + Vector3.up * (engageRadius / 2)) < engageRadius && timer >= fireSpeed) {
			op.SpawnFromPool("enemyBullet", firePoint.position, firePoint.rotation);
			timer = 0;
		}
	}





}
