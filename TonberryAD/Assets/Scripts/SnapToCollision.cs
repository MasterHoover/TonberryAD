/* 
Class: SnapToCollision
Author: Olivier Reid

Desc: 
Automatically snaps an object to a near collision, depending on maxDistance.
Useful to keep an object grounded at all time.
*/
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class SnapToCollision : MonoBehaviour 
{
	public Vector3 snapDirection = Vector3.down;
	public float maxDistance = 5f;
	private CharacterController charController;

	void Awake ()
	{
		if (maxDistance <= 0f)
		{
			maxDistance = 0f;
			Debug.LogWarning (name + "[SnapToGround]/Awake () : maxSnappingDistance is 0f which makes the script obsolete. Disabling script.");
		}
		charController = GetComponent<CharacterController> ();
	}

	void Update ()
	{
		if (DetectingCollision ())
		{
			Snap ();
		}
	}

	private bool DetectingCollision ()
	{
		return CollisionDetector.DetectingCollision (transform.position, snapDirection, maxDistance);
	}

	private void Snap ()
	{
		charController.Move (snapDirection * maxDistance);
	}

	void OnDrawGizmos ()
	{
		if (maxDistance > 0f)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (transform.position, transform.position + snapDirection * maxDistance);
		}
	}
}
