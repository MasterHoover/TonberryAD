using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TAction_FadeIn : TAction 
{
	private ScreenOverlay script;
	private const float defaultFadeSpeed = 3.5f;
	private float fadeSpeed;

	public TAction_FadeIn () : this (defaultFadeSpeed) {}

	public TAction_FadeIn (float fadeSpeed)
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
		script.intensity = 0f;
		script.blendMode = ScreenOverlay.OverlayBlendMode.AlphaBlend;
	}

	protected override void RuntimeAction ()
	{
		script.intensity += (fadeSpeed * Time.deltaTime);
	}

	protected override bool Done 
	{
		get 
		{
			return script.intensity >= 1f;
		}
	}

	public static float DEFAULT_FADE_SPEED
	{
		get{return defaultFadeSpeed;}
	}
}
