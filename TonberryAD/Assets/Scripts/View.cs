using UnityEngine;
using System.Collections;

public class View : MonoBehaviour
{
	public enum CameraType
	{
		fix,
		pan
	}

	public CameraType cameraType;
	public CameraPosition cameraPosition;
	public InputOrientation inputOrientation;
	public float camDistance = 15f;

	public void Activate ()
	{
		cameraPosition.Set ();
		if (cameraType == CameraType.fix)
		{
			CameraManager.FixCamera ();
		}
		else if (cameraType == CameraType.pan)
		{
			CameraManager.PanCamera (camDistance);
		}

		if (inputOrientation != null)
		{
			inputOrientation.Ready ();
		}
	}
}
