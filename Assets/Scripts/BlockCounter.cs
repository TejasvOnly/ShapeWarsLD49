using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class BlockCounter : MonoBehaviour {

	public TextMeshProUGUI textMeshPro;

	public void UpdateBlockCounter(int count) {
		textMeshPro.SetText(count.ToString());
	}

}
