using UnityEngine;
using System.Collections;

public class LookAtCamGizmos : CustomGizmos 
{
	protected override void DrawGizmos ()
	{
		if (Camera.main != null)
		{
			transform.LookAt (transform.position + Camera.main.transform.forward);
		}
	}
}
