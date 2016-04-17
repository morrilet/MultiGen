using UnityEngine;
using System.Collections;

public class ChasePlayerState : StateMachineBehaviour 
{
	GameObject currentCell;
	GameObject playerCell;
	GameObject goalDoor;
	float shortestPathSoFar;

	Player player;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
		shortestPathSoFar = Mathf.Infinity;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		playerCell = player.currentCell;
		currentCell = animator.GetComponent<Enemy> ().currentCell;

		Debug.Log (currentCell.GetComponent<AIPathCell> ().doors.Count);
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

		if (currentCell != playerCell && goalDoor)
			animator.transform.position += (goalDoor.transform.position - animator.transform.position).normalized * animator.GetComponent<Enemy>().speed * Time.deltaTime;
		if(player.GetComponent<Collider2D>().IsTouching(currentCell.GetComponent<Collider2D>()))
			animator.transform.position += (player.transform.position - animator.transform.position).normalized * animator.GetComponent<Enemy>().speed * Time.deltaTime;
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
