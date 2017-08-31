using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class SpriteAnimationInterface : MonoBehaviour 
{
	public RuntimeAnimatorController animationLogic;
	private Animator animator;

	private const string IDLE_MAIN_STATE = "Idle";
	public const string IDLE_LEFT_STATE = "Idle_Left";
	public const string IDLE_RIGHT_STATE = "Idle_Right";
	public const string IDLE_FORWARD_STATE = "Idle_Forward";
	public const string IDLE_BACK_STATE = "Idle_Back";
	public const string WALK_LEFT_STATE = "Walk_Left";
	public const string WALK_RIGHT_STATE = "Walk_Right";
	public const string WALK_FORWARD_STATE = "Walk_Forward";
	public const string WALK_BACK_STATE = "Walk_Back";
	public const string RUN_LEFT_STATE = "Run_Left";
	public const string RUN_RIGHT_STATE = "Run_Right";
	public const string RUN_FORWARD_STATE = "Run_Forward";
	public const string RUN_BACK_STATE = "Run_Back";

	private int idle_Main_Hash = Animator.StringToHash (IDLE_MAIN_STATE);
	private int idle_Left_Hash = Animator.StringToHash (IDLE_LEFT_STATE);
	private int idle_Right_Hash = Animator.StringToHash (IDLE_RIGHT_STATE);
	private int idle_Front_Hash = Animator.StringToHash (IDLE_FORWARD_STATE);
	private int idle_Back_Hash = Animator.StringToHash (IDLE_BACK_STATE);
	private int walk_Left_Hash = Animator.StringToHash (WALK_LEFT_STATE);
	private int walk_Right_Hash = Animator.StringToHash (WALK_RIGHT_STATE);
	private int walk_Front_Hash = Animator.StringToHash (WALK_FORWARD_STATE);
	private int walk_Back_Hash = Animator.StringToHash (WALK_BACK_STATE);
	private int run_Left_Hash = Animator.StringToHash (RUN_LEFT_STATE);
	private int run_Right_Hash = Animator.StringToHash (RUN_RIGHT_STATE);
	private int run_Front_Hash = Animator.StringToHash (RUN_FORWARD_STATE);
	private int run_Back_Hash = Animator.StringToHash (RUN_BACK_STATE);

	public enum SpriteAnimation
	{
		Idle_Left,
		Idle_Right,
		Idle_Front,
		Idle_Back,
		Walk_Left,
		Walk_Right,
		Walk_Front,
		Walk_Back,
		Run_Left,
		Run_Right,
		Run_Front,
		Run_Back
	}

	void Awake ()
	{
		if (animationLogic == null)
		{
			Debug.LogWarning ("SpriteAnimatorController[" + name + "]/Awake () : no runtimeAnimatorController was assigned.");
		}
		else
		{
			animator = GetComponent<Animator> ();
			animator.runtimeAnimatorController = animationLogic;
		}
	}

	public void ChangeState (string newState)
	{
		if (newState != CurrentState)
		{
			animator.Play (newState);
		}
	}

	public void ChangeState (SpriteAnimation newState)
	{
		ChangeState (newState.ToString());
	}

	public void MakeIdle ()
	{
		string currentState = CurrentState;
		if (currentState != IDLE_LEFT_STATE && 
			currentState != IDLE_RIGHT_STATE && 
			currentState != IDLE_FORWARD_STATE && 
			currentState != IDLE_BACK_STATE && 
			currentState != IDLE_MAIN_STATE)
		{
			switch (currentState)
			{
			case WALK_LEFT_STATE:
				goto case RUN_LEFT_STATE;
			case RUN_LEFT_STATE:
				ChangeState (IDLE_LEFT_STATE);
				break;
			case WALK_RIGHT_STATE:
				goto case RUN_RIGHT_STATE;
			case RUN_RIGHT_STATE:
				ChangeState (IDLE_RIGHT_STATE);
				break;
			case WALK_FORWARD_STATE:
				goto case RUN_FORWARD_STATE;
			case RUN_FORWARD_STATE:
				ChangeState (IDLE_FORWARD_STATE);
				break;
			case WALK_BACK_STATE:
				goto case RUN_BACK_STATE;
			case RUN_BACK_STATE:
				ChangeState (IDLE_BACK_STATE);
				break;
			default:
				//Debug.LogWarning ("SpriteAnimatorController[" + name + "]/MakeIdle () : currentState is unknown. Aborting MakeIdle ()");
				break;
			}
		}
	}

	private string GetNameFromHash (int hash)
	{
		if (hash == idle_Left_Hash)
		{
			return IDLE_LEFT_STATE;
		}
		else if (hash == idle_Right_Hash)
		{
			return IDLE_RIGHT_STATE;
		}
		else if (hash == idle_Front_Hash)
		{
			return IDLE_FORWARD_STATE; 
		}
		else if (hash == idle_Back_Hash)
		{
			return IDLE_BACK_STATE;
		}
		else if (hash == walk_Left_Hash)
		{
			return WALK_LEFT_STATE;
		}
		else if (hash == walk_Right_Hash)
		{
			return WALK_RIGHT_STATE;
		}
		else if (hash == walk_Front_Hash)
		{
			return WALK_FORWARD_STATE;
		}
		else if (hash == walk_Back_Hash)
		{
			return WALK_BACK_STATE;
		}
		else if (hash == run_Left_Hash)
		{
			return RUN_LEFT_STATE;
		}
		else if (hash == run_Right_Hash)
		{
			return RUN_RIGHT_STATE;
		}
		else if (hash == run_Front_Hash)
		{
			return RUN_FORWARD_STATE;
		}
		else if (hash == run_Back_Hash)
		{
			return RUN_BACK_STATE;
		}
		else if (hash == idle_Main_Hash)
		{
			return IDLE_MAIN_STATE;
		}
		else
		{
			return "";
		}
	}

	public string CurrentState
	{
		get
		{
			if (!_GameManager.Instance.SceneIsLoading)
			{
				return GetNameFromHash (animator.GetCurrentAnimatorStateInfo (0).shortNameHash);
			} 
			return GetNameFromHash (0);
		}
	}
		
	/*
	public void UpdateCharacterParams ()
	{
		UpdateCharacterParams (movingScript.Direction, movingScript.Moving, movingScript.Running);
	}

	public void UpdateCharacterParams (Vector3 direction, bool moving, bool running)
	{
		Vector3 fakeDir = new Vector3 (direction.x, 0f, direction.z);
		float angleWithCamRight = Vector3.Angle (fakeDir, Camera.main.transform.right);
		sided = angleWithCamRight < 45f || angleWithCamRight > 135f;
		if (sided)
		{
			facingLeft = Vector3.Angle (fakeDir, Camera.main.transform.right) > 90f;
		}
		else
		{
			float angleWithCamForward = Vector3.Angle (fakeDir, Camera.main.transform.forward);
			facingFront = !sided && angleWithCamForward > 90f;
		}
		this.moving = moving;
		this.running = running;

		if (StateChanged ())
		{
			animator.SetBool (FACING_LEFT_BOOL, facingLeft);
			animator.SetBool (MOVING_BOOL, moving);
			animator.SetBool (SIDED_BOOL, sided);
			animator.SetBool (FRONT_BOOL, facingFront);
			animator.SetBool (RUNNING_BOOL, running);
			animator.SetTrigger (CHANGE_STATE_TRIGGER);
		}
	}


	private bool StateChanged ()
	{
		return animator.GetBool (FACING_LEFT_BOOL) != facingLeft
		|| animator.GetBool (MOVING_BOOL) != moving
		|| animator.GetBool (SIDED_BOOL) != sided
		|| animator.GetBool (FRONT_BOOL) != facingFront
			|| animator.GetBool (RUNNING_BOOL) != running;
	}

	private CharacterState GetCurrentState ()
	{
		if (sided)
		{
			if (facingLeft)
			{
				if (!moving)
				{
					return CharacterState.Left_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Left_Walking;
					}
					else
					{
						return CharacterState.Left_Running;
					}
				}
			}
			else
			{
				if (!moving)
				{
					return CharacterState.Right_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Right_Walking;
					}
					else
					{
						return CharacterState.Right_Running;
					}
				}
			}
		}
		else
		{
			if (facingFront)
			{
				if(!moving)
				{
					return CharacterState.Front_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Front_Walking;
					}
					else
					{
						return CharacterState.Front_Running;
					}
				}
			}
			else
			{
				if(!moving)
				{
					return CharacterState.Back_Idle;
				}
				else
				{
					if (!running)
					{
						return CharacterState.Back_Walking;
					}
					else
					{
						return CharacterState.Back_Running;
					}
				}
			}
		}
	}

	public void ChangeCharacterState (CharacterState newState)
	{
		switch (newState)
		{
		case CharacterState.Front_Idle:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, true);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Front_Walking:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Front_Running:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		case CharacterState.Left_Idle:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, true);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Left_Walking:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Left_Running:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, true);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		case CharacterState.Right_Idle:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, false);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Right_Walking:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Right_Running:
			animator.SetBool (SIDED_BOOL, true);
			animator.SetBool (FACING_LEFT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		case CharacterState.Back_Idle:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, false);
			animator.SetBool (MOVING_BOOL, false);
			break;
		case CharacterState.Back_Walking:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, false);
			break;
		case CharacterState.Back_Running:
			animator.SetBool (SIDED_BOOL, false);
			animator.SetBool (FRONT_BOOL, false);
			animator.SetBool (MOVING_BOOL, true);
			animator.SetBool (RUNNING_BOOL, true);
			break;
		default:
			Debug.LogWarning ("CharacterAnimator/ChangeCharacterState (CharacterState) : the character state is unknown. Not changing state.");
			break;
		}
		animator.SetTrigger (CHANGE_STATE_TRIGGER);
	}

	public void ChangeToIdle ()
	{
		moving = false;
		animator.SetTrigger (CHANGE_STATE_TRIGGER);
	}

	public enum CharacterState
	{
		Front_Idle,
		Front_Walking,
		Front_Running,
		Left_Idle,
		Left_Walking,
		Left_Running,
		Right_Idle,
		Right_Walking,
		Right_Running,
		Back_Idle,
		Back_Walking,
		Back_Running
	}

	public bool Sided
	{
		get{return sided;}
		set{sided = value;}
	}

	public bool FacingLeft
	{
		get{return facingLeft;}
		set{facingLeft = value;}
	}

	public bool FacingFront
	{
		get{return facingFront;}
		set{facingFront = value;}
	}

	public bool Moving
	{
		get{return moving;}
		set{moving = value;}
	}

	public bool Running
	{
		get{return running;}
		set{running = value;}
	}

	public CharacterState CurrentState
	{
		get{return GetCurrentState ();}
		set{ChangeCharacterState (value);}
	}

    */
}
