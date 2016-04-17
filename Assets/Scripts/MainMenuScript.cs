using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour 
{

	public void Play ()
	{
		SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);
	}

	public void Exit ()
	{
		Application.Quit ();
	}
}
