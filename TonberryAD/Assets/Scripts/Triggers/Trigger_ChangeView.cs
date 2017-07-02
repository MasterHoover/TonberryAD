using UnityEngine;
using System.Collections;

public class Trigger_ChangeView : EventTrigger 
{
	public View front;
	public View back;

	public enum CamType
	{
		Fix,
		Travelling
	}

	void Start ()
	{
		if (front == null || back == null)
		{
			Debug.LogWarning ("Trigger_ChangeView[" + name + "]/Start ()/Some camera views hasn't been assigned.");
			Destroy (this);
		}
	}
		
	protected override void LaunchEnterEvent (Collider col)
	{
		if (Vector3.Angle (transform.forward, col.transform.forward) >= 90f) // Front
		{
			front.Activate ();
		}
		else
		{
			back.Activate ();
		}
	}


	protected override void LaunchExitEvent (Collider col)
	{
		if (Vector3.Angle (transform.forward, col.transform.forward) <= 90f) // Back
		{
			back.Activate ();
		}
		else
		{
			front.Activate ();
		}
	}
}
