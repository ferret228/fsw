using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DefenceSystem : MonoBehaviour {

	public enum ColliderType {
		box,
		circle
	}
	public ColliderType colliderType;

	public Transform bulletPrefab;
	public Transform target;
	public Transform defencePilon;
	public List<Transform> turrets;
	public float shootingAutoRate = 0.10f;
	private float shootCooldown;
	private Guid guid;

	// Use this for initialization
	void Start () {
		try{
			Ship ship = GetComponent<Ship> ();
			guid = ship.guid;
		} catch(Exception ex){
		}
		shootCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(colliderType == ColliderType.circle) {
			if(otherCollider.tag.Equals("Rocket")) {
				Debug.LogWarning("Rocket reach circle");
				target = otherCollider.gameObject.transform;
				Attack(); 
			}
		}

		if(colliderType == ColliderType.box) {
			if(otherCollider.tag.Equals("Rocket")) {
				Debug.LogWarning("Rocket reach box");
			}
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}

	public void Attack()
	{
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 objectPos2 = Camera.main.WorldToScreenPoint (target.position);
		Vector3 dir = objectPos2 - objectPos; 
		defencePilon.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90));
		shootCooldown = shootingAutoRate;
		StartCoroutine(FireDelay());
	}

	IEnumerator FireDelay() {
		foreach(Transform item in turrets){
			yield return new WaitForSeconds (shootingAutoRate);
			SpawnBullet(item);
		}

		if(target != null) {
			Attack();
		}
	}

	void SpawnBullet(Transform trans){
		var bulletShotTransform = Instantiate(bulletPrefab) as Transform;
		bulletShotTransform.position = trans.position;
		
		BulletScript bulletShot = bulletShotTransform.gameObject.GetComponent<BulletScript>();
		bulletShot.parentGuid = guid;
		if (bulletShot != null)
		{
			bulletShot.transform.rotation = defencePilon.rotation;
		}
	}
}
