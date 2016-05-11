using UnityEngine;
using System.Collections;

public class DeathState : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		animator.SetBool ("Dead", false);
		animator.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName = "Corpse";
		animator.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		animator.transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingLayerName = "Corpse";
		animator.transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 1;
		animator.gameObject.layer = LayerMask.NameToLayer ("Corpse");
		Destroy (animator.gameObject.GetComponent<Enemy> ());
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	//{
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	//{
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
