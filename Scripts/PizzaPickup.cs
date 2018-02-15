using UnityEngine;
using System.Collections;

public class PizzaPickup : MonoBehaviour {

	public GameObject pizzaPrefab;
	public float delayBeforeResupply = 1f;
	public float spawnInterval = 0.3f;
	int pizzasToGiveThisTrip = 0;
	int pizzasGivenThisTrip = 0;

	float spawnTimer = 0f;
	public float speedTolerance = 1f;
	bool supplying = false;

	DeliveryTruck truck;

	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		//if there's a truck in the bay, start resupplying pizzas!
		if (other.gameObject.CompareTag("DeliveryTruck"))
		{
			truck = other.GetComponent<DeliveryTruck>();

			//if the truck's already got enough pizzas, ignore it
			if (truck.HowManyPizzas() >= truck.getMaxPizzas())
				return;
			
			if (supplying == false)
			{
				InitiatePizzaResupply(truck);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		//truck's gone, forget about it
		if (other.gameObject.CompareTag("DeliveryTruck"))
		{
			truck = null;
			supplying = false;
		}
	}

	void InitiatePizzaResupply(DeliveryTruck receiver)
	{
		//make sure the truck is going slow enough to receive pizzas
		if (receiver.GetComponent<Rigidbody>().velocity.magnitude < speedTolerance)
		{
			//it is! wait a sec, then drop the 'zas
			supplying = true;
			pizzasToGiveThisTrip = truck.getMaxPizzas() - truck.HowManyPizzas();
			spawnTimer = delayBeforeResupply;
		}
	}

	void Update()
	{
		if (supplying)
		{
			//don't spawn em all at once
			spawnTimer -= Time.deltaTime;
			if (spawnTimer < 0)
			{
				spawnTimer = spawnInterval;
				GameObject pizza = (GameObject) Instantiate(pizzaPrefab, truck.pizzaSpawnPoint.position, truck.transform.rotation);
				pizza.GetComponent<Rigidbody>().velocity = truck.GetComponent<Rigidbody>().velocity;
				pizzasGivenThisTrip++;
			}

			if (pizzasGivenThisTrip >= pizzasToGiveThisTrip)
			{
				supplying = false;
				pizzasGivenThisTrip = 0;
				pizzasToGiveThisTrip = 0;
			}
		}
	}
}

