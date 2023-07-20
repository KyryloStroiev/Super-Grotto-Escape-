
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	#region Singleton
	public static ObjectPooler Instance { get; private set; }
	
	private void Awake()
	{
		Instance = this;
	}

	#endregion

	public List<Pool> pools;
	private Dictionary<string, Queue<GameObject>> poolDictionary;
	private DiContainer container;

	[Inject]
	private void Construct(DiContainer container)
	{
		this.container = container;
	}
	void Start()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = container.InstantiatePrefab(pool.prefab); 
				objectPool.Enqueue(obj);
				obj.SetActive(false);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
			return null;
		}

		GameObject objectToSpawn = poolDictionary[tag].Dequeue();
		container.Inject(objectToSpawn);
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;
		poolDictionary[tag].Enqueue(objectToSpawn);
		
		return objectToSpawn;

	}

	public void ReturnToPool(string tag, GameObject obj)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
			return;
		}

		obj.SetActive(false);
		poolDictionary[tag].Enqueue(obj);
	}

}
