using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Sequence : Action 
{
	public Action[] sequence;
	private Action[] clones;
	private CutscenePlayer cp;
	private bool done;
	private int index;

	protected override void Init ()
	{
		index = 0;
		clones = new Action[sequence.Length];
		for (int i = 0; i < sequence.Length; i++)
		{
			clones [i] = Instantiate<Action> (sequence [i]);
			clones [i].transform.parent = transform;
		}
		PlayStep ();
	}

	protected override bool Runtime ()
	{
		return done;
	}

	private void PlayStep ()
	{
		clones [index].ActionCompleted += OnAction2Done;
		clones [index].enabled = true;
	}

	private void OnAction2Done (Action a)
	{
		a.ActionCompleted -= OnAction2Done;
		Destroy (a.gameObject);
		if (++index < sequence.Length)
		{
			PlayStep ();
		}
		else
		{
			done = true;
		}
	}
}
