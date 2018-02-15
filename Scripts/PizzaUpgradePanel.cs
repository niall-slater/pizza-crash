using UnityEngine;
using System.Collections;

public class PizzaUpgradePanel : MonoBehaviour {

	public DeliveryTruck truck;

	public int boostUpgradeCost = 5;
	public int maxPizzaUpgradeCost = 10;
	public int magnetUpgradeCost = 20;

	bool visible;

	public void Start()
	{
		truck = GameObject.FindObjectOfType<DeliveryTruck>();
	}

	public void UpgradeBoostRechargeTime()
	{
		if (truck.upgrade_boostRechargeMultiplier < 4f)
		{
			if (PizzaGameManager.cash >= boostUpgradeCost)
			{
				truck.upgrade_boostRechargeMultiplier = 4f;
				PizzaGameManager.cash -= boostUpgradeCost;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public void UpgradeMaxPizzas()
	{
		if (truck.upgrade_extraPizzas < 5)
		{
			if (PizzaGameManager.cash >= maxPizzaUpgradeCost)
			{
				truck.upgrade_extraPizzas = 15;
				PizzaGameManager.cash -= maxPizzaUpgradeCost;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public void UpgradePizzaMagnet()
	{
		if (truck.pizzaHoldForce < 50f)
		{
			if (PizzaGameManager.cash >= magnetUpgradeCost)
			{
				truck.pizzaHoldForce = truck.upgrade_pizzaMagnetForce;
				PizzaGameManager.cash -= maxPizzaUpgradeCost;
				GetComponent<AudioSource>().Play();
			}
		}
	}

	public void ToggleMenu()
	{
		visible = !visible;

		if (visible)
		{
			Time.timeScale = 0.06f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}
}
