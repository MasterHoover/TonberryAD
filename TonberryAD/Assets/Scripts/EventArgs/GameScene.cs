using UnityEngine;
using System.Collections;

public class GameScene : System.EventArgs 
{
	string name;
	int spawnIndex;

	public GameScene (string name, int spawnIndex)
	{
		this.name = name;
		this.spawnIndex = spawnIndex;
	}

	public string Name
	{
		get{return name;}
	}

	public int SpawnIndex
	{
		get{return spawnIndex;}
	}
}
