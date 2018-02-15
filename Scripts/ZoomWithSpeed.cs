using UnityEngine;
using System.Collections;

public class ZoomWithSpeed : MonoBehaviour {

	public bool cameraOrthographic = true;
	public Rigidbody targetRigidbody;

	public float size_min = 10f;
	public float size_max = 20f;
	public float transitionSpeed = 1f;

	float size;
	float targetSpeed;

	public Camera cam;

	// Use this for initialization
	void Start ()
	{
		size = cam.orthographicSize;
		cameraOrthographic = cam.orthographic;
		targetRigidbody = GameObject.FindGameObjectWithTag("DeliveryTruck").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (targetRigidbody == null)
			return;

		targetSpeed = targetRigidbody.velocity.magnitude;

		float targetSize = targetSpeed;

		if (targetSize < size_min)
		{
			targetSize = size_min;
		}
		else if (targetSize > size_max)
		{
			targetSize = size_max;
		}

		//Debug.Log("targetsize = " + targetSize + "\nsize = " + size);

		size = Mathf.Lerp(size, targetSize, transitionSpeed * Time.deltaTime);
		cam.orthographicSize = size;
		GetComponentInChildren<Camera>().orthographicSize = size;
	}
}
