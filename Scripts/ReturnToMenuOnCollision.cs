using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnToMenuOnCollision : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("MainCamera"))
		{
			SceneManager.LoadScene(0);
		}
	}
}
