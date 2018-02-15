using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class FireHydrant : MonoBehaviour {

	public GameObject particleSystem;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<CarController>() == null)
		{
			return;
		}
		particleSystem.SetActive(true);
		GetComponent<AudioSource>().Play();
	}
}
