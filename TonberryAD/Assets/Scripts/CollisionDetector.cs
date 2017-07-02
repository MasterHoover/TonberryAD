/* 
Class: CollisionDetector
Author: Olivier Reid

Desc: 
CollisionDetector includes useful static functions about detecting collision.
*/
using UnityEngine;
using System.Collections;

public class CollisionDetector 
{
	public static bool DetectingCollision (Vector3 fromPos, Vector3 toPos)
	{
		return DetectingCollision (fromPos, toPos.normalized, toPos.magnitude);
	}

	public static bool DetectingCollision (Vector3 fromPos, Vector3 direction, float distance)
	{
		Ray ray = new Ray (fromPos, direction);
		return Physics.Raycast (ray, distance);
	}

	public static bool DetectingCollision (string[] affectedTags, Vector3 fromPos, Vector3 toPos)
	{
		return DetectingCollision (affectedTags, fromPos, toPos.normalized, toPos.magnitude);
	}

	public static bool DetectingCollision (string[] affectedTags, Vector3 fromPos, Vector3 direction, float distance)
	{
		if (affectedTags == null)
		{
			Debug.LogWarning ("CollisionDetecter/DetectingCollision (string[], Vector3, Vector3, float) : affectedTags is null.");
			return DetectingCollision (fromPos, direction, distance);
		}

		Ray ray = new Ray (fromPos, direction);
		RaycastHit hitInfo;
		if (!Physics.Raycast (ray, out hitInfo, distance))
		{
			return false;
		}

		for (int i = 0; i < affectedTags.Length; i++)
		{
			if (hitInfo.collider.tag == affectedTags[i])
			{
				return true;
			}
		}
		return false;
	}
}
