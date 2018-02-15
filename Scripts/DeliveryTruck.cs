using UnityEngine;
using System.Collections.Generic;

public class DeliveryTruck : MonoBehaviour {

	public Transform pizzaSpawnPoint;
	int maxPizzas = 5;

	float CrashSoundTimer = 0.4f;

	////UPGRADE STUFF
		public int upgrade_extraPizzas = 0;
		public float upgrade_boostRechargeMultiplier = 1f;
		public float upgrade_pizzaMagnetForce = 50f;
		public float upgrade_repulseForceMultiplier = 1f;

		public float magnetRange = 5f;
		public float magnetAttraction = 5f; //deprecated??
		public float pizzaHoldForce = 15f;
		public float repulseForce = 10000f;
		public float repulseRange = 10f;
		public float repulseCooldownTimer;
		public float repulseCooldownPeriod = 5f;

	public Transform magnetTarget;
	public Transform holdTarget;

	private Pizza[] pizzas;

	// Use this for initialization
	void Start ()
	{
		CrashSoundTimer = 1f;
		pizzas = GameObject.FindObjectsOfType<Pizza>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		//TIMERS
		if (CrashSoundTimer > 0f)
			CrashSoundTimer -= Time.deltaTime;
		else
			CrashSoundTimer = 0f;


		if (repulseCooldownTimer > 0f)
			repulseCooldownTimer -= Time.deltaTime;
		else
			repulseCooldownTimer = 0f;
	}

	//pizza list refresh - timer to stop it updating too frequently and causing lag
	float pizzaListRefreshTimerMax = 0.25f;
	float pizzaListRefreshTimer = 0f;

	void FixedUpdate ()
	{

		//try to hold onto the pizzas a bit
		//add a very slight pull towards the centre of the pizza bay

		if (pizzaListRefreshTimer > 0f)
		{
			pizzaListRefreshTimer -= Time.deltaTime;
		}
		else
		{
			pizzaListRefreshTimer = pizzaListRefreshTimerMax;

			pizzas = GameObject.FindObjectsOfType<Pizza>();
		}

		foreach (Pizza p in pizzas)
		{
			if (GetComponentInChildren<PizzaDetector>().GetPizzas().Contains(p))
			{
				Vector3 pullForce = holdTarget.position - p.transform.position;
				pullForce.Normalize();
				pullForce.Scale(new Vector3(pizzaHoldForce, pizzaHoldForce, pizzaHoldForce));
				p.GetComponent<Rigidbody>().AddForce(pullForce);
			}

		}


		//INPUT STUFF (excludes boost on B press, maybe that could go here instead of in its own script?
		if (Input.GetKeyDown(KeyCode.R))
		{
			Repulse();
		}
	}

	public int HowManyPizzas()
	{
		return GetComponentInChildren<PizzaDetector>().HowManyPizzas();
	}

	public List<Pizza> GetPizzas()
	{
		return GetComponentInChildren<PizzaDetector>().GetPizzas();
	}

	public float GetBoostValue()
	{
		BoostOnButtonPress boost = GetComponent<BoostOnButtonPress>();
		boost.boostRechargeMultiplier = upgrade_boostRechargeMultiplier;
		float result = 1 - boost.boostCooldownTimer / boost.boostCooldownPeriod;
		return result;
	}

	public float GetRepulseValue()
	{
		float result = 1 - repulseCooldownTimer / repulseCooldownPeriod;
		return result;
	}


	public void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.CompareTag("Pizza"))
			return;
		if (CrashSoundTimer <= 0)
		{
			CrashSoundTimer = 0.4f;
			GetComponent<AudioSource>().pitch = Random.Range(0.6f, 1.4f);
			GetComponent<AudioSource>().Play();
		}
	}

	public void Repulse()
	{
		//find closest enemy
		if (repulseCooldownTimer > 0f)
			return;
		
		GameObject target = null;
		float dist = 9999f;
		foreach (GameObject enemy in PizzaGameManager.enemies)
		{
			float checkdist = Vector3.Distance(transform.position, enemy.transform.position);

			if (checkdist < dist)
			{
				dist = checkdist;
				target = enemy;
			}
		}
		if (target == null)
			return;

		target.GetComponent<Rigidbody>().AddExplosionForce(repulseForce * upgrade_repulseForceMultiplier, transform.position, repulseRange, 1f, ForceMode.Impulse);
		Debug.DrawLine(transform.position, target.transform.position, Color.blue, 1f);
		repulseCooldownTimer = repulseCooldownPeriod;
	}

	public int getMaxPizzas()
	{
		return (maxPizzas + upgrade_extraPizzas);
	}

	public void AddTime(float seconds)
	{
		PizzaGameManager.AddTime(seconds);
	}
}
