using UnityEngine;
using System.Collections;

public class ResetOnButtonPress : MonoBehaviour {

	public KeyCode resetKey;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(resetKey))
		{
			gameObject.transform.position = new Vector3(0f, 1f, 0f);
			gameObject.transform.rotation.Set(0f,0f,0f,0f);
		}

	}
}
