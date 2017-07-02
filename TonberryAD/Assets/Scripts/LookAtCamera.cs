/* 
Class: LookAtCam
Author: Olivier Reid

Desc: 
A script that makes an object look at the main camera.
Useful for rendering a sprite perfectly in front of the camera.
*/
using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour 
{
	void Start ()
	{
		if (Camera.main == null)
		{
			Debug.Log ("LookAtCam/Start (): No MainCamera in scene. Disabling script.");
			enabled = false;
		}
	}

	void LateUpdate ()
	{
		LookAtCam ();
	}

	public void LookAtCam ()
	{
		transform.LookAt (transform.position - Camera.main.transform.forward);
	}
}
