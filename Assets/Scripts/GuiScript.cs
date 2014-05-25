using UnityEngine;
using System.Collections;
using System;

public class GuiScript : MonoBehaviour {

	private bool currentTargetIsPlayer;

	void OnGUI () {
		GUI.Label(new Rect(20,40,80,20),"Speed="+Globals.playerSpeed);
		Globals.playerSpeed = GUI.VerticalSlider(new Rect(20, 90, 20, 200), Globals.playerSpeed, 100, 0);
		
		GUI.Label(new Rect(90,40,200,20),"Camera Z-axis distance="+Globals.cameraDistance);
		Globals.cameraDistance = (int)GUI.VerticalSlider(new Rect(90, 90, 20, 200), Convert.ToSingle(Globals.cameraDistance.ToString()), -1, -80);
		
		GUI.Label(new Rect(5,15,130,20),"weapon="+Globals.currentWeapon);//+Globals.CurrentWeapon);
		bool changeWeaponClick = GUI.Button(new Rect(0,0,100,20), "change weapon");
		
		if(changeWeaponClick != Globals.changeWeaponClickHolder){
			
			if(Globals.currentWeapon == Globals.CurrentWeapon.gun){
				Globals.currentWeapon = Globals.CurrentWeapon.rocket;
			} else {
				Globals.currentWeapon = Globals.CurrentWeapon.gun;
			}
		}

		int height = 0;
		int count = 1;
		foreach(MiniMapObjects.MiniMapObject item in MiniMapObjects.Instance.MiniMapObjectsList) {
			if(item._name.Contains("Ship")) {
				GUI.Label(new Rect(270,height,180,20),"("+count+") "+"target="+item._name);
				height += 10;
				count++;
			}
		}
	}
}
