using UnityEngine;
using System.Collections;
using System;

public class FollowScript : MonoBehaviour {

	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	private Transform player;		// Reference to the player.
	private Transform target;		// Reference to the target.
	private bool currentTargetIsPlayer;

	void OnGUI (){
		bool changeTargetClick = GUI.Button(new Rect(130,0,100,20), "Look at target");
		if(changeTargetClick != Globals.changeTargetClickHolder){
			Globals.allowMoveCursor = true;

			if(currentTargetIsPlayer){
				currentTargetIsPlayer = false;
				Globals.currentTarget = Globals.CurrentTarget.enemy;
			} else {
				currentTargetIsPlayer = true;
				Globals.currentTarget = Globals.CurrentTarget.player;
			}
		}
	}
	
	void Awake ()
	{
		currentTargetIsPlayer = true;
		//player = GameObject.FindGameObjectWithTag("Player").transform;
		//target = GameObject.FindGameObjectWithTag("EnemyShip").transform;
	}
	
	void Update ()
	{
		if(player == null) {
			try{
				player = GameObject.FindGameObjectWithTag("Player").transform;
			} catch (Exception ex) {
			}

		} else {

		if(currentTargetIsPlayer){
			transform.position = player.position + offset;
		} else {
			try{
				transform.position = target.position + offset;
			}catch(Exception ex){
				currentTargetIsPlayer = true;
				Globals.currentTarget = Globals.CurrentTarget.player;
			}
		}

		Vector3 positionCache = transform.position;
		transform.position = new Vector3(positionCache.x, positionCache.y + 2, Globals.cameraDistance);
		}
	}
}
