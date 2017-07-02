using UnityEngine;
using System.Collections;

public class CameraPanner : MonoBehaviour
{
	private float distance = 10f;

	void LateUpdate ()
	{
		CenterToAvatar ();
	}

	private void CenterToAvatar ()
	{
		Camera.main.transform.position = _GameManager.Instance.GameAvatar.transform.position - Camera.main.transform.forward * distance;
	}

	public float Distance
	{
		get{return distance;}
		set{distance = value;}
	}
}
