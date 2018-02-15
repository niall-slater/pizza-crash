using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardTable : MonoBehaviour {

	public GameObject[,] table;


	void Awake ()
	{
		table = new GameObject[3,10];

		for (int y = 0; y < 10; y++)
		{
			GameObject recordText = new GameObject("recordtext " + y + ", name");
			recordText.transform.position = this.transform.position;
			recordText.transform.SetParent(this.transform);
			recordText.AddComponent<Text>();
			table[0,y] = recordText;
			table[0,y].GetComponent<Text>().text = PizzaGameManager.gameRecords[y].GetName();
			table[0,y].GetComponent<Text>().font = Resources.FindObjectsOfTypeAll<Font>()[1]; //1 is the index in this project for Joystix Monospace
			table[0,y].transform.Translate(-100f, 185f - (40 * y), 0f);
		}


		for (int y = 0; y < 10; y++)
		{
			GameObject recordText = new GameObject("recordtext " + y + ", time");
			recordText.transform.position = this.transform.position;
			recordText.transform.SetParent(this.transform);
			recordText.AddComponent<Text>();
			table[1,y] = recordText;
			table[1,y].GetComponent<Text>().text = "" + PizzaGameManager.gameRecords[y].GetTime().ToString("##.##");
			table[1,y].GetComponent<Text>().font = Resources.FindObjectsOfTypeAll<Font>()[1];
			table[1,y].transform.Translate(0f, 185f - (40 * y), 0f);
		}


		for (int y = 0; y < 10; y++)
		{
			GameObject recordText = new GameObject("recordtext " + y + ", tips");
			recordText.transform.position = this.transform.position;
			recordText.transform.SetParent(this.transform);
			recordText.AddComponent<Text>();
			table[2,y] = recordText;
			table[2,y].GetComponent<Text>().text = "£" + PizzaGameManager.gameRecords[y].GetTips();
			table[2,y].GetComponent<Text>().font = Resources.FindObjectsOfTypeAll<Font>()[1];
			table[2,y].transform.Translate(100f, 185f - (40 * y), 0f);
		}


	}

}