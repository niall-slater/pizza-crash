using UnityEngine;
using UnityStandardAssets.Utility;
using System.Collections;

public class InitPizzaGameCamera : MonoBehaviour {

	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag("DeliveryTruck");
		GetComponent<FollowTarget>().target = player.transform;
		GetComponent<ZoomWithSpeed>().targetRigidbody = player.GetComponent<Rigidbody>();
	}
}
	