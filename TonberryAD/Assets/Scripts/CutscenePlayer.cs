using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePlayer : MonoBehaviour 
{
	private Cutscene cinematic;
	private int index;
	public delegate void CinematicDoneHandler (CutscenePlayer cp);
	public event CinematicDoneHandler CinematicDone;
	public Cutscene cutscenePrototype;

	public void Launch (Cutscene c)
	{
		index = 0;
		cinematic = c;
		LaunchAction ();
	}

	public void LaunchAction ()
	{
		Debug.Log ("Playing Step: " + index + ": " + cinematic.actions[index].name);
		cinematic.actions [index].enabled = true;
		cinematic.actions [index].ActionCompleted += OnAction2Completed;
	}

	private void OnAction2Completed (Action action)
	{
		action.ActionCompleted -= OnAction2Completed;
		Destroy (action.gameObject);
		if (++index < cinematic.actions.Length)
		{
			LaunchAction ();
		}
		else
		{
			Debug.Log ("CinematicIsDone");
			Destroy (cinematic.gameObject);
			Destroy (this);
			OnCinematicDone ();
		}
	}

	void OnCinematicDone ()
	{
		if (CinematicDone != null)
		{
			CinematicDone (this);
		}
	}
}
