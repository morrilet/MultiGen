using UnityEngine;
using System.Collections;

public class BearWalkState : StateMachineBehaviour {

	bool footstep1Played;
	bool footstep2Played;

	float normalizedTime;
	float prevNormalizedTime;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		normalizedTime = state.normalizedTime;

		/*
		soundTime = state.length;

		if (soundTimer > 0 && soundTimer < soundTime / 20f) 
		{
			AudioManager.instance.PlaySoundEffect("shot 1 (uncharged)");
			Debug.Log("played");
		}
		if (soundTimer > soundTime * .5f && soundTimer < (soundTime * .5f) + (soundTime / 20f)) 
		{
			AudioManager.instance.PlaySoundEffect("shot 1 (uncharged)");
			Debug.Log("played");
		}

		if (soundTimer > soundTime)
			soundTimer = 0;

		soundTimer += Time.deltaTime;
		*/

		if (Mathf.Round ((state.normalizedTime % 1) * 10) / 10f > .5f && !footstep2Played)
		{
			AudioManager.instance.PlaySoundEffectVariation("footstep 1", .97f, 1.3f);
			footstep2Played = true;
		}
		if (Mathf.Round ((state.normalizedTime % 1) * 10) / 10f > 0f && !footstep1Played) 
		{
			AudioManager.instance.PlaySoundEffectVariation("footstep 1", .97f, 1.3f);
			footstep1Played = true;
		}

		if (normalizedTime - (normalizedTime % 1) != prevNormalizedTime - (prevNormalizedTime % 1)) 
		{
			footstep1Played = false;
			footstep2Played = false;
		}

		prevNormalizedTime = normalizedTime;
		Debug.Log (Mathf.Round ((state.normalizedTime % 1) * 10) / 10f);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
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
