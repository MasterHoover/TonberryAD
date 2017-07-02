using UnityEngine;
using System.Collections;

public class TActionMono_MoveGameAvatar : TActionMono 
{
	public Transform targetPos;
	public bool defaultSpeed = true;
	public bool running;
	public float speed = 2f;

	public override TAction CreateTAction ()
	{
		return new TAction_MoveGameAvatar (targetPos, speed, running, defaultSpeed);
	}
}
