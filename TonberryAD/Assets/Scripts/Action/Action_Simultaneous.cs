using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Simultaneous : Action 
{
	public Action[] actions;
	private Action[] clones;
	private int completedAmount;

	protected override void Init ()
	{
		clones = new Action[actions.Length];
		for (int i = 0; i < actions.Length; i++)
		{
			clones [i] = Instantiate<Action> (actions [i]);
			clones [i].transform.parent = transform;
		}

		foreach (Action a in clones)
		{
			a.ActionCompleted += Action2Completed;
			a.enabled = true;
		}
	}

	protected override bool Runtime ()
	{
		return completedAmount == actions.Length;
	}

	private void Action2Completed (Action action)
	{
		action.ActionCompleted -= Action2Completed;
		Destroy (action.gameObject);
		completedAmount++;
	}
}
