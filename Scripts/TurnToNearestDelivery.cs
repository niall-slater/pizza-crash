using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnToNearestDelivery : MonoBehaviour {

	public float RotationSpeed = 1f;

	Vector3 _direction;
	Quaternion _lookRotation;

	float alpha = 1f;

	void Update ()
	{
		Vector3 goalPos;

		float distance = 9999f;
		Transform selection = null;

		if (PizzaGameManager.pizzaDeliveries == null)
		{
			return;
		}

		foreach (PizzaDelivery pd in PizzaGameManager.pizzaDeliveries)
		{
			Transform t = pd.transform;
			float checkdist = Vector3.Distance(transform.position, t.position);
			if (checkdist < distance)
			{
				distance = checkdist;
				selection = t;
			}
		}


		if (selection == null)
		{
			return;
		}

		goalPos = selection.position;

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
			alpha = Mathf.Lerp(alpha, 1f, 0.1f);
			GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
		}

		if (PizzaGameManager.pizzaDeliveries.Count == 0)
		{
			alpha = 0f;
		}
	}
}
