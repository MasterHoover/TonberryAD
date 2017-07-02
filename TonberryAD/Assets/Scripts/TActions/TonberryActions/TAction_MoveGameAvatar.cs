using UnityEngine;
using System.Collections;

public class TAction_MoveGameAvatar : TAction_MoveCharacter
{
	public TAction_MoveGameAvatar (Transform targetPos, float speed, bool running, bool defaultSpeed)
	{
		this.character = _GameManager.Instance.GameAvatar;
		this.targetPos = targetPos;
		this.running = running;
		if (defaultSpeed)
		{
			if (running)
			{
				character.RunningScript.ForceEnter ();
			}
			else
			{
				character.RunningScript.ForceExit ();
			}
			this.speed = character.movementSpeed;
		}
		else
		{
			this.speed = speed;
		}
	}

	protected override void Initialize ()
	{
		base.Initialize ();
		character.enabled = false;
		character.RunningScript.ForceExit ();
	}

	protected override void FinalStep ()
	{
		character.enabled = true;
	}
}
