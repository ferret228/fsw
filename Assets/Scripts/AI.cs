using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;
using System.Threading;

public class AI : MonoBehaviour
{
	#region AI behaviors
	public enum AIPattern
	{
		passive,
		active
	}

	public enum AICurrentState
	{
		idle,
		attack,
		move
	}

	public enum AITask
	{
		move,
		protect,
		follow,
		kill
	}

	public AIPattern _AIPattern;
	public AICurrentState _AICurrentState;
	public AITask _AITask;
	public Globals.Origin _Origin;
	#endregion

	public Transform weaponPilon;
	public Transform target;
	public Transform secondTarget;
	public Globals.CurrentWeapon currentWeapon;
	public bool canDistract;
	public bool canLookAt = true;
	public bool canAttack = false;
	public bool canMove = false;
	bool coroutineIsStarted = false;
	float acc;
	public float attackRange;
	public float protectRange;
	public float followRange;
	float moveSpeed;
	public Thruster thruster;
	public bool isRocketGuided;

	void Start()
	{
		if(target.position.x < weaponPilon.position.x){
			// down
			if(target.position.y < weaponPilon.position.y){
				//acc = 90.0f;
			} 
			// up
			else {
				acc = 90.0f;
			}
			// right side
		} else {
			// down
			if(target.position.y < weaponPilon.position.y){
				//acc = -90.0f;
			} 
			// up
			else {
				//acc = -90.0f;
			}
		}

		transform.eulerAngles = new Vector3(0,0,acc);
	}

	void Update ()
	{
		if(_AIPattern != AIPattern.passive){
			if(_AITask == AITask.kill){
				_AICurrentState = AICurrentState.attack;

				if(Vector3.Distance(transform.position, target.position) < attackRange){
					KillTask();
					moveSpeed = 0.0f;
				} else {
					moveSpeed = 2.0f;
					FollowTask();
				}
			}

			if(_AITask == AITask.follow){
				_AICurrentState = AICurrentState.move;

				if(Vector3.Distance(transform.position, target.position) > followRange){
					FollowTask();
				}
			}

			if(_AITask == AITask.protect) {
				// get distance between objects
				if(Vector3.Distance(transform.position, secondTarget.position) > protectRange){
					FollowTask();
				}

				// can distract ?
				if(canDistract) {
					// check if enemy in range
				}
			}

			if(thruster!=null){
				thruster.parentSpeed = moveSpeed;
			}
		} else {
			_AICurrentState = AICurrentState.idle;
			moveSpeed = 0.0f;
			canAttack = false;
		}
	}

	void FixedUpdate()
	{
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 objectPos2 = Camera.main.WorldToScreenPoint(target.position);
		Vector3 dir = objectPos2 - objectPos; 
		transform.rotation = Quaternion.Euler (new Vector3(0.0f,0.0f,(Mathf.Atan2 (dir.y,dir.x) * Mathf.Rad2Deg)-90));
	}

	void KillTask()
	{
		if(canAttack){
			Attack();
			coroutineIsStarted = false;
		} else {
			if(!coroutineIsStarted){
				coroutineIsStarted = true;
				StartCoroutine(DelayedAttack(1.5f));
			}
		}
	}

	void FollowTask()
	{
		if(canMove)
			gameObject.transform.Translate(Vector2.up * (moveSpeed * Time.deltaTime));
	}

	IEnumerator DelayedAttack(float time)
	{
		yield return new WaitForSeconds(time);

		canAttack = true;
	}

	IEnumerator LookAtTarget(float time)
	{
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 objectPos2 = Camera.main.WorldToScreenPoint(target.position);
		Vector3 dir = objectPos2 - objectPos; 
		transform.rotation = Quaternion.Euler (new Vector3(0.0f,0.0f,Mathf.Atan2 (dir.y,dir.x) * Mathf.Rad2Deg));
		canLookAt = false;
		yield return new WaitForSeconds(time);
		canLookAt = true;
	}

	void Attack()
	{
		WeaponScript weapon = GetComponent<WeaponScript> ();
		weapon.weaponPilon = weaponPilon.transform;
		weapon.weaponPilon.eulerAngles = new Vector3(0,0,transform.eulerAngles.z); 
		weapon.currentWeapon = currentWeapon;

		if(weapon.currentWeapon == Globals.CurrentWeapon.rocket)
			weapon.isRocketGuided = isRocketGuided;

		weapon.Attack ();

		canAttack = false;
	}
}
