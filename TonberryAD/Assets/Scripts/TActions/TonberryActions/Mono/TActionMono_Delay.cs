using UnityEngine;
using System.Collections;

public class TActionMono_Delay : TActionMono 
{
	public float delay;

	public override TAction CreateTAction ()
	{
		return new TAction_Delay (delay);
	}
}
