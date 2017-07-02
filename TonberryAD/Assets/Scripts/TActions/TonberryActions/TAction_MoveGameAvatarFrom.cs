using UnityEngine;
using System.Collections;

public class TAction_MoveGameAvatarFrom : TAction_MoveGameAvatar 
{
	public Transform from;

	public TAction_MoveGameAvatarFrom (Transform targetPos, float speed, bool running, bool defaultSpeed, Transform from) : base (targetPos, speed, running, defaultSpeed)
	{
		this.from = from;
	}

	protected override void Initialize ()
	{
		base.Initialize ();
		character.transform.position = from.position;
		character.transform.rotation = from.rotation;
	}
}
