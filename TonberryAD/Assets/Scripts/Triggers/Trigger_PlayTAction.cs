using UnityEngine;
using System.Collections;

public class Trigger_PlayTAction : EventTrigger 
{
	public TActionMono tAction;

	protected override void LaunchEnterEvent (Collider col)
	{
		if (tAction != null)
		{
			tAction.CreateTAction ().Launch ();
		}
	}
}
