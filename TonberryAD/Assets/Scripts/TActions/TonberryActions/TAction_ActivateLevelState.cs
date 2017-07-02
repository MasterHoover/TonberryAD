using UnityEngine;
using System.Collections;

public class TAction_ActivateLevelState : TAction 
{
	private string levelName;
	private string label;

	public TAction_ActivateLevelState (string levelName, string label)
	{
		this.levelName = levelName;
		this.label = label;
	}

	protected override void Initialize ()
	{
		_GameManager.Instance.LevelStateMachine.ActivateLevelState (levelName, label);
	}

	public string LevelName
	{
		get{return levelName;}
	}

	public string Label
	{
		get{return label;}
	}
}
