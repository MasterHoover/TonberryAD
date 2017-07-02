using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CinematicPlayer : MonoBehaviour 
{
	private List<LabeledCinematic> ongoing = new List<LabeledCinematic> ();
	private CinematicMono[] sceneCinematics;

	public void TestPlay ()
	{
		TAction[] sequence = 
			new TAction[]
			{
				new TAction_LogMessage ("Starting"),
				new TAction_Delay (2f),
				new TAction_LogMessage ("3..."),
				new TAction_Delay (1f),
				new TAction_LogMessage ("2..."),
				new TAction_Delay (1f),
				new TAction_LogMessage ("1..."),
				new TAction_Delay (1f),
				new TAction_LogMessage ("LOADING..."),
				new TAction_Delay (3f),
				new TAction_LogMessage ("SUCCESS")
			};

		TAction[][] cinematic = new TAction[sequence.Length][];
		for (int i = 0; i < sequence.Length; i++)
		{
			cinematic[i] = new TAction[1];
			cinematic[i][0] = sequence[i];
		}

		PlayCinematic ("TestCinematic", cinematic);
	}

	public LabeledCinematic PlayCinematic (string label)
	{
		int index = GetSceneCinematicIndex (label);
		if (index > -1)
		{
			return PlayCinematic (label, sceneCinematics[index].CreateCinematicSequence ());
		}
		else
		{
			Debug.LogWarning ("CinematicPlayer/PlayCinematic (string) : Didn't find sceneCinematic with label [" + label + "].");
		}
		return null;
	}

	public int GetSceneCinematicIndex (string label)
	{ 
		int index = -1;
		if (sceneCinematics != null)
		{
			for (int i = 0; i < sceneCinematics.Length; i++)
			{
				if (sceneCinematics[i].label.Equals (label))
				{
					return i;
				}
			}
		}
		return index;
	}

	public LabeledCinematic PlayCinematic (string label, TAction[][] sequence)
	{
		LabeledCinematic lc = new LabeledCinematic (label, new Cinematic (sequence));
		ongoing.Add (lc);
		lc.Cinematic.CinematicDone += OnCinematicDone;
		lc.Cinematic.Play ();
		return lc;
	}

	public LabeledCinematic PlayCinematic (string label, Cinematic c)
	{
		LabeledCinematic lc = new LabeledCinematic (label, c);
		ongoing.Add (lc);
		lc.Cinematic.CinematicDone += OnCinematicDone;
		lc.Cinematic.Play ();
		return lc;
	}

	public LabeledCinematic PlayCinematic (string label, CinematicMono cinematicMono)
	{
		Cinematic c = new Cinematic (cinematicMono.CreateCinematicSequence ());
		LabeledCinematic lc = new LabeledCinematic (label, c);
		ongoing.Add (lc);
		lc.Cinematic.CinematicDone += OnCinematicDone;
		lc.Cinematic.Play ();
		return lc;
	}

	public LabeledCinematic PlayCinematic (TAction action)
	{
		LabeledCinematic lc = new LabeledCinematic ("Unnammed", new Cinematic (new TAction[1][]{new TAction[1]{action}}));
		ongoing.Add (lc);
		lc.Cinematic.CinematicDone += OnCinematicDone;
		lc.Cinematic.Play ();
		return lc;
	}

	private void OnCinematicDone (object source, EventArgs e)
	{
		Debug.Log ("Cinematic \"" + GetCinematicLabel ((Cinematic) source) + "[" + ((Cinematic) source).ID + "]\" is done");
		ongoing.Remove (FindLabeledCinematic ((Cinematic) source));
	}

	private LabeledCinematic FindLabeledCinematic (Cinematic c)
	{
		for (int i = 0; i < ongoing.Count; i++)
		{
			if (ongoing[i].Cinematic == c)
			{
				return ongoing[i];
			}
		}
		return null;
	}

	public LabeledCinematic FindLabeledCinematic (string label)
	{
		for (int i = 0; i < ongoing.Count; i++)
		{
			if (ongoing[i].Label.Equals (label))
			{
				return ongoing[i];
			}
		}
		return null;
	}

	public string GetCinematicLabel (Cinematic c)
	{
		for (int i = 0; i < ongoing.Count; i++)
		{
			if (ongoing[i].Cinematic == c)
			{
				return ongoing[i].Label;
			}
		}
		return "UNKNOWN";
	}

	public void StopCinematic (string label)
	{
		bool done = false;
		for (int i = 0; !done && i < ongoing.Count; i++)
		{
			if (ongoing[i].Label.Equals (label))
			{
				ongoing[i].Cinematic.Stop ();
				done = true;
			}
		}
	}

	void OnSceneLoaded (object source, System.EventArgs e)
	{
		FetchSceneCinematics ();
	}

	public void FetchSceneCinematics ()
	{
		sceneCinematics = GameObject.FindObjectsOfType<CinematicMono> ();
	}
}
