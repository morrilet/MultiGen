using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
	public override void Awake()
	{
		isPersistant = true;
		base.Awake();
	}

	void Start ()
	{
		
	}

	void Update () 
	{

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
