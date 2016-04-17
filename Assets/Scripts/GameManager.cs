using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

	public bool isPaused;
	public bool isPausedPrevious;

	public override void Awake()
	{
		isPersistant = true;
		isPaused = false;
		base.Awake();
	}

	void Start ()
	{
		
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isPaused = (isPaused) ? false : true;
		}
		isPausedPrevious = isPaused;
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
