using UnityEngine;
using System.Collections;

public class TAction_DisableAvatarMovement : TAction 
{
	protected override void Initialize ()
	{
		_GameManager.Instance.GameAvatar.enabled = false;
	}
}
