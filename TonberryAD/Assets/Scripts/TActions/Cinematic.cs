using UnityEngine;
using System.Collections;
using System;

public class Cinematic
{
	private static uint generatedID;
	protected uint id;

	private CinematicStep[] cinematicSteps;
	private ushort stepIndex;

	public delegate void CinematicDoneHandler (object obj, EventArgs e);
	public event CinematicDoneHandler CinematicDone;

	public Cinematic (TAction[][] sequence)
	{
		id = generatedID++;
		cinematicSteps = new CinematicStep[sequence.Length];
		for (int i = 0; i < sequence.Length; i++)
		{
			cinematicSteps[i] = new CinematicStep (sequence[i]);
		}
	}

	public void Play ()
	{
		stepIndex = 0;
		if (cinematicSteps != null)
		{
			if (cinematicSteps.Length > 0)
			{
				cinematicSteps[0].CinematicStepDone += OnCinematicStepDone;
				cinematicSteps[0].Launch ();
			}
			else
			{
				//Debug.LogWarning ("Cinematic step has a length of 0");
			}
		}
		else
		{
			//Debug.LogWarning ("Cinematic step is null");	
		}
	}

	public void Stop ()
	{
		//Debug.Log ("Cinematic with id " + id + " is calling his Stop () at index " + stepIndex);
		cinematicSteps[stepIndex].Stop ();
		stepIndex = ushort.MaxValue;
	}

	public void Next ()
	{
		if (stepIndex++ < cinematicSteps.Length - 1)
		{
			//Debug.Log ("Launching step : " + stepIndex);
			cinematicSteps[stepIndex].CinematicStepDone += OnCinematicStepDone;
			cinematicSteps[stepIndex].Launch ();
		}
		else
		{
			OnCinematicDone ();
		}
	}

	// Event sent by the actions themselves
	public void OnCinematicStepDone (object obj, EventArgs e)
	{
		//Debug.Log ("Cinematic: OnCinematicStepDone");
		cinematicSteps[stepIndex].CinematicStepDone -= OnCinematicStepDone;
		Next ();
	}

	public void OnCinematicDone ()
	{
		Debug.Log ("Cinematic: OnCinematicDone");
		if (CinematicDone != null)
		{
			CinematicDone (this, EventArgs.Empty);
		}
	}

	public uint ID
	{
		get{return id;}
	}
}
