using UnityEngine;
using System.Collections;

public class TActionMono_MoveCharacter : TActionMono 
{
	public ControllableSprite character;
	public Transform targetPos;
	public float speed = 2f;
	public bool running;

	public override TAction CreateTAction ()
	{
		return new TAction_MoveCharacter (character, targetPos, speed, running);
	}
}
