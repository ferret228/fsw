using UnityEngine;
using System.Collections;
using System;

public class NewPlayerScript : MonoBehaviour {

	public Guid guid;

	private GameObject shipObject;
	private ShipScript shipScript;

	void Start() {
		guid = Guid.NewGuid();
		MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.ship, Globals.Origin.player, "player"));

		Controls.KeyNum += HandleKeyNum;
		Controls.KeyQ += HandleKeyQ;
		Controls.KeyE += HandleKeyE;
		Controls.AxisH += HandleAxisH;
		Controls.AxisY += HandleAxisY;
		Controls.Shoot += HandleShoot;
	}

	void Update () {
	}

	public void SetShip(GameObject gameObject) {
		shipObject = gameObject;
		shipScript = shipObject.GetComponent<ShipScript>();
		shipScript.SetGuid(guid);
	}

	void HandleKeyNum (int obj)
	{
		shipScript.ChangeWeapon(obj);
	}
	
	void HandleKeyQ (bool obj)
	{
		shipScript.RotateTurretsLeft();
	}
	
	void HandleKeyE (bool obj)
	{
		shipScript.RotateTurretsRight();
	}
	
	void HandleShoot (bool obj)
	{
		shipScript.Shoot();
	}
	
	void HandleAxisH (float obj)
	{
		if(obj>0){
			shipScript.RotateLeft();
		}
		
		if(obj<0){
			shipScript.RotateRight();
		}
	}

	void HandleAxisY (float obj)
	{
		shipScript.SetMoveSpeed(obj);
	}
	
}