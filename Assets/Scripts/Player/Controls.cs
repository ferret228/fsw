using UnityEngine;
using System.Collections;
using System;

public class Controls : MonoBehaviour {

	public static event Action<int> KeyNum;
	public static event Action<bool> KeyQ;
	public static event Action<bool> KeyE;
	public static event Action<float> AxisH;
	public static event Action<float> AxisY;
	public static event Action<bool> Shoot;

	private static float inputX;
	private static float inputY;

	void Update () {
		if(Input.GetKey(KeyCode.Space)) {
			if(Time.timeScale != 0) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		}

		if(Input.GetKey(KeyCode.Q)){
			OnPressQ();
		}

		if(Input.GetKey(KeyCode.E)){
			OnPressE();
		}

		if(Input.GetAxis("Horizontal") != null){
			OnAxisH(inputX = Input.GetAxis("Horizontal"));
		}

		if(Input.GetAxis("Vertical") != null){
			OnAxisY(inputY = Input.GetAxis("Vertical"));
		}

		if(Input.GetKey("0")) {
			OnPressNum(0);
		}
		if(Input.GetKey("1")) {
			OnPressNum(1);
		}
		if(Input.GetKey("2")) {
			OnPressNum(2);
		}
		if(Input.GetKey("3")) {
			OnPressNum(3);
		}
		if(Input.GetKey("4")) {
			OnPressNum(4);
		}
		if(Input.GetKey("5")) {
			OnPressNum(5);
		}
		if(Input.GetKey("6")) {
			OnPressNum(6);
		}
		if(Input.GetKey("7")) {
			OnPressNum(7);
		}
		if(Input.GetKey("8")) {
			OnPressNum(8);
		}
		if(Input.GetKey("9")) {
			OnPressNum(9);
		}

		bool shoot = Input.GetButtonDown("Fire1");
		shoot |= Input.GetButtonDown("Fire2");

		if (shoot)
		{
			OnShoot();
		}

		foreach(MiniMapObjects.MiniMapObject item in MiniMapObjects.Instance.MiniMapObjectsList) {
			//if(item._tag.Equals("Player")) {
				//Debug.LogWarning(item._tag + "pos x = " + item._transform.position.x + ", pos y = " + item._transform.position.y);
			//}
		}
	}

	public static void OnPressNum(int num) {
		if(KeyNum != null)
			KeyNum(num);
	}

	public static void OnPressQ()
	{
		if (KeyQ != null)
			KeyQ(true);
	}

	public static void OnPressE()
	{
		if (KeyE != null)
			KeyE(true);
	}

	public static void OnAxisH(float f)
	{
		if (AxisH != null)
			AxisH(f);
	}

	public static void OnAxisY(float f)
	{
		if (AxisY != null)
			AxisY(f);
	}

	public static void OnShoot()
	{
		if (Shoot != null)
			Shoot(true);
	}
}
