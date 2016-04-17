using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour 
{
	public GameObject currentCell;
	Player player;
	GameObject playerCell;
	public GameObject goalDoor;
	public float shortestPathSoFar;
	float waitToStart = 5f;

	float maxMoveSpeed = 3f;
	float minMoveSpeed = 1f;
	float speedRecover = 1f;
	float speedDamage = .1f;
	float currentMoveSpeed = 2f;

	bool randomizedCourse = false;
	public Vector3 randomizeCourseVector;
	bool calculatedNewRandomizeCourseVector = false;

	float viewAngle = 360f;
	bool aware = true;
	float unawareSpeed = 1f;

	Vector3 lastPos;

	bool stuck = false;

	void Awake()
	{
		shortestPathSoFar = Mathf.Infinity;
		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();
		waitToStart = 5f;
		randomizeCourseVector = transform.position;
		aware = false;
		lastPos = transform.position;
	}

	void Update()
	{
		if (waitToStart <= 0) 
		{
			playerCell = player.currentCell;
			foreach (GameObject doorCheckingNow in currentCell.GetComponent<AIPathCell>().doors) 
			{
				for (int i = 0; i <= doorCheckingNow.GetComponent<AIPathDoor> ().cells.Length - 1; i++) 
				{
					if (doorCheckingNow.GetComponent<AIPathDoor> ().cells [i] == playerCell)
					if (doorCheckingNow.GetComponent<AIPathDoor> ().doorsToCells [i] < shortestPathSoFar) 
					{
						goalDoor = doorCheckingNow;
						shortestPathSoFar = doorCheckingNow.GetComponent<AIPathDoor> ().doorsToCells [i];
					}
				}
			}
			shortestPathSoFar = Mathf.Infinity;
		}
		waitToStart--;

		RaycastHit2D[] hits;
		bool anyhit = false;
		if (Vector3.Angle (transform.up, player.transform.position - transform.position) < viewAngle / 2 && !aware) 
		{
			hits = Physics2D.CircleCastAll (transform.position, player.transform.localScale.x / 3, 
				player.transform.position - transform.position, Vector3.Distance (player.transform.position, transform.position));
			foreach (RaycastHit2D hit in hits) 
			{
				if (hit.transform.tag == "Terrain")
					anyhit = true;
			}
			if (!anyhit)
				aware = true;
		}

		if (!aware)
			goalDoor = null;

		if (goalDoor)
		if (!goalDoor.GetComponent<AIPathDoor> ().doorOpen)
			goalDoor = null;

		if (!calculatedNewRandomizeCourseVector) 
		{
			randomizeCourseVector = findRandomSpotWithinCurrentCell ();
			calculatedNewRandomizeCourseVector = true;
		}

		if (currentCell != playerCell || playerCell == null) 
		{
			if (randomizedCourse && goalDoor)
				transform.position += (goalDoor.transform.position - transform.position).normalized * currentMoveSpeed * Time.deltaTime;
			if (!randomizedCourse) 
			{
				transform.position += (randomizeCourseVector - transform.position).normalized * currentMoveSpeed * Time.deltaTime;
			}
			if (Vector3.Distance (transform.position, randomizeCourseVector) < transform.localScale.x) 
			{
				if (goalDoor)
					randomizedCourse = true;
				if (goalDoor == null)
					calculatedNewRandomizeCourseVector = false;
			}
		}

		if(goalDoor)
		if (currentCell != playerCell && Vector3.Distance (goalDoor.transform.position, transform.position) < .05f) 
		{
			Debug.Log ("Stuck");
			calculatedNewRandomizeCourseVector = false;
			randomizedCourse = false;
			stuck = true;
		}

		if (goalDoor)
		if (stuck && Vector3.Distance (goalDoor.transform.position, transform.position) > .75f) 
		{
			Debug.Log ("Unstuck");
			stuck = false;
		}

		if(aware && playerCell != null && !stuck)
			randomizedCourse = true;

		if (currentCell == playerCell && Vector3.Distance(player.transform.position, transform.position) > transform.localScale.x + .5f)
			transform.position += (player.transform.position - transform.position).normalized * currentMoveSpeed * Time.deltaTime;

		if (Vector3.Distance (player.transform.position, transform.position) < transform.localScale.x + .4f)
			aware = true;

		if (currentMoveSpeed < maxMoveSpeed && aware)
			currentMoveSpeed += speedRecover * Time.deltaTime;
		if (currentMoveSpeed < unawareSpeed && !aware)
			currentMoveSpeed += speedRecover * Time.deltaTime;

		if (currentMoveSpeed > maxMoveSpeed && aware)
			currentMoveSpeed = maxMoveSpeed;
		if (currentMoveSpeed > unawareSpeed && !aware)
			currentMoveSpeed = unawareSpeed;

		if (currentCell == playerCell && randomizedCourse)
			LookAtPlayerPos ();
		else
			LookAtTargetPos ();
		lastPos = transform.position;

		//Debug.Log ("Path cell @ " + currentCell + " w/ goaldoor " + goalDoor + ". " + Time.time);
	}

	void LookAtPlayerPos()
	{
		Vector3 targetPos = player.transform.position;

		//Get rotation in radians.
		float rotation = Mathf.Atan2 (targetPos.y - transform.position.y, targetPos.x - transform.position.x);
		//Convert to degrees.
		rotation *= (180 / Mathf.PI);

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (0, 0, rotation - 90), 0.05f);
	}

	void LookAtTargetPos()
	{
		Vector3 targetPos = transform.position;

		//Get rotation in radians.
		float rotation = Mathf.Atan2 (targetPos.y - lastPos.y, targetPos.x - lastPos.x);
		//Convert to degrees.
		rotation *= (180 / Mathf.PI);

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (0, 0, rotation - 90), 0.05f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "AIPathCell") 
		{
			currentCell = other.gameObject;
			randomizedCourse = false;
			calculatedNewRandomizeCourseVector = false;
			//Debug.Log ("Entered new path cell @ " + currentCell + " w/ goaldoor " + goalDoor + ". " + Time.time);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Enemy" && other.gameObject != gameObject) 
		{
			if (currentMoveSpeed > minMoveSpeed)
				currentMoveSpeed -= speedDamage;
			transform.position += (transform.position - other.transform.position).normalized * 0.025f;
		}
	}

	Vector3 findRandomSpotWithinCurrentCell()
	{
		return currentCell.transform.position + (currentCell.transform.rotation * new Vector3(Random.Range(
			currentCell.transform.localScale.x * -0.5f, currentCell.transform.localScale.x * 0.5f), Random.Range(
				currentCell.transform.localScale.y * -0.5f, currentCell.transform.localScale.y * 0.5f), 0));
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, randomizeCourseVector);

		if (goalDoor != null) 
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (transform.position, goalDoor.transform.position);
		}
	}
}
