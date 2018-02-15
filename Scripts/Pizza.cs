using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour {


	bool beingLaunched = false;
	Vector3 launchTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Launch(Vector3 launchVector, Vector3 target)
	{
		GetComponent<Collider>().isTrigger = true;
		GetComponent<Rigidbody>().AddForce(launchVector, ForceMode.Impulse);
		this.launchTarget = target;
		beingLaunched = true;
	}

	void FixedUpdate()
	{
		if (beingLaunched && Vector3.Distance(transform.position, launchTarget) < 5f)
		{
			Destroy(this.gameObject);
		}
	}
}
