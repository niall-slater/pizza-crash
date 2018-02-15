using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class CarBehaviour_ReverseFromWall : MonoBehaviour {

	private CarController carControl;

	private float timeToWait = 1f;
	private float stallTimer = 0f;
	private float reverseTimer = 0f;
	private bool stuck;

	// Use this for initialization
	void Start ()
	{
		carControl = GetComponent<CarController>();
		stuck = false;
		stallTimer = timeToWait;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!stuck)
		{
			if (GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
			{
				stallTimer -= Time.deltaTime;
			}
			else
			{
				stallTimer = timeToWait;
			}
		}

		if (stallTimer <= 0f && !stuck)
		{
			stallTimer = timeToWait; //reset stall timer
			stuck = true;
			reverseTimer = 3f;
		}

		if (stuck)
		{
			ReverseAndTurn();

			reverseTimer -= Time.deltaTime;

			if (reverseTimer <= 0f)
			{
				stuck = false;
			}
		}

	}

	private void ReverseAndTurn()
	{
		Vector3 reverseForce = -transform.forward;
		Vector3 multiplier = new Vector3(1800,1800,1800);
		reverseForce.Scale(multiplier);
		GetComponent<Rigidbody>().AddForce(reverseForce);

		/*
		print("REVERSING");
		print("stuck is " + stuck);
		carControl.Move(0f, -50f, 0f, 0f);
		*/

	}

}
