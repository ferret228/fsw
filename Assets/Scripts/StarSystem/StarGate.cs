using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class StarGate : MonoBehaviour {

	public event Action<Guid, String> ObjectEnterGate;

	public String destinationSystem;
	public float contactDistance;

	private Guid guid;

	void Start () {
		guid = Guid.NewGuid();
		MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.gate, Globals.Origin.playerAlly1, "gate_system_1"));
	}
	
	void Update () {
		if(MiniMapObjects.Instance.MiniMapObjectsList.Count > 0) {
			// replace with by all ships
			try{
			Vector3 postition = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._origin == Globals.Origin.player).SingleOrDefault()._transform.position;
			Guid playerGuid = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._origin == Globals.Origin.player).SingleOrDefault()._guid;
			if(postition != null) {
				float distance = Vector3.Distance(postition, transform.position);
				if(distance < contactDistance) {
					OnObjectEnterGate(playerGuid, destinationSystem);
				}
			}
			}catch(Exception ex) {
			}
		}
	}

	public void OnObjectEnterGate(Guid guid, String d)
	{
		if (ObjectEnterGate != null)
			ObjectEnterGate(guid, d);
	}
}
