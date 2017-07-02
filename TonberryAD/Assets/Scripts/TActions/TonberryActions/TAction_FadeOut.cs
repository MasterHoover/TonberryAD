using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TAction_FadeOut : TAction 
{
	private ScreenOverlay script;
	private float fadeSpeed;
	private const float defaultFadeSpeed = 3.5f;

	public TAction_FadeOut () : this (defaultFadeSpeed) {}

	public TAction_FadeOut (float fadeSpeed)
	{
		this.fadeSpeed = fadeSpeed;
	}

	protected override void Initialize ()
	{
		script = Camera.main.GetComponent<ScreenOverlay> ();
		if (script == null)
		{
			script = Camera.main.gameObject.AddComponent<ScreenOverlay> ();
		}
		script.intensity = 1f;
		script.blendMode = ScreenOverlay.OverlayBlendMode.AlphaBlend;
	}

	protected override void RuntimeAction ()
	{
		float inc = fadeSpeed * Time.deltaTime;
		if (script.intensity - inc > 0f)
		{
			script.intensity -= inc;
		}
		else
		{
			script.intensity = 0f;
		}
	}

	protected override bool Done 
	{
		get 
		{
			return script.intensity <= 0f;
		}
	}

	public static float DEFAULT_FADE_SPEED
	{
		get{return defaultFadeSpeed;}
	}
}
