using UnityEngine;
using System.Collections;

public class SpriteAnimationHandler : MonoBehaviour 
{
	private ControllableSprite controllableSprite;
	public SpriteAnimationInterface animationInterface;

	void Awake ()
	{
		controllableSprite = GetComponent<ControllableSprite> ();

		if (controllableSprite == null)
		{
			Debug.LogWarning ("Controllable sprite wasn't found on gameobject. Disabling script.");
			enabled = false;
		}

		if (animationInterface == null)
		{
			Debug.LogWarning ("SpriteAnimationHandler[" + name + "]/Awake (): Animator controller wasn't assigned. Disabling script.");
			enabled = false;
		}
	}

	public void UpdateAnimation (Vector3 direction, bool moving, bool running)
	{
		string animation = GetAnimationStateFromInfos (direction, moving, running);
		UpdateAnimation (animation);
	}

	private void UpdateAnimation (string animation)
	{
		animationInterface.ChangeState (animation);
	}

	private string GetAnimationStateFromInfos (Vector2 direction, bool moving)
	{
		return GetAnimationStateFromInfos (direction, moving, false);
	}

	private string GetAnimationStateFromInfos (Vector2 direction, bool moving, bool running)
	{
		bool sided = IsSided (direction);
		bool facingRight = IsFacingRight (direction);
		bool facingForward = IsFacingForward (direction);

		if (!moving)
		{
			if (sided)
			{
				if (facingRight)
				{
					return SpriteAnimationInterface.IDLE_RIGHT_STATE;
				}
				else // facingRight
				{
					return SpriteAnimationInterface.IDLE_LEFT_STATE;
				}
			}
			else // fronted
			{
				if (facingForward)
				{
					return SpriteAnimationInterface.IDLE_FORWARD_STATE;
				}
				else // facingBack
				{
					return SpriteAnimationInterface.IDLE_BACK_STATE;
				}
			}
		}
		else // moving
		{
			if (!running)
			{
				if (sided)
				{
					if (facingRight)
					{
						return SpriteAnimationInterface.WALK_RIGHT_STATE;
					}
					else // facingRight
					{
						return SpriteAnimationInterface.WALK_LEFT_STATE;
					}
				}
				else
				{
					if (facingForward)
					{
						return SpriteAnimationInterface.WALK_FORWARD_STATE;
					}
					else // facingBack
					{
						return SpriteAnimationInterface.WALK_BACK_STATE;
					}
				}
			}
			else // running
			{
				if (sided)
				{
					if (facingRight)
					{
						return SpriteAnimationInterface.RUN_RIGHT_STATE;
					}
					else // facingRight
					{
						return SpriteAnimationInterface.RUN_LEFT_STATE;
					}
				}
				else // fronted
				{
					if (facingForward)
					{
						return SpriteAnimationInterface.RUN_FORWARD_STATE;
					}
					else // facingBack
					{
						return SpriteAnimationInterface.RUN_BACK_STATE;
					}
				}
			}
		}
	}

	public void Idle ()
	{
		animationInterface.MakeIdle ();
	}

	private bool IsSided (Vector2 direction)
	{
		Vector3 flatDirection = new Vector3 (direction.x, 0f, direction.y);
		float angleWithCamRight = Vector3.Angle (flatDirection, Camera.main.transform.right);
		return angleWithCamRight < 45f || angleWithCamRight > 135f;
	}

	private bool IsFacingRight (Vector2 direction)
	{
		Vector3 flatDirection = new Vector3 (direction.x, 0f, direction.y);
		return Vector3.Angle (flatDirection, Camera.main.transform.right) > 90f;
	}

	private bool IsFacingForward (Vector2 direction)
	{
		Vector3 flatDirection = new Vector3 (direction.x, 0f, direction.y);
		float angleWithCamForward = Vector3.Angle (flatDirection, Camera.main.transform.forward);
		return angleWithCamForward > 90f;
	}
}
