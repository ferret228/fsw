using UnityEngine;
using System;

public class MainMenu : MonoBehaviour {
	public GameObject shipForGlobalManager;

	void OnGUI () {
		if(GUI.Button(new Rect(05,05,100,50), "New Game")) {
			GlobalManager.Instance.shipPrefab = shipForGlobalManager;
			GlobalManager.Instance.LoadLevel(null, "System1");
			GlobalManager.Instance.StartBackThread();
		}
	}
}