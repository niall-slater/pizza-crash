using UnityEngine;
using System.Collections.Generic;

public class PizzaDetector : MonoBehaviour {

	List<Pizza> pizzaList;

	void Start()
	{
		pizzaList = new List<Pizza>();
	}

	void Update()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		Pizza p = other.GetComponent<Pizza>();
		
		if (p != null)
		{
			pizzaList.Add(p);
		}
	}

	void OnTriggerExit(Collider other)
	{
		Pizza p = other.GetComponent<Pizza>();

		if (p != null)
		{
			pizzaList.Remove(p);
		}
	}

	public int HowManyPizzas()
	{
		return pizzaList.Count;
	}

	public List<Pizza> GetPizzas()
	{
		return pizzaList;
	}
}
