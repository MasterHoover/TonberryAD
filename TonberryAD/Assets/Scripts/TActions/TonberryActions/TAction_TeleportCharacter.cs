using UnityEngine;
using System.Collections;

public class TAction_TeleportCharacter : TAction 
{
	public ControllableSprite character;
	public Transform targetPos;

	public TAction_TeleportCharacter () {}

	public TAction_TeleportCharacter (ControllableSprite character, Transform targetPos)
	{
		this.character = character;
		this.targetPos = targetPos;
	}

	protected override void Initialize ()
	{
		character.transform.position = targetPos.position;
	} 
}
