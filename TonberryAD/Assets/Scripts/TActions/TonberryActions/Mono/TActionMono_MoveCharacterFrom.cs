using UnityEngine;
using System.Collections;

public class TActionMono_MoveCharacterFrom : TActionMono_MoveCharacter 
{
	public Transform fromPosition;

	public override TAction CreateTAction ()
	{
		return new TAction_MoveCharacterFrom (character, targetPos, speed, running, fromPosition);
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
