using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using PizzaGameScripts;

[System.Serializable]
public class PizzaGameManager : MonoBehaviour {

	public Canvas mainCanvas;
	public DeliveryTruck playerTruck;
	public PizzaDelivery pizzaDeliveryPrefab;
	public GameObject enemyPrefab;
	public PizzaPickup pizzaPickup;
	public GameObject winScreen;
	public GameObject loseScreen;
	public GameObject leaderboard;

	public int deliveriesToSpawn = 5;
	int deliveriesRemaining;

	public static List<GameObject> possibleDeliveries;
	public static List<PizzaDelivery> pizzaDeliveries;
	public static List<GameObject> enemies;

	public float timer_max = 60f;
	static float gameTimer;
	public static int cash;
	public float enemySpawnInterval = 15f;
	private float enemySpawnCounter;

	bool lost = false;
	bool won = false;

	//STATS STUFF
	public static List<GameRecord> gameRecords = new List<GameRecord>();

	static float timeElapsedThisGame;


	void Start()
	{
		playerTruck = GameObject.FindObjectOfType<DeliveryTruck>();
		lost = false;
		gameTimer = timer_max;
		deliveriesRemaining = 0;
		timeElapsedThisGame = 0f;
		LoadLeaderboard();

		//foreach (GameRecord g in gameRecords)
			//print(g.GetDescription());

		possibleDeliveries = new List<GameObject>();
		pizzaDeliveries = new List<PizzaDelivery>();
		enemies = new List<GameObject>();

		//get each possible delivery and add it to a list, and add a number of deliveries at random using the possibilities

		foreach (GameObject poss in GameObject.FindGameObjectsWithTag("PossibleDelivery"))
		{
			possibleDeliveries.Add(poss);
		}

		for (int i = 0; i < deliveriesToSpawn; i++)
		{
			GameObject poss = possibleDeliveries[Random.Range(0, possibleDeliveries.Count-1)];
			possibleDeliveries.Remove(poss);
			PizzaDelivery newPD = Instantiate(pizzaDeliveryPrefab, poss.transform.position, poss.transform.rotation) as PizzaDelivery;
			//newPD.launchTarget.position = poss.GetComponent<PossibleDelivery>().GetLaunchPosition();
			newPD.pizzasOrdered = Random.Range(1, 5);
			pizzaDeliveries.Add(newPD);
			deliveriesRemaining++;
		}

		cash = 0;
		enemySpawnCounter = enemySpawnInterval;
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameTimer > 0 && !won)
		{
			gameTimer -= Time.deltaTime;
			timeElapsedThisGame += Time.deltaTime;
		}
		else
		{
			lost = true;
			Lose();
		}

		if (enemySpawnCounter > 0f)
		{
			enemySpawnCounter -= Time.deltaTime;
			if (enemySpawnCounter <= 0f)
			{
				enemySpawnCounter = enemySpawnInterval;
				enemies.Add(Instantiate(enemyPrefab, possibleDeliveries[0].transform.position, possibleDeliveries[0].transform.rotation) as GameObject);
			}
		}

		foreach (Text t in mainCanvas.GetComponentsInChildren<Text>())
		{
			if (t.name.Equals("Pizza Count"))
			{
				t.text = "PIZZA COUNT: " + playerTruck.HowManyPizzas();
			}

			if (t.name.Equals("Tips"))
			{
				t.text = "TIPS: £" + cash;
			}

			if (t.name.Equals("Timer"))
			{
				t.text = "" + (int) gameTimer;
			}
		}

		UpdateBoostMeter();
		UpdateRepulseMeter();
		CheckWinCondition();

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("main menu");
		}
	}

	private void UpdateBoostMeter()
	{
		Slider boostMeter = GetComponentsInChildren<Slider>()[0];
		boostMeter.value = playerTruck.GetBoostValue();
	}

	private void UpdateRepulseMeter()
	{
		Slider repulseMeter = GetComponentsInChildren<Slider>()[1];
		repulseMeter.value = playerTruck.GetRepulseValue();
	}

	private void CheckWinCondition()
	{
		if (gameTimer <= 0)
			return;
		
		if (pizzaDeliveries == null)
			return;
		

		foreach (PizzaDelivery pd in pizzaDeliveries)
		{
			if (pd.IsComplete())
			{
				deliveriesRemaining--;
				pizzaDeliveries.Remove(pd);
			}
		}

		if (deliveriesRemaining <= 0 && !lost)
		{
			won = true;
			winScreen.gameObject.SetActive(true);
			SaveToLeaderboard();
			leaderboard.SetActive(true);
		}

	}

	private void Lose()
	{
		if (won)
			return;
		
		loseScreen.gameObject.SetActive(true);
		leaderboard.SetActive(true);

		foreach (PizzaDelivery pd in pizzaDeliveries)
		{
			pd.gameObject.SetActive(false);
		}

		pizzaDeliveries.Clear();
	}

	public static void AddCash(int amount)
	{
		cash += amount;
	}

	public static void AddTime(float seconds)
	{
		gameTimer += seconds;
	}

	//PERSISTENCE

	public static void SaveToLeaderboard()
	{
		GameRecord currentGame = new GameRecord("Nige " + Random.Range(0, 10), timeElapsedThisGame, cash);
		gameRecords.Add(currentGame);

		SortLeaderboard();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		bf.Serialize(file, gameRecords);
		file.Close();
	}

	private static void SortLeaderboard()
	{
		List<GameRecord> sortedLeaderboard = new List<GameRecord>();

		gameRecords.Sort
		(delegate (GameRecord a, GameRecord b)
		{
				return a.GetTime().CompareTo(b.GetTime()); //try to sort the list based on time taken
		}
		);

		/*
		float timeCheck = 99999f;
		GameRecord leader = null;

		for (int i = 0; i < 10; i++)
		{
			foreach (GameRecord g in gameRecords)
			{
				if (g.GetTime() < timeCheck) //FIND THE LOWEST TIME ON THE BOARD, MAKE IT THE LEADER
				{
					leader = g;
					timeCheck = g.GetTime();
				}
			}

			print("adding " + leader.GetName() + " to sortboard");
			print("sortboard at " + sortedLeaderboard.Count);
			sortedLeaderboard.Add(leader); //PUT THE LEADER AT THE START OF THE SORTED LIST
			gameRecords.Remove(leader); //REMOVE THE LEADER FROM THE UNSORTED LIST AND LOOP AGAIN
		}

		gameRecords = sortedLeaderboard;
		*/
	}

	public static void LoadLeaderboard()
	{
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			gameRecords = (List<GameRecord>)bf.Deserialize(file);
			file.Close();
		}
	}
}
