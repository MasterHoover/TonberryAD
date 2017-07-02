using UnityEngine;
using System.Collections;

public class TAction_LogMessage : TAction 
{
	private string message;

	public TAction_LogMessage (string message)
	{
		this.message = message;
	}

	protected override void Initialize ()
	{
		Debug.Log (message);
	}
}
