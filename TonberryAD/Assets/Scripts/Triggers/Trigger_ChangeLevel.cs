using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Trigger_ChangeLevel : EventTrigger 
{
	public string levelName;
	public int spawnId;
	public bool useFading = true;

	protected override void LaunchEnterEvent (Collider col)
	{
		//Debug.Log ("[" + name + "]: Changing level. New level: " + levelName + "; SpawnId: " + spawnId);
		_GameManager.Instance.LoadSceneWithFading (levelName, spawnId);
	}
}
