using UnityEngine;
using System.Collections;

[System.Serializable]
public class LabeledCinematic                                
{
	private string label;
	private Cinematic cinematic;

	public LabeledCinematic (string label, Cinematic c)
	{
		this.label = label;
		cinematic = c;
	}

	public string Label 
	{
		get{return label;}
	}

	public Cinematic Cinematic
	{
		get{return cinematic;}
	}
}
