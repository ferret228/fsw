using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Threading;
using System;

public class RocketScript : MonoBehaviour {

	public float speed;
	public int damage;
	public bool isEnemyShot = false;
	public bool isGuided = true;
	public Transform target;
	private Vector3 targetPoint;
	private Quaternion targetRotation;
	public GameObject explosion;
	public bool allowTriggers = false;
	public Guid parentGuid;
	public Turret.DamageType damageType;
	public Turret.TurretSize turretSize;

	void Start () {
		Destroy(gameObject, 30);
		StartCoroutine(DelayedInit());
	}

	void Update () 
	{
		gameObject.transform.Translate(Vector2.up * (speed * Time.deltaTime));

		if(target != null) {
			Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 objectPos2 = Camera.main.WorldToScreenPoint(target.position);
			Vector3 dir = objectPos2 - objectPos;
		
			if(Vector3.Distance(transform.position, target.position) > 3 && isGuided) {
				transform.rotation = Quaternion.Slerp(transform.rotation, 
		        	                              Quaternion.Euler (new Vector3(0.0f,0.0f,(Mathf.Atan2 (dir.y,dir.x) * Mathf.Rad2Deg) - 90)), 
		            	                          0.030f);
			} else {
				isGuided = false;
			}
		}
	}

	void FixedUpdate()
	{
	}

	IEnumerator DelayedInit() {
		yield return new WaitForSeconds(0.3f);
		allowTriggers = true;
	}

	public void CustomDestroy()
	{
		Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 3.0f);
		Destroy(gameObject);
	}
}
