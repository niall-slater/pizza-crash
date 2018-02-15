using UnityEngine;
using System.Collections;

public class SlideIn : MonoBehaviour {

	bool activated = false;
	Vector3 startPos;
	public Vector2 goalPos = new Vector2();
	public float speed = 5f;

	// Use this for initialization
	void Start ()
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activated)
		{
			transform.position = Vector2.Lerp(transform.position, goalPos, Time.deltaTime * speed);
		}
	}

	public void Activate()
	{
		activated = true;
	}
}
