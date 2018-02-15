using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BoostOnButtonPress : MonoBehaviour {

	public float boostForce = 4000f;
	public float boostDuration = 1f;
	public float boostForceTimer = 0f;
	public float boostCooldownPeriod = 2f;
	public float boostCooldownTimer = 0f;

	public float boostRechargeMultiplier = 1f;

	public KeyCode boostKey;
		
	// Update is called once per frame
	void Update () 
	{
		//check if we're still cooling down. if so, tick down timer and return
		if (boostCooldownTimer > 0f)
		{
			boostCooldownTimer -= Time.deltaTime * boostRechargeMultiplier;
			if (boostCooldownTimer < 0f)
				boostCooldownTimer = 0f;
		}

		//we're ready to boost! check for input then set timer if positive
		if (Input.GetKeyDown(boostKey)) //replace this with crossplatforminput thing
		{
			if (boostCooldownTimer <= 0f)
				boostForceTimer = boostDuration;
		}

		//if boosting, tick down timer
		if (boostForceTimer > 0f)
		{
			boostForceTimer -= Time.deltaTime;
			//if boost has ended, set cooldown timer to go
			if (boostCooldownTimer <= 0f)
			{
				boostCooldownTimer = boostCooldownPeriod;
			}
		}

		if (boostForceTimer < 0f)
			boostForceTimer = 0f;
			
	}

	void FixedUpdate()
	{
		//check if we're boosting, then if so BOOOOOST
		if (boostForceTimer > 0f)
		{
			Vector3 boostVector = Vector3.forward * boostForce;
			GetComponent<Rigidbody>().AddRelativeForce(boostVector);
		}
	}
}
