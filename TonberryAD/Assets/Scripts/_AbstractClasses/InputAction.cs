using UnityEngine;
using System.Collections;

public abstract class InputAction : MonoBehaviour 
{
	public bool useKeyboard = true;
	public string inputName;
	public KeyCode key;

	void Update ()
	{
		if (Input.GetButtonDown (inputName) || useKeyboard && Input.GetKeyDown (key))
		{
			ButtonDownAction ();
		}
		if (Input.GetButtonDown (inputName) || useKeyboard && Input.GetKeyUp (key))
		{
			ButtonUpAction ();
		}
		if (Input.GetButton (inputName) || useKeyboard && Input.GetKey (key))
		{
			ButtonAction ();
		}
		else
		{
			NotButtonAction ();
		}
	}

	protected virtual void ButtonDownAction (){}
	protected virtual void ButtonAction (){}
	protected virtual void ButtonUpAction (){}
	protected virtual void NotButtonAction (){}
	public virtual void ForceExit (){}
	public virtual void ForceEnter (){}
}
