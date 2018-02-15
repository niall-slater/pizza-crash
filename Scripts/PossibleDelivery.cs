using UnityEngine;
using System.Collections;

public class PossibleDelivery : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 GetLaunchPosition()
	{
		Debug.Log(GetComponentInChildren<Transform>().position);
		return GetComponentInChildren<Transform>().position;
	}
}
