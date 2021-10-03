using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour {
	public TextMeshProUGUI textMeshPro;
	public void UpdateScore(int score) {

		textMeshPro.SetText(score.ToString());
	}
}
