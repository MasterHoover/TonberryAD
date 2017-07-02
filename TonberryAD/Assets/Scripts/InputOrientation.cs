using UnityEngine;
using System.Collections;

public class InputOrientation : MonoBehaviour 
{
	void Awake ()
	{
		enabled = false;
	}

	void Update ()
	{
		if (Input.GetAxisRaw ("Horizontal") == 0f && Input.GetAxisRaw ("Vertical") == 0f)
		{
			Set ();
			enabled = false;
		}
	}

	public void Ready ()
	{
		enabled = true;
	}

	public void Set ()
	{
		_GameManager.Instance.GameAvatar.MovementScript.InputOrientation = transform;
	}

	public void Cancel ()
	{
		enabled = false;
	}
}
