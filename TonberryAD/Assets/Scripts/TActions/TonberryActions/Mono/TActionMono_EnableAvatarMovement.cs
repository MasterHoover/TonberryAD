using UnityEngine;
using System.Collections;

public class TActionMono_EnableAvatarMovement : TActionMono 
{
	public override TAction CreateTAction ()
	{
		return new TAction_EnableAvatarMovement ();
	}
}
