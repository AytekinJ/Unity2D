using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset = new Vector3(0, 0, -10);
	public float smooth = 0.125f;

	Vector3 velocity = Vector3.zero;
	void FixedUpdate() //If player movement code runs in Update, this method should also run in Uptade
	{
		Vector3 movePosition = target.position + offset;
		transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, smooth);
	}
}
