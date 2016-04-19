using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScript : MonoBehaviour 
{
	public void Retry()
	{
		GameManager.instance.StartGame ();
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}
