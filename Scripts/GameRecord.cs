using System;

namespace PizzaGameScripts
{
	[System.Serializable]
	public class GameRecord
	{

		string name;
		float time;
		int tips;

		public GameRecord(string playerName, float timeTaken, int cash)
		{
			this.name = playerName;
			this.time = timeTaken;
			this.tips = cash;
		}

		public string GetName()
		{
			return name;
		}
		public float GetTime()
		{
			return time;
		}

		public int GetTips()
		{
			return tips;
		}


		public string GetDescription()
		{
			return name + ", TIME: " + (int) time + ", CASH: " + tips;
		}

	}
}

