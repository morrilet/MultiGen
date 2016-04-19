using UnityEngine;
using System.Collections;

public class PatrolState : StateMachineBehaviour 
{
	GameObject currentCell;

	bool randomizedCourse;
	bool calculatedNewRandomizedCourseVector;
	Vector3 randomizedCourseVector;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		currentCell = animator.GetComponent<Enemy> ().currentCell;
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
	}

	Vector3 FindRandomSpotWithinCurrentCell()
	{
		if(currentCell != null)
			return currentCell.transform.position + (currentCell.transform.rotation * new Vector3(Random.Range(
				currentCell.transform.localScale.x * -0.5f, currentCell.transform.localScale.x * 0.5f), Random.Range(
					currentCell.transform.localScale.y * -0.5f, currentCell.transform.localScale.y * 0.5f), 0));

		return Vector3.zero;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
