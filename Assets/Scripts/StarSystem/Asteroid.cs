using UnityEngine;
using System.Collections;
using System;

public class Asteroid : MonoBehaviour {

	//Guid guid;
	public bool allowMovement;
	public float ttl = 0;
	public float moveSpeed;

	public int damage = 1;

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		//guid = Guid.NewGuid();
		//MiniMapObjects.Instance.MiniMapObjectsList.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.asteroid));

		if(ttl != 0){
			Destroy(gameObject, ttl);
		}
	}

	// Update is called once per frame
	void Update () {
		if(allowMovement){
			gameObject.transform.Translate(Vector2.up * (moveSpeed * Time.deltaTime));
		}
	} 

	public void CustomDestroy()
	{
		Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 3.0f);
		Destroy(gameObject);
	}
}
