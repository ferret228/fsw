using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ActiveSystemManager : MonoBehaviour {

	// system params
	public String systemName;
	public List<GameObject> shipsList = new List<GameObject>();

	public Guid guid;

	// scene objects
	public GameObject enemyShipPrefab;
	public Transform playerSpawnPoint;
	public GameObject playerShipPrefab;
	public List<GameObject> planetList = new List<GameObject>();
	public List<GameObject> stationList = new List<GameObject>();
	public List<GameObject> gateList = new List<GameObject>();

	private List<Planet> planets = new List<Planet>();
	private List<Station> stations = new List<Station>();
	private List<StarGate> starGates = new List<StarGate>();

	private GameObject player;

	void Awake () {
		Hashtable sceneArguments = SceneManager.GetSceneArguments();

		if(sceneArguments != null && sceneArguments.Keys.Count > 0) {
			foreach(DictionaryEntry entry in sceneArguments) {
				if(entry.Key.Equals("Ships")) {
					List<Vector3> shipsCords = (List<Vector3>) entry.Value;
					if(shipsCords != null) {
						foreach(Vector3 shipCoord in shipsCords) {
							Instantiate(enemyShipPrefab, shipCoord, Quaternion.identity);
						}
					}
				}
			}
		}
	}

	void Start () {
		SpawnPlayer();

		#region Init planets
		foreach(GameObject item in planetList) {
			planets.Add(item.GetComponent<Planet>());
		}
		
		foreach(Planet item in planets) {
			item.ObjectEnterPlanet += HandleObjectEnterPlanet;
		}
		#endregion

		#region Init stations
		foreach(GameObject item in stationList) {
			stations.Add(item.GetComponent<Station>());
		}
		
		foreach(Station item in stations) {
			item.ObjectEnterStation += HandleObjectEnterStation;
		}
		#endregion

		#region Init gates
		foreach(GameObject item in gateList) {
			starGates.Add(item.GetComponent<StarGate>());
		}

		foreach(StarGate item in starGates) {
			item.ObjectEnterGate += HandleObjectEnterGate;
		}
		#endregion
	}

	void HandleObjectEnterGate (Guid obj, String d)
	{
		MiniMapObjects.Instance.MiniMapObjectsList.Clear();
		GlobalManager.Instance.LoadLevel(systemName, d);
	}

	void HandleObjectEnterPlanet (Guid obj)
	{
	}

	void HandleObjectEnterStation (Guid obj)
	{
	}
	
	void Update () {
		if(player == null) {
			SpawnPlayer();
		}
	}

	private void SpawnPlayer() {
		player = Instantiate(playerShipPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;
		player.tag = "Player";
		player.AddComponent("NewPlayerScript");

		NewPlayerScript newPlayerScript = player.GetComponent<NewPlayerScript>();
		newPlayerScript.SetShip(player);


		//guid = Guid.NewGuid();
		//MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.ship, Globals.Origin.player, "player"));
	}
}
