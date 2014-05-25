using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour {

	// spawners
	public List<GameObject> spawners; 

	// asteroids
	public List<Transform> asteroidPredabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject spawnerItem in spawners){
			var spawner = (AsteroidSpawner)spawnerItem.GetComponent("AsteroidSpawner");
			if(spawner.allowSpawn){
				Spawn(spawner.transform, spawner.vectorSpawnPoint);
				spawner.StartRespawnCounter();
			}
		}
	}

	void Spawn(Transform transform, AsteroidSpawner.VectorSpawnPoint vectorSpawnPoint)
	{
		// select asteroid
		var asteroidTransform = Instantiate(asteroidPredabs[Random.Range(0,3)]) as Transform;
		asteroidTransform.position = transform.position;
		
		// initiate new asteroid object
		Asteroid asteroid = asteroidTransform.gameObject.GetComponent<Asteroid>();

		asteroid.ttl = 40.0f;
		asteroid.moveSpeed = Random.Range(1.0f, 20.0f);

		switch(vectorSpawnPoint){
			case AsteroidSpawner.VectorSpawnPoint.topRight:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(90,180));
			} break;
			case AsteroidSpawner.VectorSpawnPoint.topLeft:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(-90,-180));
			} break;
			case AsteroidSpawner.VectorSpawnPoint.bottomRight:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(0,90));
			} break;
			case AsteroidSpawner.VectorSpawnPoint.bottomLeft:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(0,-90));
			} break;

			case AsteroidSpawner.VectorSpawnPoint.mediumLeft:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(-10,-180));
			}break;
			case AsteroidSpawner.VectorSpawnPoint.mediumRight:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(10,180));
			}break;

			case AsteroidSpawner.VectorSpawnPoint.topDown:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(95,275));
			}break;
			case AsteroidSpawner.VectorSpawnPoint.bottomUp:{
				asteroid.transform.eulerAngles = new Vector3(0,0,Random.Range(90,-90));
			}break;
		}

		asteroid.allowMovement = true;
	}

	void Destroy()
	{
	}
}
