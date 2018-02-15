using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {


	public void NewGame()
	{
		SceneManager.LoadScene(1);
	}

	public void LoadScene(int number)
	{
		SceneManager.LoadScene(number);
	}

	public void Credits()
	{
		SceneManager.LoadScene("Credits");
	}

	public void Exit()
	{
		Application.Quit();
	}

}
