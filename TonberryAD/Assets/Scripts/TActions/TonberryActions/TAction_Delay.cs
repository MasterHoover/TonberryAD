using UnityEngine;
using System.Collections;

public class TAction_Delay : TAction
{
	private float delay;

	private float initialTime;
	private float elapsedTime;

	public TAction_Delay (float delay)
	{
		this.delay = delay;
	}

	protected override void Initialize ()
	{
		initialTime = Time.time;
	}

	protected override void RuntimeAction ()
	{
		elapsedTime = Time.time - initialTime;
	}

	protected override bool Done 
	{
		get 
		{
			return elapsedTime >= delay;
		}
	}

	/*
	private void Co_StartDelay (float duration)
	{
		
	}
	*/
}
