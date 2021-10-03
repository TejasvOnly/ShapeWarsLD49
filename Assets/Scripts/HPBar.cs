using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HPBar : MonoBehaviour {

	public RectTransform rect;

	private void Start() {
		rect = GetComponent<RectTransform>();
	}

	public void UpdateHp(float hp) {
		rect.sizeDelta = new Vector2(hp, rect.rect.height);
	}

	public void UpdateMaxHp(float hp) {
		rect.sizeDelta = new Vector2(hp, rect.rect.height);
	}
}
