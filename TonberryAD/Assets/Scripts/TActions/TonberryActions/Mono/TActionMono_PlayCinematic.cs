using UnityEngine;
using System.Collections;

public class TActionMono_PlayCinematic : TActionMono 
{
	public string cinematicLabel;

	public override TAction CreateTAction ()
	{
		return new TAction_PlayCinematic (cinematicLabel);
	}
}
