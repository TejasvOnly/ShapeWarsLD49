using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
	public GameObject menu;
	public GameObject info;

	public void ShowMenu() {
		menu.SetActive(true);
		info.SetActive(false);
	}


	public void ShowInfo() {
		menu.SetActive(false);
		info.SetActive(true);
	}


	public void StartGame() {
		SceneManager.LoadScene(1);
	}
}
