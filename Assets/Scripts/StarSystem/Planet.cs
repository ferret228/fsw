using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class Planet : MonoBehaviour {
	public event Action<Guid> ObjectEnterPlanet;

	private Guid guid;

	public float contactDistance;
	public Globals.Origin origin;
	
	void Start () {
		guid = Guid.NewGuid();
		MiniMapObjects.Instance.Add(new MiniMapObjects.MiniMapObject(transform, guid, MiniMapObjects.MinimapObjectType.planet, origin, "planet"));
	}
	
	void Update () {
		if(MiniMapObjects.Instance.MiniMapObjectsList.Count > 0) {
			// replace with by all ships
			Vector3 postition = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._origin == Globals.Origin.player).SingleOrDefault()._transform.position;
			Guid playerGuid = MiniMapObjects.Instance.MiniMapObjectsList.Where(i => i._origin == Globals.Origin.player).SingleOrDefault()._guid;
			if(postition != null) {
				float distance = Vector3.Distance(postition, transform.position);
				if(distance < contactDistance) {
					OnOObjectEnterPlanet(playerGuid);
				}
			}
		}
	}

	public void OnOObjectEnterPlanet(Guid guid)
	{
		if (ObjectEnterPlanet != null)
			ObjectEnterPlanet(guid);
	}
}
