using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnToTarget : MonoBehaviour {


	public float RotationSpeed = 1f;

	Vector3 _direction;
	Quaternion _lookRotation;

	float alpha = 0.3f;

	public PizzaDelivery target;

	void Update ()
	{
		Vector3 goalPos;

		goalPos = target.transform.position;

		//find the vector pointing from our position to the target
		_direction = (goalPos - transform.position).normalized;

		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//rotate us over time according to speed until we are in the required rotation
		//transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		transform.rotation = _lookRotation * Quaternion.Euler(90f, 0f, 0f);

		if (Vector3.Distance(transform.position, goalPos) < 6f)
		{
			alpha = Mathf.Lerp(alpha, 0f, 0.1f);
			GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
		}
		else
		{
			alpha = Mathf.Lerp(alpha, .3f, 0.1f);
			GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
		}
	}

	public float GetDistanceToTarget()
	{
		return Vector3.Distance(transform.position, target.transform.position);
	}
}
