using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

class NewAiScript : MonoBehaviour
{
	public enum GlobalObjecive
	{
		protectSystem,
		tradeInSystem,
		attackSystem
	}
	public GlobalObjecive globalObjecive;
	
	public enum UrgentObjective
	{
		retreat,
		attack,
		none
	}
	public UrgentObjective urgentObjective;
	
	public enum AITask
	{
		move,
		protect,
		patrol,
		follow,
		kill,
		fallback
	}
	
	public bool allowAI = false;
	public Globals.Origin origin;
	public float attackRange;
	public float followRange;
	public float protectRange;
	public Transform target1;
	public Transform target2;
	public ArmorScript armorScript;
	public Transform baseLocation;
	public Transform target;
	public Transform weaponPilon;
	public Thruster thruster;
	public Globals.CurrentWeapon currentWeapon;
	public bool isRocketGuided;
	private int baseArmor;
	private bool lockTarget = false;
	private List<MiniMapObjects.MiniMapObject> bases = new List<MiniMapObjects.MiniMapObject> ();
	private List<MiniMapObjects.MiniMapObject> foes = new List<MiniMapObjects.MiniMapObject> ();
	private List<MiniMapObjects.MiniMapObject> allies = new List<MiniMapObjects.MiniMapObject> ();
	private float moveSpeed;
	private bool canLookAt = true;
	public bool canAttack = true;
	private bool canMove = true;
	private bool coroutineIsStarted = false;
	private bool allowSearchForTargets = true;
	private bool allowSearchForBases = true;
	private bool allowSetTarget = true;
	private bool allowSetBase = true;
	private bool noMoreTargets = false;
	private bool firstRun = true;
	private float moveSpeed1 = 0.0f;
	private WeaponScript weapon;
	
	// Use this for initialization
	void Start ()
	{
		armorScript.OnDamageTaken += HandleOnDamageTaken;
		MiniMapObjects.OnObjectDestroyed += HandleOnObjectDestroyed;
		baseArmor = armorScript.totalArmor;
		transform.eulerAngles = new Vector3 (0, 0, 90.0f);
		weapon = GetComponent<WeaponScript> ();

		moveSpeed = UnityEngine.Random.Range(2,10);
	}

	#region Event handlers
	void HandleOnDamageTaken (Guid obj)
	{
		Debug.LogWarning (tag + " take damage from " + obj);
		
		if (urgentObjective == UrgentObjective.none) {
			urgentObjective = UrgentObjective.attack;
			// get target by obj
			allowSetTarget = true;
			SetTarget (obj);
		}
	}

	void HandleOnObjectDestroyed (Guid obj) 
	{
		var deletedObject = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._guid == obj).FirstOrDefault();

		if(foes.Contains(deletedObject)){
			foes.Remove(deletedObject);
			Debug.LogWarning("foe was destoyed");
		}

		if(allies.Contains(deletedObject)){
			allies.Remove(deletedObject);
		}

