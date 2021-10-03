using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private Collision coll;

	[Space]
	[Header("Stats")]
	public float speed = 10f;
	[Range(1, 30)]
	public float jumpVelocity;
	public float slideSpeed = 5;
	public float wallJumpLerp = 10;
	public float hp = 200;



	[Space]
	[Header("Booleans")]
	public bool canMove;
	public bool wallJumped;
	public bool wallSlide;

	[Space]
	private bool groundTouch;
	public Vector2 dir;
	public int side = 1;
	public ParticleSystem ps;
	public HPBar hPBar;


	void Start() {
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collision>();
		hPBar.UpdateMaxHp(hp);
	}

	void Update() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		dir = new Vector2(x, y);

		hPBar.UpdateHp(hp);

		if (hp <= 0) {
			Die();
		}

		Walk();
		if (coll.onWall && !coll.onGround) {
			if (x != 0) {
				wallSlide = true;
				WallSlide();
			}
		}
		if (!coll.onWall || coll.onGround)
			wallSlide = false;
		if (Input.GetButtonDown("Jump")) {
			if (coll.onGround)
				Jump(Vector2.up);
			if (coll.onWall && !coll.onGround)
				WallJump();
		}

	}

	private void Walk() {
		if (!canMove)
			return;
		if (!wallJumped) {
			rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
		} else {
			rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
		}
	}

	private void Jump(Vector2 Jdir) {
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.velocity += Jdir * jumpVelocity;
	}
	private void WallSlide() {
		if (!canMove)
			return;

		bool pushingWall = false;
		if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall)) {
			pushingWall = true;
		}
		float push = pushingWall ? 0 : rb.velocity.x;

		rb.velocity = new Vector2(push, -slideSpeed);
	}

	private void WallJump() {

		StopCoroutine(DisableMovement(0));
		StartCoroutine(DisableMovement(.1f));

		Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

		Jump((Vector2.up + wallDir));

		wallJumped = true;
	}
	IEnumerator DisableMovement(float time) {
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;

	}

	public void TakeDamage(float damage) {
		hp -= damage;
		SendMessage("Flash", SendMessageOptions.DontRequireReceiver);
	}

	void Die() {
		ps.transform.SetParent(null);
		ps.Play();
		FindObjectOfType<GameDirector>().OnPlayerDeath();
		Destroy(gameObject);
	}

	public void AddHealth(float health) {
		hp += health;
		if (hp > 200) {
			hp = 200;
		}
	}
}
