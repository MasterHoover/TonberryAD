using UnityEngine;
using System.Collections;

[RequireComponent (typeof (View))]
public class CameraViewGizmos : CustomGizmos 
{
	public Color cameraPositionColor =  Color.yellow;
	public Color worldOrientationColor = Color.cyan;

	protected override void DrawGizmos ()
	{
		View script = GetComponent<View> ();
		if (script.cameraPosition != null)
		{
			Camera.main.transform.position = script.cameraPosition.transform.position;
			Camera.main.transform.rotation = script.cameraPosition.transform.rotation;

			Gizmos.color = cameraPositionColor;
			Gizmos.DrawLine (transform.position, script.cameraPosition.transform.position);
		}

		if (script.inputOrientation != null)
		{
			Gizmos.color = worldOrientationColor;
			Gizmos.DrawLine (transform.position, script.inputOrientation.transform.position);
		}
	}
}
