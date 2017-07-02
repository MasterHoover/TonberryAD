using UnityEngine;
using System.Collections;

public class Trigger_PlayCinematic : EventTrigger 
{
	public CinematicMono cinematic;

	protected override void LaunchEnterEvent (Collider col)
	{
		_GameManager.Instance.CinematicPlayer.PlayCinematic (cinematic.label, cinematic);
	}
}
