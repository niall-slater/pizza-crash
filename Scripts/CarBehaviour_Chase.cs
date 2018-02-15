using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;

public class CarBehaviour_Chase : MonoBehaviour {

	private CarAIControl carAI;
	private CarController carController;
	public GameObject chaseTarget;
	public GameObject emptyTransformPrefab;

	private NavMeshPath path;
	private bool needPath;
	private float pointReachedDistance = 3f;

	private float findPathTimer = 0.4f;

	private int pointCounter;

	//de-stucking stuff

	private float timeToWait = .5f;
	private float stallTimer = 0f;
	private float reverseTimer = 0f;
	private bool stuck;

	// Use this for initialization
	void Start ()
	{
		carAI = GetComponent<CarAIControl>();
		carController = GetComponent<CarController>();
		if (chaseTarget == null)
			chaseTarget = GameObject.FindGameObjectWithTag("DeliveryTruck");

		emptyTransformPrefab = (GameObject) Instantiate(emptyTransformPrefab, chaseTarget.transform.position, chaseTarget.transform.rotation);
		path = new NavMeshPath();

		needPath = true;
		pointCounter = 0;
		stallTimer = timeToWait;
	}

	// Update is called once per frame
	void Update ()
	{
		CheckIfStuck();


		if (findPathTimer > 0f)
			findPathTimer -= Time.deltaTime;

		if (findPathTimer <= 0f)
		{
			needPath = true;
			findPathTimer = 0.4f;
		}

		if (needPath)
			FindPath();

		for (int i = 0; i < path.corners.Length-1; i++)
			Debug.DrawLine(path.corners[i], path.corners[i+1], Color.red);

		if (Vector3.Distance(transform.position, emptyTransformPrefab.transform.position) <= pointReachedDistance)
		{
			if (pointCounter < path.corners.Length)
				NextPoint();
			else
				needPath = true;
		}
	}

	private void FindPath()
	{
		//path.ClearCorners();
		//I think areamask 1 is 'walkable' in the navmesh bake menu, so i've set the parameter to 1.
		//print("FINDING PATH");
		NavMesh.CalculatePath(gameObject.transform.position, chaseTarget.transform.position, 1, path);		

		pointCounter = 0;
		emptyTransformPrefab.transform.position = path.corners[pointCounter];
		carAI.SetTarget(emptyTransformPrefab.transform);

		needPath = false;
	}

	private void NextPoint()
	{
		//print("counter on " + pointCounter);
		pointCounter++;
		emptyTransformPrefab.transform.position = path.corners[pointCounter];
	}

	private void CheckIfStuck()
	{

		if (!stuck)
		{
			if (GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
			{
				stallTimer -= Time.deltaTime;
			}
			else
			{
				stallTimer = timeToWait;
			}
		}

		if (stallTimer <= 0f && !stuck)
		{
			stallTimer = timeToWait; //reset stall timer
			stuck = true;
			carAI.reversing = true;
			reverseTimer = 3f;
		}

		if (stuck)
		{
			Vector3 reversingOffset = -transform.forward;
			float mult = 7f;
			reversingOffset.Scale(new Vector3(mult,mult,mult));

			WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>();

			foreach (WheelCollider w in wheels)
			{
				carAI.m_Driving = false;
				w.motorTorque = -100f;
			}

			//needPath = false;

			reverseTimer -= Time.deltaTime;

			if (reverseTimer <= 0f)
			{
				stuck = false;
				carAI.m_Driving = true;
				carAI.reversing = false;
			}
		}
	}
}
