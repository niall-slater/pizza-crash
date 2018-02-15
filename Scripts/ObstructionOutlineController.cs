using UnityEngine;
using System.Collections;

public class ObstructionOutlineController : MonoBehaviour {

	public Camera GlowCamera;
	bool active = false;
	public float sizeToPassDown = 10f;
		
	void Update()
	{
		GlowCamera.gameObject.SetActive(active);

		if (active)
		{
			sizeToPassDown = GetComponent<Camera>().orthographicSize;
			GlowCamera.orthographicSize = sizeToPassDown;
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
		{
			other.gameObject.GetComponent<TurnTransparent>().GoTransparent();
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
		{
			other.gameObject.GetComponent<TurnTransparent>().Reappear();
		}
	}

	void FixedUpdate ()
	{
		/*
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(transform.position, fwd, out hit, 100f))
		{
			 
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Buildings"))
			{
				//active = true; //this line makes the car appear with a glowing outline

				obstructions.Add(hit.collider.gameObject);
				return;
			}
		}

		active = false;
		*/
	}
}
