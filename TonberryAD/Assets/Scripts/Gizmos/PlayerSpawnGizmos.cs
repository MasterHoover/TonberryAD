using UnityEngine;
using System.Collections;

public class PlayerSpawnGizmos : CustomGizmos 
{
	public Color cameraPositionColor = Color.yellow;
	public Color worldOrientationColor = Color.cyan;

	protected override void DrawGizmos ()
	{
		SpawnPoint script = GetComponent<SpawnPoint> ();
		if (script != null && script.cameraView != null)
		{
			if (script.cameraView.cameraPosition != null)
			{
				Gizmos.color = cameraPositionColor;
				Gizmos.DrawLine (transform.position, script.cameraView.cameraPosition.transform.position);
			}

			if (script.cameraView.inputOrientation != null)
			{
				Gizmos.color = worldOrientationColor;
				Gizmos.DrawLine (transform.position, script.cameraView.inputOrientation.transform.position);
			}
		}
	}
}
