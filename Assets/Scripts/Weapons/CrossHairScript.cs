using UnityEngine;
using System.Collections;

public class CrossHairScript : MonoBehaviour {

	private Transform player;		// Reference to the player.
	private Transform target;		// Reference to the target.
	private Transform crossHair;		// Reference to the target.

	void Awake ()
	{
		//player = GameObject.FindGameObjectWithTag("Player").transform;
		//target = GameObject.FindGameObjectWithTag("EnemyShip").transform;
		//crossHair = GameObject.FindGameObjectWithTag("CrossHair").transform;
		//Globals.defaultCrossHairPosition = crossHair.position;
		//Globals.defaultCrossHairRotation = gameObject.transform.eulerAngles;
	}

	void Update () {
		/*
		gameObject.transform.eulerAngles = Globals.degree;

		if(Globals.allowMoveCursor){
			if(Globals.currentTarget == Globals.CurrentTarget.player){
				gameObject.transform.eulerAngles = Globals.defaultCrossHairRotation;
				crossHair.position = new Vector3(Globals.defaultCrossHairPosition.x, Globals.defaultCrossHairPosition.y, Globals.defaultCrossHairPosition.z);
			} else {
				float distance = target.position.y - player.position.y;
				gameObject.transform.eulerAngles = Globals.defaultCrossHairRotation;
				crossHair.position = new Vector3(Globals.defaultCrossHairPosition.x, distance, Globals.defaultCrossHairPosition.z);
			}
			Globals.allowMoveCursor = false;	
		}
		*/
	}
}
