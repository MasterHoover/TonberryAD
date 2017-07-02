/* 
Class: MovementControl
Author: Olivier Reid

Desc: 
A script that holds functions to move an object in an horizontal plane.
*/
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class MovementInterface : MonoBehaviour
{
	private CharacterController charController;
	private Transform relativeOrientation;

	protected void Awake ()
	{
		charController = GetComponent <CharacterController> ();
	}

	public Vector2 Move (Vector2 movement)
	{
		Vector3 movement3D = movement.x * RightDirection + movement.y * ForwardDirection;
		movement3D.y = 0f;
		charController.Move (movement3D);
		return new Vector2 (movement3D.x, movement3D.z);
	}

	public Transform InputOrientation
	{
		set{relativeOrientation = value;}
	}

	private Vector3 ForwardDirection
	{
		get{return relativeOrientation != null ? relativeOrientation.forward : Vector3.forward;}
	}

	private Vector3 RightDirection
	{
		get{return relativeOrientation != null ? relativeOrientation.right : Vector3.right;}
	}
}
