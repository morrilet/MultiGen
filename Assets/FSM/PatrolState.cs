using UnityEngine;
using System.Collections;

public class PatrolState : StateMachineBehaviour 
{
	GameObject currentCell;

	bool randomizedCourse;
	bool calculatedNewRandomizedCourseVector;
	Vector3 randomizedCourseVector;

	float stuckTime;
	float stuckTimer;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		currentCell = animator.GetComponent<Enemy> ().currentCell;

		stuckTime = 5f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		currentCell = animator.GetComponent<Enemy> ().currentCell;

		if (!calculatedNewRandomizedCourseVector) 
		{
			randomizedCourseVector = FindRandomSpotWithinCurrentCell ();
			calculatedNewRandomizedCourseVector = true;
		}

		if (Vector3.Distance (animator.transform.position, randomizedCourseVector) < animator.transform.localScale.x) 
		{
			calculatedNewRandomizedCourseVector = false;
		}

		if (!randomizedCourse) 
		{
			animator.transform.position += (randomizedCourseVector - animator.transform.position).normalized * animator.GetComponent<Enemy>().speed * Time.deltaTime;
		}

		if (stuckTimer >= stuckTime) 
		{
			randomizedCourseVector = FindRandomSpotWithinCurrentCell ();
		}

		GameObject[] walls = GameObject.FindGameObjectsWithTag ("Wall");
		for (int i = 0; i < walls.Length; i++) 
		{
			if (animator.gameObject.GetComponent<Collider2D> ().IsTouching (walls [i].GetComponent<Collider2D> ()))
				randomizedCourseVector = FindRandomSpotWithinCurrentCell ();
		}

		Debug.DrawLine (animator.transform.position, randomizedCourseVector, Color.green);
		stuckTimer += Time.deltaTime;
	}

	Vector3 FindRandomSpotWithinCurrentCell()
	{
		if (currentCell != null) 
		{
			stuckTimer = 0f;

			return currentCell.transform.position + (currentCell.transform.rotation * new Vector3 (Random.Range (
				currentCell.transform.localScale.x * -0.4f, currentCell.transform.localScale.x * 0.4f), Random.Range (
				currentCell.transform.localScale.y * -0.4f, currentCell.transform.localScale.y * 0.4f), 0));
		}

		return Vector3.zero;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
