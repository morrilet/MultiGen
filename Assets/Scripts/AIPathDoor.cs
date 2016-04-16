using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPathDoor : MonoBehaviour 
{
	public GameObject[] cells;
	public int[] doorsToCells;
	public List<GameObject> immediateCells = new List<GameObject>();
	bool testForCells = true;
	float waitToTestCells = 2f;
	int stage = 1;

	public bool doorOpen = true;

	void Awake()
	{
		doorOpen = true;
		cells = GameObject.FindGameObjectsWithTag ("AIPathCell");
		doorsToCells = new int[cells.Length];
		testForCells = true;
		waitToTestCells = 2;
		stage = 1;
	}

	void Update()
	{
		if (testForCells && waitToTestCells <= 0) 
		{
			foreach (GameObject immediateCell in immediateCells) 
			{
				for (int i = 0; i <= cells.Length - 1; i++) 
				{
					if (cells [i] == immediateCell)
						doorsToCells [i] = 1;
				}
			}

			for (stage = 2; stage <= cells.Length; stage++) 
			{
				for (int i = 0; i <= cells.Length - 1; i++) 
				{
					if (doorsToCells [i] == stage - 1)
						foreach (GameObject checkDoor in cells[i].GetComponent<AIPathCell>().doors) 
						{
							if (checkDoor != gameObject) 
							{
								foreach (GameObject checkCell in checkDoor.GetComponent<AIPathDoor>().immediateCells) 
								{
									for (int j = 0; j <= cells.Length - 1; j++) 
									{
										if (cells [j] == checkCell && doorsToCells [j] == 0)
											doorsToCells [j] = stage;
									}
								}
							}
						}
				}
			}

			testForCells = false;
		}
		waitToTestCells--;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "AIPathCell")
			immediateCells.Add (other.gameObject);
	}
}
