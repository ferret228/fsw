using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turret : MonoBehaviour {

	#region turret info
	public enum TurretType{
		rocket,
		gun,
		defence
	}
	public TurretType turretType;

	public enum TurretSize{
		small,
		medium,
		big
	}
	public TurretSize turretSize;

	public enum DamageType{
		kinetic,
		energy
	}
	public DamageType damageType;
	#endregion

	public Transform prefab;
	public bool canRotate;
	public float turretRotateSpeed;
	public float angleLeft;
	public float angleRight;
	public List<Transform> guns = new List<Transform>();
	public int rotateSpeed = 20;
	public float currentRotateSpeed = 0;
	public float fireRate = 0.40f;
	public float shellSpeed = 5;
	public int damage = 2;

	private bool allowRotate = false;
	private float shootCooldown;

	void Start () {

	}
	
	void Update () {
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}

		if(allowRotate && canRotate) {
			transform.eulerAngles = new Vector3(0,0,currentRotateSpeed);
			allowRotate = false;
		}
	}

	public void RotateTurretLeft() {
		if(canRotate) {
			allowRotate = true;
			currentRotateSpeed -= (Time.deltaTime * 5 * rotateSpeed);
		}
	}

	public void RotateTurretRight() {
		if(canRotate) {
			allowRotate = true;
			currentRotateSpeed += (Time.deltaTime * 5 * rotateSpeed);
		}
	}

	public bool CanAttack
	{
		get
		{
			if(shootCooldown <= 0f) {
				shootCooldown = fireRate;
				return true;
			} else {
				return false;
			}
		}
	}
}
