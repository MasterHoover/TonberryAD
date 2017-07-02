using UnityEngine;
using System.Collections;

public class TActionMono_FadeOut : TActionMono 
{
	public float fadeOutSpeed = TAction_FadeOut.DEFAULT_FADE_SPEED;

	public override TAction CreateTAction ()
	{
		return new TAction_FadeOut (fadeOutSpeed);
	}
}
