using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Debug : Action 
{
	public string message;

	protected override bool Runtime ()
	{
		Debug.Log (message);
		return true;
	}
}
