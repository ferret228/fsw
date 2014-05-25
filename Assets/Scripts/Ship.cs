using UnityEngine;
using System.Collections;
using System;

public class Ship : MonoBehaviour {

	public Guid guid;
	public GameObject explosion;
	public Globals.Origin origin;
	public bool isAlive;
	
	void Start () {
		isAlive = true;
		guid = Guid.NewGuid();
		MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.ship, origin, "Ship"+MiniMapObjects.Instance.MiniMapObjectsList.Count));
	}
	
	void Update () {
	
	}

	public void CustomDestroy()
	{
		MiniMapObjects.Instance.RemoveObject(guid);
		isAlive = false;
		Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 3.0f);
		Destroy(gameObject);
	} 
}
