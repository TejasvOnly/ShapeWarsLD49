using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToCursor : MonoBehaviour {


	void Update() {
		Vector2 mousePosition = Input.mousePosition;
		Vector2 direction = new Vector2(
		mousePosition.x - Screen.width / 2,
		mousePosition.y - Screen.height / 2
		).normalized;

		transform.right = direction;
	}
}
