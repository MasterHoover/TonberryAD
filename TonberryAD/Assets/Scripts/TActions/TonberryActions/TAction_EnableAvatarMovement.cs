using UnityEngine;
using System.Collections;

public class TAction_EnableAvatarMovement : TAction 
{
	protected override void Initialize ()
	{
		_GameManager.Instance.GameAvatar.enabled = true;
	}
}
