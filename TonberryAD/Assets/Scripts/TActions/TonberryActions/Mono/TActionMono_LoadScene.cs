using UnityEngine;
using System.Collections;

public class TActionMono_LoadScene : TActionMono
{
	public string sceneName;
	public int spawnIndex;
	public bool fade = true;

	public override TAction CreateTAction ()
	{
		return new TAction_LoadScene (sceneName, spawnIndex, fade);
	}
}
