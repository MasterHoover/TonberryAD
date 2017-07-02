using UnityEngine;
using System.Collections;

public class TActionMono_ActivateLevelState : TActionMono 
{
	public string levelName;
	public string stateLabel;

	public override TAction CreateTAction ()
	{
		return new TAction_ActivateLevelState (levelName, stateLabel);
	}
}
