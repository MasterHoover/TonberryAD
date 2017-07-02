using UnityEngine;
using System.Collections;

public class TAction_MoveCharacter : TAction 
{
	public ControllableSprite character;
	public Transform targetPos;
	protected float speed;
	protected bool running;

	public TAction_MoveCharacter () {}

	public TAction_MoveCharacter (ControllableSprite character, Transform targetPos, float speed, bool running)
	{
		this.character = character;
		this.targetPos = targetPos;
		this.speed = speed;
		this.running = running;
	}

	protected override void RuntimeAction ()
	{
		Vector2 flatCharPos = FlatCharPos;
		Vector2 flatTargetPos = FlatTargetPos;
		float distanceFromTarget = Vector2.Distance (flatCharPos, flatTargetPos);
		Vector2 direction = (flatTargetPos - flatCharPos).normalized;
		if (distanceFromTarget > speed * Time.deltaTime)
		{
			character.MoveSprite (direction, speed, running);
		}
		else
		{
			character.transform.position = targetPos.position;
			character.ChangeRotation (direction);
		}
	}

	protected override bool Done 
	{
		get 
		{
			return FlatCharPos.Equals (FlatTargetPos);
		}
	}

	private Vector2 FlatCharPos
	{
		get{return new Vector2 (character.transform.position.x, character.transform.position.z);}
	}

	private Vector2 FlatTargetPos
	{
		get{return new Vector2 (targetPos.position.x, targetPos.position.z);}
	}
}
