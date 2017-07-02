using UnityEngine;
using System.Collections;

public class TAction_TeleportGameAvatar : TAction_TeleportCharacter 
{
	public TAction_TeleportGameAvatar () {}

	public TAction_TeleportGameAvatar (Transform targetPos)
	{
		this.targetPos = targetPos;
	}

	protected override void Initialize ()
	{
		_GameManager.Instance.GameAvatar.transform.position = targetPos.position;
	}
}
