using UnityEngine;
using System.Collections;

public class Streetlamp : MonoBehaviour {

	public Light pointlight;
	public Light spotlight;

	bool isTurnedOn;

	float timer;

	public void Start()
	{
		TurnOff();
	}

	public void Update()
	{
	}

	public void TurnOn()
	{
		pointlight.gameObject.SetActive(true);
		spotlight.gameObject.SetActive(true);
		isTurnedOn = true;
	}

	public void TurnOff()
	{
		pointlight.gameObject.SetActive(false);
		spotlight.gameObject.SetActive(false);
		isTurnedOn = false;
	}
}
