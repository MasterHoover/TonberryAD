using UnityEngine;
using System.Collections;
using System;

public class CinematicStep 
{
	private TAction[] step;
	private bool[] dones;

	public delegate void OnCinematicStepDoneHandler (object source, EventArgs e);
	public event OnCinematicStepDoneHandler CinematicStepDone;

	public CinematicStep (TAction[] step)
	{
		this.step = step;
		dones = new bool[step.Length];
	}

	public void Launch ()
	{
		for (int i = 0; i < step.Length; i++)
		{
			step[i].ActionDone += OnActionDone;
			step[i].Launch ();
		}
	}

	public void Stop ()
	{
		Debug.Log ("CinematicStep calls his stop");
		for (int i = 0; i < step.Length; i++)
		{
			step[i].End ();
		}
	}

	private bool AllDone ()
	{
		for (int i = 0; i < dones.Length; i++)
		{
			if (!dones[i])
			{
				return false;
			}
		}
		return true;
	}

	private void CheckOneDone ()
	{
		bool good = false;
		for (int i = 0; !good && i < dones.Length; i++)
		{
			if (!dones[i])
			{
				dones[i] = true;
				good = true;
			}
		}
	}

	public TAction[] Step
	{
		get{return step;}
	}

	private void OnActionDone (object source, EventArgs e)
	{
		//Debug.Log ("CinematicStep: OnActionDone");
		CheckOneDone ();
		if (AllDone ())
		{
			//Debug.Log ("All done");
			OnCinematicStepDone ();
		}
		else
		{
			//Debug.Log ("Still remaining");
		}
	}

	protected void OnCinematicStepDone ()
	{
		//Debug.Log ("CinematicStep: CinematicStepDone");
		if (CinematicStepDone != null)
		{
			CinematicStepDone (this, EventArgs.Empty);
		}
	}
}
