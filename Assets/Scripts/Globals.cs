using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	#region Change weapon UI
	public enum CurrentWeapon {
		rocket,
		gun
	}

	static CurrentWeapon _currentWeapon;
	public static CurrentWeapon currentWeapon{
		get {
			if(_currentWeapon == null){
				_currentWeapon = CurrentWeapon.gun;
			}

			return _currentWeapon;
		}

		set{
			_currentWeapon = value;
		}
	}
	public static bool changeWeaponClickHolder = false;
	#endregion

	#region Change target UI
	public enum CurrentTarget {
		player,
		enemy
	}

	static CurrentTarget _currentTarget;
	public static CurrentTarget currentTarget{
		get {
			if(_currentTarget == null){
				_currentTarget = CurrentTarget.player;
			}
			
			return _currentTarget;
		}
		
		set{
			_currentTarget = value;
		}
	}
	public static bool changeTargetClickHolder = false;
	public static bool allowMoveCursor;
	public static Vector3 defaultCrossHairPosition;
	public static Vector3 defaultCrossHairRotation;
	#endregion

	#region Fractions
	public enum Origin 
	{
		player,
		playerAlly1,
		enemyFaction1,
		enemyFaction2,
	}
	#endregion

	static int _cameraDistance;
	public static int cameraDistance{
		get {
			if(_cameraDistance == null || _cameraDistance == 0){
				_cameraDistance = - 20;
			}
			
			return _cameraDistance;
		}
		
		set{
			_cameraDistance = value;
		}
	}
	public static float playerSpeed;
	public static Vector3 degree;
	public static Quaternion weaponPilonRotation;
}
