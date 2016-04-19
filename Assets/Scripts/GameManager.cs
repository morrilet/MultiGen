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

	public void FlashWhite(SpriteRenderer sprite, float duration, Color baseColor)
	{
		StartCoroutine(ApplyFlashWhite(sprite, duration, baseColor));
	}

	IEnumerator ApplyFlashWhite(SpriteRenderer sprite, float duration, Color baseColor)
	{
		for (float i = 0; i < duration; i += Time.deltaTime) 
		{	
			if(sprite != null)
				sprite.color = new Color (baseColor.r + .5f * .3f, baseColor.g + .5f * .59f, baseColor.b + .5f * .11f);
			yield return null;
		}

		if(sprite != null)
			sprite.color = baseColor;
	}
}
