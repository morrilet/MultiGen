using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour 
{
	public float xMin, xMax;
	public float yMin, yMax;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		//Horiz lines.
		Gizmos.DrawLine (new Vector3 (xMin, yMax), new Vector3 (xMax, yMax));
		Gizmos.DrawLine (new Vector3 (xMin, yMin), new Vector3 (xMax, yMin));

		//Vert lines.
		Gizmos.DrawLine (new Vector3 (xMin, yMin), new Vector3 (xMin, yMax));
		Gizmos.DrawLine (new Vector3 (xMax, yMin), new Vector3 (xMax, yMax));
	}
}