using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGizmos : MonoBehaviour 
{
	public Color color1 = Color.yellow;
	public Color color2 = Color.magenta;

	void OnDrawGizmos ()
	{
		SpawnPoint script = GetComponent<SpawnPoint> ();
		if (script != null)
		{
			if (script.cameraView != null)
			{
				if (script.cameraView.cameraPosition != null)
				{
					Gizmos.color = color1;
					Gizmos.DrawLine (transform.position, script.cameraView.cameraPosition.transform.position);
				}
				if (script.cameraView.inputOrientation != null)
				{
					Gizmos.color = color2;
					Gizmos.DrawLine (transform.position, script.cameraView.inputOrientation.transform.position);
				}
			}
		}
	}
}
