using UnityEngine;
using System.Collections;

public class TActionMono_FadeIn : TActionMono 
{
	public float fadeInSpeed = TAction_FadeIn.DEFAULT_FADE_SPEED;

	public override TAction CreateTAction ()
	{
		return new TAction_FadeIn (fadeInSpeed);
	}
}
