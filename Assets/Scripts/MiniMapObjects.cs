using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class MiniMapObjects {

	private MiniMapObjects() {}

	public static event Action<Guid> OnObjectDestroyed;

	#region Instance
	private static MiniMapObjects instance;
	public static MiniMapObjects Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new MiniMapObjects();
			}
			return instance;
		}
	}
	#endregion

	public void Add(MiniMapObject miniMapObject)
	{
		MiniMapObjectsList.Add(miniMapObject);
	}

	public void RemoveObject(Guid guid)
	{
		int removeIndex = 0;
		foreach(MiniMapObject item in MiniMapObjectsList){
			if(item._guid == guid){
				break;
			} else {
				removeIndex++;
			}
		}
		DestroyObject(guid);
		MiniMapObjectsList.RemoveAt(removeIndex);
	}

	public void DestroyObject(Guid tag) {
		if(OnObjectDestroyed != null)
			OnObjectDestroyed(tag);
	}

	#region Data
	public enum MinimapObjectType{
		ship,
		planet,
		asteroid,
		station,
		gate
	}

	public class MiniMapObject
	{
		public Transform _transform;
		public Guid _guid;
		public MinimapObjectType _minimapObjectType;
		public Globals.Origin _origin;
		public String _name;

		public MiniMapObject(Transform transform, Guid guid, MinimapObjectType minimapObjectType, Globals.Origin origin, String name){
			_transform = transform;
			_guid = guid;
			_minimapObjectType = minimapObjectType;
			_origin = origin;
			_name = name;
		}
	}

	private List<MiniMapObject> miniMapObjectsList = new List<MiniMapObject>();

	public List<MiniMapObject> MiniMapObjectsList {
		set {
		}
		get {
			return miniMapObjectsList;
		}
	}
	#endregion
}
