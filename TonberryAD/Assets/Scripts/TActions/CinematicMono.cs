using UnityEngine;
using System.Collections;

[System.Serializable]
public class CinematicMono : MonoBehaviour
{
	public string label;
	public CinematicStepMono[] sequence;

	public TAction[][] CreateCinematicSequence ()
	{
		TAction[][] cinematic = new TAction[sequence.Length][];
		for (int i = 0; i < sequence.Length; i++)
		{
			TAction[] steps = new TAction[sequence[i].step.Length];
			for (int j = 0; j < steps.Length; j++)
			{
				steps[j] = sequence[i].step[j].CreateTAction ();
			}
			cinematic[i] = steps;
		}
		return cinematic;
	}

	[System.Serializable]
	public class CinematicStepMono
	{
		public TActionMono[] step;
	}
}
