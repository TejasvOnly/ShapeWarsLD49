using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {


	public Transform firePoint;
	public GameObject block;
	public GameObject bullet;
	public BlockCounter blockDisplay;
	public float fireSpeed = 0.2f;
	public int blocks = 10;

	private float timer = 0;
	private ObjectPooler op;
	void Start() {
		op = ObjectPooler.Instance;
	}

	// Update is called once per frame
	void Update() {
		blockDisplay.UpdateBlockCounter(blocks);
		if (Input.GetButtonDown("Fire2")) {
			Build();
		}
		if (Input.GetButton("Fire1")) {
			Shoot();
		} else {
			if (timer > fireSpeed) {
				timer = fireSpeed;
			}
		}
		timer += Time.deltaTime;


	}

	private void Build() {
		if (blocks > 0) {
			Instantiate(block, firePoint.position, firePoint.rotation);
			blocks--;
		}
	}
	private void Shoot() {
		if (timer >= fireSpeed) {
			op.SpawnFromPool("bullet", firePoint.position, firePoint.rotation);
			timer = 0;
		}
	}

	public void AddBlocks(int value) {
		blocks += value;
	}

}
