using UnityEngine;
using System.Collections;

public class Trigger_StartCinematic : MonoBehaviour 
{
	public string label;

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player")
		{
			_GameManager.Instance.CinematicPlayer.PlayCinematic (label);
		}
	}
}
