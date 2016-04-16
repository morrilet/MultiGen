using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPathCell : MonoBehaviour 
{
	[HideInInspector]
	public List<GameObject> doors = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "AIPathDoor") 
		{
			doors.Add (other.gameObject);
		}
	}
}
