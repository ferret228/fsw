using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Station : MonoBehaviour {
	public event Action<Guid> ObjectEnterStation;

	private Guid guid;
	public Globals.Origin origin;
	public float contactDistance;

	void Start () {
		guid = Guid.NewGuid();
		MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.station, origin, "Station"));
	}
	
	void Update () {
		if(MiniMapObjects.Instance.MiniMapObjectsList.Count > 0) {
			// replace with by all ships
			Vector3 postition = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._origin == Globals.Origin.player).SingleOrDefault()._transform.position;
			Guid playerGuid = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._origin == Globals.Origin.player).SingleOrDefault()._guid;
			if(postition != null) {
				float distance = Vector3.Distance(postition, transform.position);
				if(distance < contactDistance) {
					ObjectEnterStation(playerGuid);
				}
			}
		}
	}

	public void OnObjectEnterStation(Guid guid)
	{
		if (ObjectEnterStation != null)
			ObjectEnterStation(guid);
	}
}
