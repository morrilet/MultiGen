using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

	public bool isPaused;
	public bool isPausedPrevious;

	[HideInInspector]
	public int currentLevel;
	[HideInInspector]
	public int currentCharacter;
	[HideInInspector]
	public float playerHealth;

	public override void Awake()
	{
		isPersistant = true;
		isPaused = false;
		base.Awake();
	}

	void Start ()
	{
		currentCharacter = 1;
		playerHealth = GameObject.FindWithTag ("Player").GetComponent<Player> ().health;
		currentLevel = 1;
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isPaused = (isPaused) ? false : true;
		}
		isPausedPrevious = isPaused;
	}
		
	public void StartGame()
	{
		SceneManager.LoadScene ("LevelGenTest", LoadSceneMode.Single);

		playerHealth = GameObject.FindWithTag ("Player").GetComponent<Player> ().maxHealth;
		currentCharacter = 1;
		GameObject.FindWithTag ("Player").GetComponent<Player> ().Start ();
	}

	public void Sleep(int framesOfSleep)
	{
		StartCoroutine (ApplySleep (framesOfSleep));
	}

	IEnumerator ApplySleep(int framesOfSleep)
	{
		for (int i = 0; i < framesOfSleep; i++)
		{
			Time.timeScale = 0f;
			yield return null;
		}

		Time.timeScale = 1f;
	}
}
