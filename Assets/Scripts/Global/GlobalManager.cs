using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class GlobalManager {

	public GameObject shipPrefab;
	public ActiveSystemManager activeSystemManager;
	public  List<PassiveSystemManager> passiveSystemManagers = new List<PassiveSystemManager>();

	#region Instance
	private static GlobalManager instance;
	public static GlobalManager Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new GlobalManager();
			}
			return instance;
		}
	}
	#endregion

	private Thread thread;
	private bool allowWorker = true;

	public GlobalManager () {
		PassiveSystemManager system1 = new PassiveSystemManager("System1");
		PassiveSystemManager system2 = new PassiveSystemManager("System2");
		PassiveSystemManager system3 = new PassiveSystemManager("System3");

		system1.index = 0;
		system2.index = 1;
		system3.index = 2;

		passiveSystemManagers.Add(system1);
		passiveSystemManagers.Add(system2);
		passiveSystemManagers.Add(system3);
	}

	public void StartBackThread() {
			thread = new Thread(Worker);
			thread.Start();
	}

	public void LoadLevel(String levelName, String newLevelName) {
		allowWorker = false;
		if(thread != null)
			thread.Abort();
		Hashtable sceneArguments = new Hashtable();

		lock(passiveSystemManagers) {
			if(levelName != null) {
			//Debug.LogWarning("levelName="+levelName);
				PassiveSystemManager psmToHold = passiveSystemManagers.Where(i => i.name.Equals(levelName)).FirstOrDefault();
				if(psmToHold != null) {
					//if(psmToHold.isActive) {
						passiveSystemManagers.ElementAt(psmToHold.index).isActive = false;
						psmToHold.isActive = false;

						foreach(PassiveSystemManager item in passiveSystemManagers) {
						if(item.name.Equals(levelName)) {
							item.isActive = false;
							break;
						}
						}

					//Debug.LogWarning("!!!Add "+levelName+" to background, psm[0] is active = " + passiveSystemManagers[0].isActive);
					//}
				}
			}
				PassiveSystemManager psmToStart = passiveSystemManagers.Where(i => i.name.Equals(newLevelName)).FirstOrDefault();
			if(psmToStart != null) {
				if(!psmToStart.isActive) {
					psmToStart.isActive = true;
					Debug.LogWarning("!!!Add "+newLevelName+" to foreground");
				}

				if(psmToStart.shipsList != null && psmToStart.shipsList.Count > 0) {
					sceneArguments.Add("Ships", psmToStart.shipsList);
				}
			}
		}

		SceneManager.LoadScene(newLevelName, sceneArguments);
		allowWorker = true;
		if(thread != null)
			StartBackThread();
	}

	private void Worker() {
		int secondsPassed = 0;
		while(true) {
			if(allowWorker) {
				Thread.Sleep(1000);

				if(secondsPassed == 11) {
					secondsPassed = 0;
				}

				lock(passiveSystemManagers) {
					foreach (PassiveSystemManager psm in passiveSystemManagers) {
						Debug.LogWarning("Worker " + psm.name + ", is active = " + psm.isActive);
						if(!psm.isActive) {
							if(secondsPassed == psm.shipSpawnDelayInSeconds) {
								if(psm.shipsList.Count < psm.maxShips) {
									psm.SpawnShip(shipPrefab);
									//Debug.LogWarning("Worker! Spawn new ship in system " + psm.name);
								}
							}
						}
					}
				}

				secondsPassed++;
			}
		}
	}
}