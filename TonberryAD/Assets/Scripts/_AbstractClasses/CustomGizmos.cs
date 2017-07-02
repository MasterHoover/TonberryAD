using UnityEngine;
using System.Collections;

public abstract class CustomGizmos : MonoBehaviour 
{
	public bool showGizmos = true;
	public bool selectedOnly;
	public bool destroyGizmosOnStart;

	void Start ()
	{
		if (destroyGizmosOnStart)
		{
			Destroy (this);
		}
	}

	void OnDrawGizmos ()
	{
		if(showGizmos && !selectedOnly)
		{
			DrawGizmos ();
		}
	}

	void OnDrawGizmosSelected ()
	{
		if(showGizmos && selectedOnly)
		{
			DrawGizmos ();
		}
	}

	protected abstract void DrawGizmos ();
}
