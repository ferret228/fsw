using UnityEngine;
using System.Collections;
using System;

public class ArmorScript : MonoBehaviour {

	public enum Parent{
		Player,
		Ship,
		Asteroid,
		Rocket,
		Bullet
	}

	public event Action<Guid> OnDamageTaken;

	public Parent parent;
	public GameObject explosion;

	// armor
	public int totalArmor;
	public int armorLightResist;
	public int armorMedResist;
	public int armorHeavyResist;
	public int armorEnergyResist;
	public int armorKineticResist;

	public void Damage(int damage) {
		totalArmor -= damage;
		
		if (totalArmor <= 0)
		{
			DestroyObject();
		}
	}

	public void Damage(int damage, Turret.DamageType damageType, Turret.TurretSize turretSize)
	{
		int totalDamage = damage;
		// calculate damage
		if(parent == Parent.Ship) {
			if(damageType == Turret.DamageType.energy) {
				if(armorEnergyResist > 0) {
					totalDamage -= damage / 100 * armorEnergyResist;
				}
			} else if(damageType == Turret.DamageType.kinetic){
				if(armorKineticResist > 0) {
					totalDamage -= damage / 100 * armorKineticResist;
				}
			}

			if(turretSize == Turret.TurretSize.small) {
				if(armorLightResist > 0) {
					totalDamage -= damage / 100 * armorLightResist;
				}
			} else if(turretSize == Turret.TurretSize.medium) {
				if(armorMedResist > 0) {
					totalDamage -= damage / 100 * armorMedResist;
				}
			} else if(turretSize == Turret.TurretSize.big){ 
				if(armorHeavyResist > 0) {
					totalDamage -= damage / 100 * armorHeavyResist;
				}
			}
		}

		totalArmor -= totalDamage;

		if (totalArmor <= 0)
		{
			DestroyObject();
		}
	}

	private void DestroyObject() {
		if (totalArmor <= 0)
		{
			switch(parent)
			{
			case Parent.Rocket:{
				RocketScript rocket = gameObject.GetComponent<RocketScript>();
				if(rocket != null && rocket.allowTriggers) {
					rocket.CustomDestroy();
				}
			}break;
				
			case Parent.Asteroid:{
				Asteroid asteroid = gameObject.GetComponent<Asteroid>();
				if(asteroid != null){
					asteroid.CustomDestroy();
				}
			}break;
				
			case Parent.Ship:{
				ShipScript ship = gameObject.GetComponent<ShipScript>();
				if(ship != null){
					ship.CustomDestroy();
				}
			}break;
			}
		}
	}

	public void TakeDamage(Guid tag) {
		if(OnDamageTaken != null) {
			OnDamageTaken(tag);
		}
	}

	void Start()
	{
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(otherCollider.GetType() == typeof(BoxCollider2D)) {
		// Is this a rocket shot?
		RocketScript rocketShot = otherCollider.gameObject.GetComponent<RocketScript>();
		if (rocketShot != null)
		{
			if(rocketShot.allowTriggers){
				TakeDamage(rocketShot.parentGuid);
					Damage(rocketShot.damage, rocketShot.damageType, rocketShot.turretSize);
				rocketShot.CustomDestroy();
			}
		}

		// Is this a gun shot?
		BulletScript bulletShot = otherCollider.gameObject.GetComponent<BulletScript>();
		if (bulletShot != null)
		{
			if(bulletShot.allowTriggers){
				TakeDamage(bulletShot.parentGuid);
				Damage(bulletShot.damage, bulletShot.damageType, bulletShot.turretSize);
				bulletShot.CustomDestroy();
			}
		}

		// Is this a asteroid collision?
		Asteroid asteroid = otherCollider.gameObject.GetComponent<Asteroid>();
		if(asteroid != null)
		{
			Damage(asteroid.damage);
		}

		// Is this a player collision?
		ShipScript ship = otherCollider.gameObject.GetComponent<ShipScript>();
		if(ship != null)
		{
			//Damage(10);
		}
		}
	}

}
