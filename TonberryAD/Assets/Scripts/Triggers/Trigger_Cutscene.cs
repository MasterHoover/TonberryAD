using UnityEngine;
using System.Collections;

public class Trigger_Cutscene : EventTrigger 
{
	public Cutscene cutscene;

	protected override void LaunchEnterEvent (Collider col)
	{
		_GameManager.Instance.LaunchCutscene(cutscene);
	}
}
