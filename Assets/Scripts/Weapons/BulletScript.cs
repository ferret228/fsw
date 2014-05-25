using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class BulletScript : MonoBehaviour {

	public float speed;
	public int damage;
	public bool allowTriggers = false;
	public GameObject explosion;
	public Guid parentGuid;
	public Turret.DamageType damageType;
	public Turret.TurretSize turretSize;

	void Start () {
		float distort = UnityEngine.Random.Range(-2.0f, 1.0f);
		Vector3 cache = gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(cache.x, cache.y, cache.z + distort);

		if(GetComponent<MeshRenderer>() != null) {
			MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
			meshRenderer.sortingOrder = 1;
		}

		Destroy(gameObject, 5);
		Thread t = new Thread(Wait);
		t.Start();
	}
	
	void Update () {
		gameObject.transform.Translate(Vector2.up * (speed * Time.deltaTime));
	}

	void Wait()
	{
		Thread.Sleep(50);
		allowTriggers = true;
	}

	public void CustomDestroy()
	{
		Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 2.0f);
		Destroy(gameObject);
	}
}
