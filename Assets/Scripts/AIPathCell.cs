using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPathCell : MonoBehaviour 
{
	//[HideInInspector]
	public List<GameObject> doors = new List<GameObject>();

	public float dir = -1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "AIPathDoor") 
		{
			doors.Add (other.gameObject);
		}

		if (other.tag == "Enemy") 
		{
			other.GetComponent<Enemy> ().currentCell = this.gameObject;
		}
	}
}
