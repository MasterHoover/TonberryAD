using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour 
{
	protected virtual void Init (){}
	protected abstract bool Runtime ();
	public delegate void ActionCompletedHandler (Action action);
	public event ActionCompletedHandler ActionCompleted;

	void Awake ()
	{
		enabled = false;
	}

	void OnEnable ()
	{
		Init ();
	}

	void Update ()
	{
		if (Runtime ())
		{
			enabled = false;
			OnActionCompleted ();
		}
	}

	void OnActionCompleted ()
	{
		if (ActionCompleted != null)
		{
			ActionCompleted (this);
		}
	}
}
