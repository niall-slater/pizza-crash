using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PizzaDelivery : MonoBehaviour {

	public float delayBeforeReceive = 1f;
	public float receiveInterval = 1f;
	public int pizzasOrdered = 4;
	public float pizzaThrowForce = 50f;
	int pizzasTaken = 0;

	float launchTimer = 0f;
	public float speedTolerance = 1f;
	bool receivingPizzas = false;
	bool complete = false;

	public int tipValue = 5;

	public Transform launchTarget;

	DeliveryTruck truck;
	public MeshRenderer ringRenderer;

	void OnTriggerStay(Collider other)
	{
		//if there's a truck in the bay, start taking pizzas!
		if (other.gameObject.CompareTag("DeliveryTruck"))
		{
			truck = other.GetComponent<DeliveryTruck>();

			//if the truck has no pizzas, ignore it
			if (truck.HowManyPizzas() == 0)
				return;

			if (receivingPizzas == false)
			{
				InitiatePizzaReception(truck);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		//truck's gone, forget about it
		if (other.gameObject.CompareTag("DeliveryTruck"))
		{
			truck = null;
			receivingPizzas = false;
		}
	}



	void InitiatePizzaReception(DeliveryTruck receiver)
	{
		//make sure the truck is going slow enough to deliver pizzas
		if (receiver.GetComponent<Rigidbody>().velocity.magnitude < speedTolerance)
		{
			//it is! wait a sec, then drop the 'zas
			receivingPizzas = true;
			launchTimer = delayBeforeReceive;
		}
	}

	void Update()
	{
		foreach (Text t in GetComponentsInChildren<Text>())
		{
			if (t.name.Equals("Pizzas Still Needed"))
			{
				if (!complete)
					t.text = "ORDER: " + (pizzasOrdered - pizzasTaken);
				else
					t.text = "COMPLETE!";
				
			}
		}

		if (pizzasTaken >= pizzasOrdered)
		{
			if (complete == false)
			{
				PizzaGameManager.AddCash(tipValue);
				truck.AddTime(20f);
				GetComponent<AudioSource>().Play();
			}
			complete = true;
		}

		if (complete)
		{
			//make the circle vanish
			ringRenderer.enabled = false;
		}
	}

	void FixedUpdate()
	{
		if (receivingPizzas)
		{
			//don't launch em all at once
			launchTimer -= Time.deltaTime;

			if (launchTimer < 0)
			{
				launchTimer = receiveInterval;
				if (truck.GetPizzas().Count > 0)
				{
					Pizza p = truck.GetPizzas()[truck.GetPizzas().Count-1];
					if (p != null)
					{
						Vector3 launchForce = new Vector3(pizzaThrowForce, pizzaThrowForce, pizzaThrowForce);
						Vector3 launchVector = launchTarget.position - p.transform.position;
						launchVector.Scale(launchForce);
						p.Launch(launchVector, launchTarget.position);
						pizzasTaken++;
					}
				}
			}

			if (pizzasTaken >= pizzasOrdered)
			{
				receivingPizzas = false;
				pizzasTaken = 0;
				pizzasOrdered = 0;
			}
		}
	}

	public bool IsComplete()
	{
		return complete;
	}
}
