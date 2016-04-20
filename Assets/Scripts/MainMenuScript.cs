using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour 
{
	public void Start ()
	{
		AudioManager.instance.PlayMusic ("menu music louder");
	}

	public void Play ()
	{
		AudioManager.instance.StopMusic ();
		AudioManager.instance.PlayMusicWithIntro ("main music intro", "main music loop");
		SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);
		//GameManager.instance.StartGame ();
	}

	public void PlayTutorial()
	{
		AudioManager.instance.StopMusic ();
		AudioManager.instance.PlayMusicWithIntro ("main music intro", "main music loop");
		SceneManager.LoadScene ("Tutorial", LoadSceneMode.Single);
	}

	public void Exit ()
	{
		Application.Quit ();
	}
}
