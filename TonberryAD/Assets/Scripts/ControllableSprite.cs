/* 
Class: ControllableSprite
Author: Olivier Reid

Desc: 
Master script. 
It defines the behavior of what a Controllable sprite is in TonberryAD.
*/
using UnityEngine;
using System.Collections;

public class ControllableSprite : MonoBehaviour 
{
	private MovementInterface movementScript;
	private SnapToCollision snappingScript;
	private LookAtCamera lookAtScript;
	private Input_ModifySpeed runningScript;
	private SpriteAnimationHandler spriteAnimationHandlerScript;

	public float movementSpeed = 2f;
	private bool isMoving;

	void Awake ()
	{
		movementScript = GetComponent<MovementInterface> ();
		snappingScript = GetComponent<SnapToCollision> ();
		lookAtScript = GetComponent<LookAtCamera> ();
		runningScript = GetComponent<Input_ModifySpeed> ();
		spriteAnimationHandlerScript = GetComponent<SpriteAnimationHandler> ();
	}

	void Update ()
	{
		MoveWithInputs ();
	}
		
	private Vector2 MoveWithInputs ()
	{
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
		return MoveSprite (new Vector2 (x,y).normalized);
	}

	public Vector2 MoveSprite (Vector2 movement)
	{
		return MoveSprite (movement, movementSpeed, runningScript.IsActive);
	}

	public Vector2 MoveSprite (Vector2 movement, float speed, bool running)
	{
		Vector2 dir = Vector2.zero;
		movement = movement.normalized * speed * Time.deltaTime;
		isMoving = movement != Vector2.zero;
		if (isMoving)
		{
			dir = movementScript.Move (movement);
		}
		if (isMoving)
		{
			FacingDirection = movement;
		}
		UpdateAnimation (FacingDirection, isMoving, running);
		return dir;
	}

	public void ChangeRotation (Vector2 lookingDirection)
	{
		FacingDirection = lookingDirection;
		UpdateAnimation (lookingDirection, false, false);
	}

	private void UpdateAnimation (Vector3 direction, bool moved, bool running)
	{
		spriteAnimationHandlerScript.UpdateAnimation (direction, moved, running);
	}

	void OnDisable ()
	{
		spriteAnimationHandlerScript.Idle ();
	}
		
	public MovementInterface MovementScript
	{
		get{return movementScript;}
	}

	public SpriteAnimationHandler SpriteAnimationHandler
	{
		get{return spriteAnimationHandlerScript;}
	}

	public Input_ModifySpeed RunningScript
	{
		get{return runningScript;}
	}

	public SnapToCollision SnappingScript
	{
		get{return snappingScript;}
	}

	public LookAtCamera LookAtScript
	{
		get{return lookAtScript;}
	}

	public Vector2 FacingDirection
	{
		get{return new Vector2 (transform.forward.x, transform.forward.z);}
		set{transform.forward = new Vector3 (value.x, 0f, value.y);}
	}

	public bool IsMoving
	{
		get{return isMoving;}
	}
}
