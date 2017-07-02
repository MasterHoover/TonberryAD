using UnityEngine;
using System.Collections;

public class TActionMono_DisableAvatarMovement : TActionMono 
{
	public override TAction CreateTAction ()
	{
		return new TAction_DisableAvatarMovement ();
	}
}
