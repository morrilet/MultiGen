using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeLevelTrigger : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			GameManager.instance.currentLevel++;
			GameManager.instance.currentCharacter = other.GetComponent<Player> ().currentCharacter;
			GameManager.instance.playerHealth = other.GetComponent<Player> ().health;
			SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);
		}
	}
}
