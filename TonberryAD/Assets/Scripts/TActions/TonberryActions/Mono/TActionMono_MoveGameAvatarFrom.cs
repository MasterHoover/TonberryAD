using UnityEngine;
using System.Collections;

public class TActionMono_MoveGameAvatarFrom : TActionMono_MoveGameAvatar 
{
	public Transform fromPosition;

	public override TAction CreateTAction ()
	{
		return new TAction_MoveGameAvatarFrom (targetPos, speed, running, defaultSpeed, fromPosition);
	}

	void OnDrawGizmosSelected ()
	{
		if (fromPosition != null && targetPos != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (fromPosition.position, targetPos.position);
		}
	}
}
