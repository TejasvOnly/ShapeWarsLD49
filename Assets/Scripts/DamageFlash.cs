using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {
	public Color damageColor;
	public Color loopColor;
	public float flashTime = 0.2f;
	public string internalNameForColor = "Color_4882617fbef548f5b1418260b96acc46";
	private bool stopLoop = true;

	public MeshRenderer meshRenderer;
	Color originalColor;
	void Start() {
		if (meshRenderer == null) { meshRenderer = GetComponent<MeshRenderer>(); }
		originalColor = meshRenderer.material.GetColor(internalNameForColor);
	}

	public void Flash() {
		stopLoop = true;
		meshRenderer.material.SetColor(internalNameForColor, damageColor);
		Invoke("StopFlash", flashTime);
	}

	public void StopFlash() {
		stopLoop = true;
		meshRenderer.material.SetColor(internalNameForColor, originalColor);
	}

	public void FlashLoop() {
		if (!stopLoop) {
			meshRenderer.material.SetColor(internalNameForColor, loopColor);
			Invoke("ReFlash", flashTime / 2);
		}
	}
	public void ReFlash() {
		if (!stopLoop) {
			meshRenderer.material.SetColor(internalNameForColor, originalColor);
			Invoke("FlashLoop", flashTime / 2);
		}
	}

	public void StartLoop() {
		if (stopLoop) {
			stopLoop = false;
			FlashLoop();
		}
	}

}
