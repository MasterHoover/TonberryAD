using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Action_FadeIn : Action 
{
	private ScreenOverlay overlay;
	public float speed = 0.5f;

	protected override void Init ()
	{
		overlay = _GameManager.Instance.CreateCamOverlay ();
		overlay.intensity = 1f;
	}

	protected override bool Runtime ()
	{
		overlay.intensity -= speed * Time.deltaTime;
		return overlay.intensity <= 0f;
	}
}
