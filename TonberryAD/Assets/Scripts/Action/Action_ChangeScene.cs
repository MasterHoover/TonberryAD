using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ChangeScene : Action 
{
	public string sceneName;
	private bool sceneFullyLoaded;

	protected override void Init ()
	{
		sceneFullyLoaded = false;
		_GameManager.Instance.SceneWasSetup += OnSceneWasSetup;
		_GameManager.Instance.LoadScene (sceneName);
	}

	protected override bool Runtime ()
	{
		return sceneFullyLoaded;
	}

	private void OnSceneWasSetup (object source)
	{
		_GameManager.Instance.SceneWasSetup -= OnSceneWasSetup;
		sceneFullyLoaded = true; 
	}
}
