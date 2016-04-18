using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevelTrigger : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);
		}
	}
}
