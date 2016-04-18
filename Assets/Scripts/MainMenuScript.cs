using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour 
{
	public void Start ()
	{
		AudioManager.instance.PlayMusic ("menu music");
	}

	public void Play ()
	{
		AudioManager.instance.StopMusic ();
		SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);
	}

	public void Exit ()
	{
		Application.Quit ();
	}
}
