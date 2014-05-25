using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class ShipScript : MonoBehaviour {

	public Thruster thruster;
	public ParticleSystem strafeLeft;
	public ParticleSystem strafeRight;
	public GameObject explosion;

	// armor stats
	public int baseArmor = 0;
	public int armorLightResist = 0;
	public int armorMedResist = 0;
	public int armorHeavyResist = 0;
	public int armorEnergyResist = 0;
	public int armorKineticResist = 0;

	// shield stats
	public int baseShield = 0;
	public int shieldLightResist = 0;
	public int shieldMedResist = 0;
	public int shieldHeavyResist = 0;
	public int shieldEnergyResist = 0;
	public int shieldKineticResist = 0;

	// modules
	public List<AbstractModule> modules = new List<AbstractModule>();

	// movement modules
	public float maxSpeed = 2;
	public float minSpeed = -1;
	public float speedPerSecond = 1;
	public float maxRotateSpeed = 10;
	public float currentMoveSpeed = 0;
	public float currentRotateSpeed = 0;

	private bool rotatingLeft = false;
	private bool rotatingRight = false;

	// warfare modules
	private NewWeaponScript newWeaponScript;

	// base
	private Guid guid;
	private ArmorScript armorScript;

	public void SetGuid(Guid guid) {
		this.guid = guid;
	}

	void Start () {
		// add scripts
		newWeaponScript = GetComponent<NewWeaponScript>();
		newWeaponScript.SetParentGuid(guid);

		armorScript = GetComponent<ArmorScript>();
		armorScript.totalArmor = baseArmor;
		armorScript.OnDamageTaken += HandleOnDamageTaken;
		UpdateArmorScript();
		// add modules
		modules.Add(new EngineModule("", "", Module.engine, 0, 0));

		maxSpeed = modules[0].ApplyModuleMultiplyer1(maxSpeed);
	}

	void HandleOnDamageTaken (Guid obj)
	{
		// take damage from...
	}
	
	void Update () {
		if(strafeLeft != null && strafeRight != null) {
			if(!rotatingLeft) {
				strafeLeft.maxParticles = 0;
			} else {
				strafeLeft.maxParticles = 100;
			}
		
			if(!rotatingRight) {
				strafeRight.maxParticles = 0;
			} else {
				strafeRight.maxParticles = 100;
			}
		}

		if(thruster != null) {
			if(currentMoveSpeed > 0) {
				thruster.parentSpeed = 100;
			} else {
				thruster.parentSpeed = 0;
			}
		}

		transform.eulerAngles = new Vector3(0,0,currentRotateSpeed);

		rotatingLeft = false;
		rotatingRight = false;
	}

	void FixedUpdate() {
		transform.Translate(Vector2.up * (currentMoveSpeed * Time.deltaTime));
	}

	public void CustomDestroy() {
		Destroy(Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation), 3.0f);
		Destroy(gameObject);
	}

	#region Movement
	public void RotateLeft() {
		rotatingLeft = true;
		currentRotateSpeed -= (maxRotateSpeed * Time.deltaTime);
	}

	public void RotateRight() {
		rotatingRight = true;
		currentRotateSpeed += (maxRotateSpeed * Time.deltaTime);
	}

	public void SetMoveSpeed(float speed) {
		if(speed > 0) {
			if(currentMoveSpeed + (speedPerSecond * Time.deltaTime) < maxSpeed) {
				currentMoveSpeed += (speedPerSecond * Time.deltaTime);
			} else {
				currentMoveSpeed = maxSpeed;
			}
		} else if(speed < 0) {
			if(currentMoveSpeed - (speedPerSecond * Time.deltaTime) > minSpeed) {
				currentMoveSpeed -= (speedPerSecond * Time.deltaTime);
			} else {
				currentMoveSpeed = minSpeed;
			}
		}
	}
	#endregion

	#region Warfare
	public void RotateTurretsLeft() {
		newWeaponScript.RotateTurretsLeft();
	}

	public void RotateTurretsRight() {
		newWeaponScript.RotateTurretsRight();
	}

	public void ChangeWeapon(int i) {
		newWeaponScript.ChangeWeapon(i);
	}

	public void ChangeWeaponMode() {
		newWeaponScript.ChangeWeaponMode();
	}

	public void Shoot() {
		newWeaponScript.Attack();
	}
	#endregion

	#region Modules
	public void UpdateArmorScript() {
		armorScript.armorLightResist = armorLightResist;
		armorScript.armorMedResist = armorMedResist;
		armorScript.armorHeavyResist = armorHeavyResist;
		armorScript.armorKineticResist = armorKineticResist;
		armorScript.armorEnergyResist = armorEnergyResist;
	}
	#endregion
}