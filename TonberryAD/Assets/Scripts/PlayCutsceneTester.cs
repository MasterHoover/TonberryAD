using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutsceneTester : MonoBehaviour 
{
	public Cutscene c;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.KeypadDivide))
		{
			_GameManager.Instance.LaunchCutscene (c);
		}
	}
}
