using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestChangeLevel : MonoBehaviour 
{
	public static TestChangeLevel instance;

	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.KeypadDivide))
		{
			SceneManager.LoadScene ("TestScripts");
		}
		else if (Input.GetKeyDown (KeyCode.KeypadMultiply))
		{
			SceneManager.LoadScene ("TestScripts_V02");
		}
	}
	/*
	void OnLevelWasLoaded (int level)
	{
		Debug.Log ("OnLevelWasLoaded");
	}
	*/
}
