using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {
	public Transform player;
	public GameObject[] uiElements;

	public float circleEnenmySpawn;
	public float spawnInterval = 9;
	private float timer = 4;
	private int maxHeight = 0;

	public ScoreDisplay score;




	private ObjectPooler op;
	void Start() {
		op = ObjectPooler.Instance;
		EnableOnlyUi(0);
	}

	void Update() {
		if (player) {
			if (player.position.y > maxHeight) {
				maxHeight = (int)player.position.y;
				score.UpdateScore(maxHeight);
			}
			float offset = maxHeight / 125f;

			if (timer >= spawnInterval * (1 - offset) + 1) {
				SpawnCircleEnemies();
				SpawnTriangleEnemies();
				timer = 0;
			}

			timer += Time.deltaTime;
			// Debug.Log(timer);
		}

	}

	void SpawnCircleEnemies() {

		for (int i = 0; i < 5; i++) {
			float xPos = Random.Range(-50.0f, 50.0f);
			float gap = 10f;
			xPos += xPos > 0 ? gap : gap * -1;
			xPos += player.position.x;

			Vector3 pos = new Vector3(xPos, circleEnenmySpawn, 0);
			op.SpawnFromPool("circleEnemy", pos, Quaternion.identity);

		}


	}
	void SpawnTriangleEnemies() {

		for (int i = 0; i < 1 + player.position.y / 10; i++) {
			float xPos = Random.Range(-50.0f, 50.0f);
			float gap = 10f;
			xPos += xPos > 0 ? gap : gap * -1;
			xPos += player.position.x;

			Vector3 pos = new Vector3(xPos, player.position.y + 10f, 0);
			op.SpawnFromPool("triangleEnemy", pos, Quaternion.identity);

		}


	}

	public void OnPlayerDeath() {
		Invoke("ShowGameOverUi", 1.5f);
	}

	void DisableUi() {
		for (int i = 0; i < uiElements.Length; i++) {
			uiElements[i].SetActive(false);
		}
	}

	void EnableUi(int index) {
		if (index < uiElements.Length) {
			uiElements[index].SetActive(true);
		}
	}

	void EnableOnlyUi(int index) {
		DisableUi();
		EnableUi(index);
	}

	void ShowGameOverUi() {
		EnableOnlyUi(1);
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Menu() {
		SceneManager.LoadScene(0);
	}
}
