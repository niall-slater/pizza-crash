using UnityEngine;
using System.Collections;

public class Camera_SeeThroughObstacles : MonoBehaviour {


	GameObject transparentTarget;

	public Color transColor = new Color(1f,1f,1f, 0.2f);

	// Update is called once per frame
	void FixedUpdate ()
	{

		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		RaycastHit hit;

		if (Physics.Raycast(transform.position, fwd, out hit, 70f))
		{
			if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Buildings"))
				return;
			transparentTarget = hit.collider.gameObject;
			Debug.Log("turning " + transparentTarget.name + " transparent");
			Renderer rend = transparentTarget.GetComponent<Renderer>();
			rend.material.shader = Shader.Find("Transparent Diffuse");
			rend.material.SetColor("_Color", transColor);
		}
		else
		{
			if (transparentTarget != null)
			{
				Debug.Log("turning " + transparentTarget.name + " visible");
				Renderer rend = transparentTarget.GetComponent<Renderer>();
				rend.material.shader = Shader.Find("Standard");
				rend.material.SetColor("_Color", Color.white);
			}
			transparentTarget = null;
		}
	}
}
