using UnityEngine;
using System.Collections;
using System;

public class TAction_LoadScene : TAction 
{
	private string sceneName;
	private int spawnId;
	private bool fade;
	private bool levelLoaded;

	public TAction_LoadScene (string sceneName)
	{
		this.sceneName = sceneName;
	}

	public TAction_LoadScene (string sceneName, int spawnId) : this (sceneName)
	{
		this.spawnId = spawnId;
	}

	public TAction_LoadScene (string sceneName, int spawnId, bool fade) : this (sceneName, spawnId)
	{
		this.fade = fade;
	}

	protected override void Initialize ()
	{
		_GameManager.Instance.SceneWasSetup += OnLevelLoaded;
		_GameManager.Instance.LoadScene (sceneName);
	}

	public void OnLevelLoaded (object source, EventArgs e)
	{
		if (((GameScene) e).Name.Equals (sceneName))
		{
			levelLoaded = true;
		}
	}

	protected override bool Done 
	{
		get 
		{
			return levelLoaded;
		}
	}
}
