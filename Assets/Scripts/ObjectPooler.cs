using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool {
		public string key;
		public GameObject prefab;
		public int size;

	}

	#region Singleton
	public static ObjectPooler Instance;
	private void Awake() {
		Instance = this;
	}
	#endregion

	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	void Start() {

		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools) {
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for (int i = 0; i < pool.size; i++) {
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}
			poolDictionary.Add(pool.key, objectPool);
		}

	}

	public GameObject SpawnFromPool(string key, Vector3 position, Quaternion rotation) {


		if (!poolDictionary.ContainsKey(key)) {
			Debug.LogWarning("No Pool Exists with key: " + key + ".");
			return null;
		}

		GameObject spawnedObject = poolDictionary[key].Dequeue();
		spawnedObject.SetActive(true);
		spawnedObject.transform.position = position;
		spawnedObject.transform.rotation = rotation;

		IPooledObject pooledObject = spawnedObject.GetComponent<IPooledObject>();

		if (pooledObject != null) {
			pooledObject.Init();
		}


		poolDictionary[key].Enqueue(spawnedObject);
		return spawnedObject;

	}
}