		//GetAllTargets();
	}
	#endregion
	
	// Update is called once per frame
	void Update ()
	{
		if (allowAI) {
			if(firstRun) {
				firstRun = false;
				GetAllTargets();
				GetAllBases();
			}
			// follow principle 1
			if (armorScript.totalArmor <= baseArmor / 2) {
				Debug.LogWarning ("Low health.");
				//urgentObjective = UrgentObjective.retreat;
				// if in ally system > go to base and call for reinforcements
				// if in foes system > call for reinforcements
			}
			
			if (urgentObjective == UrgentObjective.none) {
				if (globalObjecive == GlobalObjecive.protectSystem ||
				    globalObjecive == GlobalObjecive.attackSystem) {

					SetTarget (Guid.Empty);
					if (!noMoreTargets) {
						if (target != null) {
							if (Vector3.Distance (transform.position, target.position) < attackRange) {
								KillTask ();
								moveSpeed = 0.0f;
							} else {
								moveSpeed = moveSpeed1;
								FollowTask ();
							}
						} else {
							allowSetTarget = true;
						}
					} else {
						// if protect > go to base position
						// if attack > attack planets / station etc
						target = baseLocation;
						
						if (Vector3.Distance (transform.position, target.position) > 2) {
							moveSpeed = moveSpeed1;
							FollowTask ();
						} else {
							moveSpeed = 0.0f;
						}
					}
				}
				
				if (globalObjecive == GlobalObjecive.attackSystem) {
					// get nearest target
					// if see targets > attack it
					// if no more targets > attack stations / planets
				}
				
				if (globalObjecive == GlobalObjecive.tradeInSystem) {
					SetBase (Guid.Empty);
					if (Vector3.Distance (transform.position, target.position) > 1) {
						moveSpeed = 4.0f;
						FollowTask ();
					} else {
						// delay and select new target
						allowSetBase = true;
						moveSpeed = 0.0f;
					}
					// movement between planets / stations
				}
			} else if (urgentObjective == UrgentObjective.attack) {
				if (target != null) {
					if (Vector3.Distance (transform.position, target.position) < attackRange) {
						KillTask ();
						moveSpeed = 0.0f;
					} else {
						moveSpeed = 10.0f;
						FollowTask ();
					}
				} else {
					urgentObjective = UrgentObjective.none;
					if(globalObjecive == GlobalObjecive.tradeInSystem){
						allowSetBase = true;
					}
				}
			}
			
			if (thruster != null) {
				thruster.parentSpeed = moveSpeed;
			}
		}
	}
	
	void GetAllTargets ()
	{
		//if (allowSearchForTargets) {
			allies.Clear ();
			foes.Clear ();
			foreach (MiniMapObjects.MiniMapObject item in MiniMapObjects.Instance.MiniMapObjectsList) {
				if (item._minimapObjectType == MiniMapObjects.MinimapObjectType.ship) {
					if (origin == Globals.Origin.playerAlly1) {
						if (item._origin == Globals.Origin.playerAlly1) {
							allies.Add (item);
						} else if (item._origin == Globals.Origin.player) {
							allies.Add (item);
						} else {
							foes.Add (item);
						}
					} else {
						if (item._origin != origin) {
							foes.Add (item);
						}
						
						if (item._origin == origin) {
							allies.Add (item);
						}
					}
				}
			}
			if (foes.Count > 0) {
				noMoreTargets = false;
			} else {
				noMoreTargets = true;
			}

		if(tag.Equals("enemyFrigate1")){
			//Debug.LogWarning("allies="+allies.Count+", foes="+foes.Count);
		}
	}
	
	void SetTarget (Guid targetGuid)
	{
		if(allowSetTarget) {
			if (targetGuid == Guid.Empty) {
				if(foes.Count > 0) {
					int index = UnityEngine.Random.Range (0, foes.Count);
					target = foes[index]._transform;
				} else {
					noMoreTargets = true;
				}
			} else {
				target = MiniMapObjects.Instance.MiniMapObjectsList.Where (i => i._guid == targetGuid).FirstOrDefault ()._transform;
			}
			allowSetTarget = false;
		}
	}
	
	void GetAllBases ()
	{
		foreach (MiniMapObjects.MiniMapObject item in MiniMapObjects.Instance.MiniMapObjectsList) {
			if (item._minimapObjectType == MiniMapObjects.MinimapObjectType.planet || 
			    item._minimapObjectType == MiniMapObjects.MinimapObjectType.station) {
				if (item._origin == origin) {
					bases.Add (item);
				}
			}
		}
	}

	void SetBase(Guid targetGuid) 
	{
		if(allowSetBase){
			if (targetGuid == Guid.Empty) {
				int index = UnityEngine.Random.Range (0, bases.Count);
				target = bases [index]._transform;
			} else {
				target = bases.Where(i => i._guid == targetGuid).FirstOrDefault()._transform;
			}
			allowSetBase = false;
		}
	}
	
	void FixedUpdate ()
	{
		if (target != null) {
			Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 objectPos2 = Camera.main.WorldToScreenPoint (target.position);
			Vector3 dir = objectPos2 - objectPos; 
			transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg) - 90));
		}
	}
	
	void KillTask ()
	{
		if (canAttack) {
			Attack ();
			coroutineIsStarted = false;
		} else {
			if (!coroutineIsStarted) {
				coroutineIsStarted = true;
				StartCoroutine (DelayedAttack (1.5f));
			}
		}
	}
	
	void FollowTask ()
	{
		if (canMove)
			gameObject.transform.Translate (Vector2.up * (moveSpeed * Time.deltaTime));
	}

	IEnumerator SetTargets() {
		yield return new WaitForSeconds (1000);

		//allowSearchForTargets = true;
		//allowSearchForBases = true;
		//SearchForTargets ();
		//GetNearestTarget (Guid.Empty);
	}

	IEnumerator DelayedAttack (float time)
	{
		yield return new WaitForSeconds (time);
		
		canAttack = true;
	}
	
	IEnumerator LookAtTarget (float time)
	{
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 objectPos2 = Camera.main.WorldToScreenPoint (target.position);
		Vector3 dir = objectPos2 - objectPos; 
		transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg));
		canLookAt = false;
		yield return new WaitForSeconds (time);
		canLookAt = true;
	}
	
	void Attack ()
	{
		if(weapon != null) {
			weapon.weaponPilon = weaponPilon.transform;
			weapon.weaponPilon.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z); 
			weapon.currentWeapon = currentWeapon;
		
			if (weapon.currentWeapon == Globals.CurrentWeapon.rocket)
				weapon.isRocketGuided = true;
		
			weapon.Attack ();
		}
		//canAttack = false;
	}
}

