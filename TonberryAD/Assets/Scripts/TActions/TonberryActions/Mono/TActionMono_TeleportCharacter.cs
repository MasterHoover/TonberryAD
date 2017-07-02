using UnityEngine;
using System.Collections;

public class TActionMono_TeleportCharacter : TActionMono 
{
	public ControllableSprite character;
	public Transform targetPos; 

	public override TAction CreateTAction ()
	{
		return new TAction_TeleportCharacter (character, targetPos);
	}
}
