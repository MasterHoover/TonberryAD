using UnityEngine;
using System.Collections;

public class SceneLoadedData : System.EventArgs 
{
	private string levelName;

	public SceneLoadedData (string levelName)
	{
		this.levelName = levelName;
	}

	public string LevelName
	{
		get{return levelName;}
	}
}
