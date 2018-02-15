using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	public float RotationSpeed = 1f;

	public Transform CameraTransform;

	Vector3 _direction;
	Quaternion _lookRotation;

	void Start()
	{
		CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void Update ()
	{
		Vector3 camPos;

		if (CameraTransform == null)
		{
			return;
		}

		camPos = CameraTransform.position;

		//find the vector pointing from our position to the target
		_direction = (camPos - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		//transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		transform.rotation = _lookRotation * Quaternion.Euler(0f, 180f, 0f);

	}
}
