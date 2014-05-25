using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PassiveSystemManager {
	// TODO abstract class
	public int index;
	public String name;
	public bool isActive = false;
	public int maxShips = 100;
	public int shipSpawnDelayInSeconds = 10;
	public List<Vector3> shipsList = new List<Vector3>();

	private Vector3 nextShipCoords = new Vector3(0,0,0);
	private float lastShipX = -10.0f;
	private float nextShipX = 1.5f;

	public PassiveSystemManager(String name) {
		this.name = name;
	}

	public void SpawnShip (GameObject shipPrefab) {
		nextShipCoords = new Vector3(lastShipX, 0, 0);
		shipsList.Add(nextShipCoords);
		lastShipX += nextShipX;
	}
}