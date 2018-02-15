using UnityEngine;
using System.Collections;

public class ShowCenterOfMass : MonoBehaviour {
	
	public Rigidbody rb;
	Vector3 offset;
	public Transform parentTransform;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponentInParent<Rigidbody>();
		offset = rb.centerOfMass;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		offset = rb.centerOfMass;
		transform.position = parentTransform.position + offset;
	}
}
