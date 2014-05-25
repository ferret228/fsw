using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class NewWeaponScript : MonoBehaviour
{
	public enum CurrentWeapon {
		light,
		medium,
		heavy,
		rockets,
		none
	}
	public CurrentWeapon currentWeapon = CurrentWeapon.none;

	public enum WeaponMode {
		volley,
		burst
	}
	public WeaponMode weaponMode = WeaponMode.volley;

	public List<Turret> turrets = new List<Turret>();

	private Guid guid;

	void Start () {
	}
	
	void Update () {
	}

	public void SetParentGuid(Guid guid) {
		this.guid = guid;
	}

	public void Attack()
	{
		if(currentWeapon != CurrentWeapon.rockets && currentWeapon != CurrentWeapon.none) {
			if(weaponMode == WeaponMode.burst) {
				StartCoroutine(FireDelay());
			} else {
				foreach(Turret turret in turrets){
					if(turret.turretType != Turret.TurretType.rocket && turret.CanAttack) {
						foreach(Transform item in turret.guns){
							if(currentWeapon == CurrentWeapon.light) {
								if(turret.turretSize == Turret.TurretSize.small) {
									SpawnBullet(turret.prefab, item, turret.damage, turret.shellSpeed, turret.damageType, turret.turretSize);
								}
							} else if (currentWeapon == CurrentWeapon.medium) {
								if(turret.turretSize == Turret.TurretSize.medium) {
									SpawnBullet(turret.prefab, item, turret.damage, turret.shellSpeed, turret.damageType, turret.turretSize);
								}
							} else if(currentWeapon == CurrentWeapon.heavy) {
								if(turret.turretSize == Turret.TurretSize.big) {
									SpawnBullet(turret.prefab, item, turret.damage, turret.shellSpeed, turret.damageType, turret.turretSize);
								}
							}
						}
					}
				}
			}
		} else if(currentWeapon == CurrentWeapon.rockets){
			foreach(Turret turret in turrets){
				if(turret.turretType == Turret.TurretType.rocket && turret.CanAttack){
					foreach(Transform item in turret.guns){
						SpawnRocket(turret.prefab, item, turret.damage, turret.shellSpeed, turret.damageType, turret.turretSize);
					}
				}
			}
		}
	}

	IEnumerator FireDelay() {
		foreach(Turret turret in turrets){
			foreach(Transform item in turret.guns){
				yield return new WaitForSeconds (turret.fireRate);
				SpawnBullet(turret.prefab, item, turret.damage, turret.shellSpeed, turret.damageType, turret.turretSize);
			}
		}
	}

	private void SpawnBullet(Transform prefab, Transform gun, int damage, float speed1, Turret.DamageType damageType, Turret.TurretSize turretSize){
		var bulletShotTransform = Instantiate(prefab) as Transform;
		bulletShotTransform.position = gun.position;
		
		BulletScript bulletShot = bulletShotTransform.gameObject.GetComponent<BulletScript>();
		if (bulletShot != null)
		{
			bulletShot.damageType = damageType;
			bulletShot.turretSize = turretSize;
			bulletShot.speed = speed1;
			bulletShot.damage = damage;
			bulletShot.parentGuid = guid;
			bulletShot.transform.rotation = gun.rotation;
		}
	}

	public void SpawnRocket(Transform prefab, Transform gun, int damage, float speed1, Turret.DamageType damageType, Turret.TurretSize turretSize) 
	{
		var rocketShotTransform = Instantiate(prefab) as Transform;
		
		RocketScript rocketShot = rocketShotTransform.gameObject.GetComponent<RocketScript>();
		rocketShot.damageType = damageType;
		rocketShot.turretSize = turretSize;
		rocketShot.speed = speed1;
		rocketShot.damage = damage;
		rocketShot.parentGuid = guid;
		// rocketShot.target = target;
		rocketShot.isGuided = false;
		if (rocketShot != null)
		{
			rocketShot.transform.position = gun.position;
			rocketShot.transform.rotation = gun.rotation;
		}
	}

	public void RotateTurretsLeft() {
		foreach(Turret turret in turrets) {
			if(currentWeapon == CurrentWeapon.light) {
				if(turret.turretSize == Turret.TurretSize.small) {
					turret.RotateTurretLeft();
				}
			} else if (currentWeapon == CurrentWeapon.medium) {
				if(turret.turretSize == Turret.TurretSize.medium) {
					turret.RotateTurretLeft();
				}
			} else if(currentWeapon == CurrentWeapon.heavy) {
				if(turret.turretSize == Turret.TurretSize.big) {
					turret.RotateTurretLeft();
				}
			}
		}
	}

	public void RotateTurretsRight() {
		foreach(Turret turret in turrets) {
			if(currentWeapon == CurrentWeapon.light) {
				if(turret.turretSize == Turret.TurretSize.small) {
					turret.RotateTurretRight();
				}
			} else if (currentWeapon == CurrentWeapon.medium) {
				if(turret.turretSize == Turret.TurretSize.medium) {
					turret.RotateTurretRight();
				}
			} else if(currentWeapon == CurrentWeapon.heavy) {
				if(turret.turretSize == Turret.TurretSize.big) {
					turret.RotateTurretRight();
				}
			}
		}
	}

	public void ChangeWeapon(int i) {
		switch(i) {
			case 1:{
				Debug.LogWarning("Medium!");
				currentWeapon = CurrentWeapon.medium;
			}break;

			case 2:{
				Debug.LogWarning("Heavy!");
				currentWeapon = CurrentWeapon.heavy;
			}break;

			case 3:{
				Debug.LogWarning("Rockets!");
				currentWeapon = CurrentWeapon.rockets;
			}break;

			case 4:{
				Debug.LogWarning("light!");
				currentWeapon = CurrentWeapon.light;
			}break;

			case 5:{
				Debug.LogWarning("light!");
				currentWeapon = CurrentWeapon.light;
			}break;
		}
	}

	public void ChangeWeaponMode() {
		if(weaponMode == WeaponMode.burst) {
			weaponMode = WeaponMode.volley;
		} else {
			weaponMode = WeaponMode.burst;
		}
	}
}

