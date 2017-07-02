using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour 
{
	public void Set ()
	{
		Camera.main.transform.position = transform.position;
		Camera.main.transform.rotation = transform.rotation;
	}
}
