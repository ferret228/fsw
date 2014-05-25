using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public float spawnSpeed;
	public float spawnError;
	public bool allowSpawn;

	public enum VectorSpawnPoint{
		topLeft,
		topRight,
		bottomLeft,
		bottomRight,
		mediumLeft,
		mediumRight,
		topDown,
		bottomUp
	}
	public VectorSpawnPoint vectorSpawnPoint;

	// Use this for initialization
	void Start () {
		allowSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartRespawnCounter()
	{
		allowSpawn = false;
		StartCoroutine(StartCounter());
	}

	IEnumerator StartCounter()
	{
		yield return new WaitForSeconds(Random.Range(5.0f, 31.0f));

		allowSpawn = true;
	}
}
