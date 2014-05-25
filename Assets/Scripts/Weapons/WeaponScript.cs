using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WeaponScript : MonoBehaviour {

	public enum FireMode {
		volley,
		burst
	}
	public FireMode fireMode;

	// different weapons
	public Transform rocketPrefab;
	public bool isRocketGuided;
	public Transform bulletPrefab;
	public Transform target;
	public float shootingBurstRate = 0.5f;
	public float shootingAutoRate = 0.10f;
	private float shootCooldown;
	public Transform weaponPilon;
	public Globals.CurrentWeapon currentWeapon;
	public List<Transform> turrets;
	private Guid guid;

	public enum Owner {
		player,
		enemy
	}
	public Owner owner;

	void Start()
	{
		// TODO if parent == ship
		try{
			Ship ship = GetComponent<Ship> ();
			guid = ship.guid;
		} catch(Exception ex){
		}
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}
	
	public void Attack()
	{
		if (CanAttack1)
		{
			shootCooldown = shootingBurstRate;
			if(currentWeapon == Globals.CurrentWeapon.gun){
				if(fireMode == FireMode.volley) {
					foreach(Transform item in turrets){
						SpawnBullet(item);
					}
				} else if(fireMode == FireMode.burst) {
					StartCoroutine(FireDelay());
				}
			} 
			if(currentWeapon == Globals.CurrentWeapon.rocket){
				SpawnRocket(turrets[0]);
			} 
		}
	}

	void SpawnRocket(Transform trans){
		var rocketShotTransform = Instantiate(rocketPrefab) as Transform;
		rocketShotTransform.position = trans.position;
		
		RocketScript rocketShot = rocketShotTransform.gameObject.GetComponent<RocketScript>();
		rocketShot.parentGuid = guid;
		rocketShot.target = target;
		rocketShot.isGuided = isRocketGuided;
		if (rocketShot != null)
		{
			rocketShot.transform.rotation = weaponPilon.rotation;
		}
	}

	void SpawnBullet(Transform trans){
		var bulletShotTransform = Instantiate(bulletPrefab) as Transform;
		bulletShotTransform.position = trans.position;

		BulletScript bulletShot = bulletShotTransform.gameObject.GetComponent<BulletScript>();
		bulletShot.parentGuid = guid;
		if (bulletShot != null)
		{
			bulletShot.transform.rotation = weaponPilon.rotation;
		}
	}

	IEnumerator FireDelay() {
		foreach(Transform item in turrets){
			yield return new WaitForSeconds (shootingAutoRate);
			SpawnBullet(item);
		}
	}

	public bool CanAttack1
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
