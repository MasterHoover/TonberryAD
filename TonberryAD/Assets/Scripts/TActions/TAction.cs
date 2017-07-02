/* Serialized version of a TonberryAction.
 * 
 * A tonberryAction is an action that can be triggered in the game.
 * It can be about moving a character, changing its animation. Waiting a delay, fade In & out 
 * or changing level.
 * 
 * TonberryAction needs to define a way to send its status.
 * 
 */
using System;
using UnityEngine;
using System.Collections;

public abstract class TAction
{ 
	public delegate void ActionDoneHandler (object source, EventArgs args);
	public event ActionDoneHandler ActionDone;
	private bool forceEnd;
	private bool paused;
	private bool cancel;

	public void Launch ()
	{
		Initialize ();
		_GameManager.Instance.CinematicPlayer.StartCoroutine (Runtime ());
	}

	// Launch the action
	private IEnumerator Runtime ()
	{
		while (!cancel)
		{
			if (!paused)
			{
				RuntimeAction ();
			}

			if (forceEnd || Done)
			{
				FinalStep ();
				OnActionDone ();
				break;
			}
			yield return null;
		}
	}

	protected virtual void RuntimeAction () {}
	protected virtual void Initialize () {}

	public void Pause ()
	{
		paused = true;
	}

	public void Resume ()
	{
		paused = false;
	}

	public void End ()
	{
		forceEnd = true;
	}

	public void Cancel ()
	{
		cancel = true;
	}

	protected virtual void FinalStep (){}
	protected virtual void OnActionDone ()
	{
		//Debug.Log ("TAction: OnActionDone");
		if (ActionDone != null)
		{
			ActionDone (this, EventArgs.Empty);
		}
	}

	// EndCondition is true when the action has been completed.
	protected virtual bool Done 
	{
		get{return true;}
	}
}
