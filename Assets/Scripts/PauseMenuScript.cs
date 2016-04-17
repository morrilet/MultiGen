using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour 
{
	GameObject[] childObjects;

	void Start ()
	{
		GameManager.instance.isPaused = false;
		childObjects = new GameObject [transform.childCount];
		for (int i = 0; i < childObjects.Length; i++)
		{
			childObjects [i] = transform.GetChild (i).gameObject;
		}
	}
	

	void Update ()
	{
		if (GameManager.instance.isPaused) 
		{
			for (int i = 0; i < childObjects.Length; i++) 
			{
				childObjects [i].SetActive (true);
			}
		}

		if (!GameManager.instance.isPaused) 
		{
			for (int i = 0; i < childObjects.Length; i++) 
			{
				childObjects [i].SetActive (false);
			}
		}
	}

	public void Resume()
	{
		GameManager.instance.isPaused = false;
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}
