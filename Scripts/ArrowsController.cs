using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArrowsController : MonoBehaviour {

	public GameObject goalArrowPrefab;
	List<GameObject> arrows;
	bool initialised;

	void Start()
	{
		initialised = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!initialised)
		{
			arrows = new List<GameObject>();
			foreach (PizzaDelivery pd in PizzaGameManager.pizzaDeliveries)
			{
				GameObject child = Instantiate(goalArrowPrefab, transform.position, transform.rotation) as GameObject;
				arrows.Add(child);
				child.transform.parent = transform;
				child.GetComponent<TurnToTarget>().target = pd;
			}
			initialised = true;
		}


		foreach (GameObject arrow in arrows)
		{
			if (arrow.GetComponent<TurnToTarget>().target.IsComplete())
			{
				arrow.SetActive(false);
				arrows.Remove(arrow);
			}
		}
	}
}
