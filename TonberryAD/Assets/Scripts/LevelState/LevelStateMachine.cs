using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelStateMachine : MonoBehaviour 
{
	private LevelState[] sceneLevelStates;
	private List<LevelStateInfo> activeLevelStates = new List<LevelStateInfo> ();

	public void ActivateAutomaticStates ()
	{
		AutomaticLevelState[] auto = GameObject.FindObjectsOfType<AutomaticLevelState> ();
		for (int i = 0; i < auto.Length; i++)
		{
			ActivateLevelState (_GameManager.Instance.LevelName, auto[i].label);
		}
	}

	public void FetchAllAndPlayActiveStates ()
	{
		FetchSceneLevelState ();
		string levelName = _GameManager.Instance.LevelName;
		LaunchAllStateEvents (levelName);
	}

	public void FetchSceneLevelState ()
	{
		sceneLevelStates = GameObject.FindObjectsOfType<LevelState> ();
	}

	public void ActivateLevelState (string levelName, string label)
	{
		int index = IndexOfLevelName (levelName);
		if (index == -1)
		{
			activeLevelStates.Add (new LevelStateInfo (levelName));
			index = IndexOfLevelName (levelName);
		}
		activeLevelStates[index].AddState (label);
	}

	public void DeactivateLevelState (string levelName, string label)
	{
		int index = IndexOfLevelName (levelName);
		if (index > -1)
		{
			activeLevelStates[index].RemoveState (label);
		}
		else
		{
			Debug.LogWarning ("LevelStateMachine[" + name + "]/DeactivateLevelState (string, string) : no active states belongs to level [" + levelName + "].");
		}
	}

	public void LaunchAllStateEvents (string levelName)
	{
		int index = IndexOfLevelName (levelName);
		if (index > -1)
		{
			Debug.Log ("Launch level states : " + levelName);
			string[] infos = activeLevelStates[index].States;
			for (int i = 0; i < infos.Length; i++)
			{
				string state = infos[i];
				bool foundIt = false;
				for (int j = 0; !foundIt && j < sceneLevelStates.Length; j++)
				{
					if (state.Equals (sceneLevelStates[j].label))
					{
						foundIt = true;
						Debug.Log ("Playing [" + sceneLevelStates[j].label + "] State.");
						_GameManager.Instance.CinematicPlayer.PlayCinematic (sceneLevelStates[j].cinematicLabel);

					}
				}
			}
		}
		else
		{
			Debug.LogWarning ("Index is not found");
		}
	}

	private int IndexOfLevelName (string levelName)
	{
		for (int i = 0; i < activeLevelStates.Count; i++)
		{
			if (activeLevelStates[i].LevelName.Equals (levelName))
			{
				return i;
			}
		}
		return -1;
	}

	[System.Serializable]
	public class LevelStateInfo
	{
		private string levelName;
		private List<string> states = new List<string> ();

		public LevelStateInfo (string levelName)
		{
			this.levelName = levelName;
		}

		public void AddState (string label)
		{
			if (states.IndexOf (label) == -1)
			{
				states.Add (label);
			}
			else
			{
				Debug.LogWarning ("LevelStateInfos with levelName [" + levelName + "]/AddState (string) : state with label [" + label + "] already exist.");
			}
		}

		public void RemoveState (string label)
		{
			int index = states.IndexOf (label);
			if (index > -1)
			{
				states.RemoveAt (index);
			}
			else
			{
				Debug.LogWarning ("LevelStateInfos with levelName [" + levelName + "]/RemoveState (string) : state with label [" + label + "] doesn't exist.");
			}
		}

		public string LevelName
		{
			get{return levelName;}
		}

		public string[] States
		{
			get{return states.ToArray ();}
		}
	}
}
