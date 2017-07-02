using UnityEngine;
using System.Collections;

public class TAction_MoveCharacterFrom : TAction_MoveCharacter 
{
	public Transform from;

	public TAction_MoveCharacterFrom () : base () {}

	public TAction_MoveCharacterFrom (ControllableSprite character, Transform targetPos, float speed, bool running, Transform from)
		: base (character, targetPos, speed, running)
	{
		this.from = from;
	}

	protected override void Initialize ()
	{
		character.transform.position = from.position;
		character.ChangeRotation (new Vector2 (from.forward.x, from.forward.z));
		base.Initialize ();
	}
}
