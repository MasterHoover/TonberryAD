using UnityEngine;
using System.Collections;

public class TActionMono_LogMessage : TActionMono 
{
	public string message;

	public override TAction CreateTAction ()
	{
		return new TAction_LogMessage (message);
	}
}
