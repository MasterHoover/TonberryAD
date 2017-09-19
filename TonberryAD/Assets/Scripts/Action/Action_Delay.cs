using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Delay : Action 
{
	public float delay;
	private bool completed;

	protected override void Init ()
	{
		Debug.Log ("Playing delay of: " + delay);
		completed = false;
		Invoke ("DelayHandler", delay);
	}

	protected override bool Runtime ()
	{
		return completed;
	}

	void DelayHandler ()
	{
		completed = true;
	}
}
