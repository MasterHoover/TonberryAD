using UnityEngine;
using System.Collections;

public class TActionMono_TeleportGameAvatar : TActionMono 
{
	public Transform targetPos;

	public override TAction CreateTAction ()
	{
		return new TAction_TeleportGameAvatar (targetPos);
	}
}
