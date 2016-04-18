using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI : MonoBehaviour 
{
	public Text healthText;
	public Text levelText;

	Player player;

	void Start()
	{
		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();
	}

	void Update()
	{
		DisplayPlayerHealth ();
		levelText.text = "Level -- " + GameManager.instance.currentLevel;
	}

	void DisplayPlayerHealth()
	{
		float healthPercentage = (player.health / player.maxHealth) * 100f;
		healthText.text = "Health -- " + (int)healthPercentage;
	}
}
