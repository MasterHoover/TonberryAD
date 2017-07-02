using UnityEngine;
using System.Collections;

public class Input_ModifySpeed : InputAction 
{
	public ControllableSprite spriteMover;
	public float speedModifier;
	private float previousSpeed;
	private bool isActive;

	void Awake ()
	{
		if (spriteMover == null)
		{
			Debug.LogWarning ("Run(" + gameObject.name + ")/Awake () : No movementScript is assigned. " +
				"Running won't change character speed.");
			enabled = false;
		}
	}

	protected override void ButtonDownAction ()
	{
		previousSpeed = spriteMover.movementSpeed;
		spriteMover.movementSpeed *= speedModifier;
		isActive = true;
	}

	protected override void ButtonUpAction ()
	{
		spriteMover.movementSpeed = previousSpeed;
		isActive = false;
	}

	public override void ForceExit ()
	{
		if (isActive)
		{
			ButtonUpAction ();
		}
	}

	public override void ForceEnter ()
	{
		if (!isActive)
		{
			ButtonDownAction ();
		}
	}

	public bool IsActive
	{
		get{return isActive;} 
	}
}
