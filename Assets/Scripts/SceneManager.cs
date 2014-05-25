
using System.Collections;
using System;
using UnityEngine;

public static class SceneManager
{
	private static Hashtable sceneArguments = new Hashtable();
	
	public static void LoadScene(String sceneName, Hashtable sceneArguments)
	{
		SceneManager.sceneArguments = sceneArguments;
		Application.LoadLevel(sceneName);
	}

	public static Hashtable GetSceneArguments()
	{
		return sceneArguments;
	}
}