using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraManager : MonoBehaviour 
{
	private ScreenOverlay overlay;

	public static void FixCamera ()
	{
		CameraPanner panner = Camera.main.GetComponent<CameraPanner> ();
		if (panner != null)
		{
			Destroy (panner);
		}
	}

	public static void PanCamera (float distance)
	{
		CameraPanner panner = Camera.main.GetComponent<CameraPanner> ();
		if (panner == null)
		{
			panner = Camera.main.gameObject.AddComponent<CameraPanner> ();
		}
		panner.Distance = distance;
	}
}
