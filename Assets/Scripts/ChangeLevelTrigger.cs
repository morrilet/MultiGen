using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevelTrigger : MonoBehaviour 
{
	public bool isForTutorial;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			if (!isForTutorial) 
			{
				GameManager.instance.currentLevel++;
				GameManager.instance.currentCharacter = other.GetComponent<Player> ().currentCharacter;
				GameManager.instance.playerHealth = other.GetComponent<Player> ().health;
				SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);
			} 
			else 
			{
				GameManager.instance.StartGame ();
			}
		}
	}
}
