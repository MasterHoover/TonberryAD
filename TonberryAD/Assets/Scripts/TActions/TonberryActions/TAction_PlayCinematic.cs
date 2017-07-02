using UnityEngine;
using System.Collections;

public class TAction_PlayCinematic : TAction 
{
	private string label;
	private bool cinematicDone;

	public TAction_PlayCinematic (string label)
	{
		this.label = label;
	}
		
	protected override void Initialize ()
	{
		LabeledCinematic lc = _GameManager.Instance.CinematicPlayer.PlayCinematic (label);
		lc.Cinematic.CinematicDone += OnCinematicDone;
	}

	public void OnCinematicDone (object source, System.EventArgs e)
	{
		cinematicDone = true;
	}

	protected override bool Done 
	{
		get 
		{
			return cinematicDone;
		}
	}
}
