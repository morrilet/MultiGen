using UnityEngine;
using System.Collections;

public class HairDryerWalkState : StateMachineBehaviour 
{
	bool footstep1Played;
	bool footstep2Played;

	float normalizedTime;
	float prevNormalizedTime;

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		normalizedTime = state.normalizedTime;

		if (Mathf.Round ((state.normalizedTime % 1) * 10) / 10f > .5f && !footstep2Played)
		{
			AudioManager.instance.PlaySoundEffectVariation("Hairdryer walking final", .97f, 1.3f);
			footstep2Played = true;
		}
		if (Mathf.Round ((state.normalizedTime % 1) * 10) / 10f > 0f && !footstep1Played) 
		{
			AudioManager.instance.PlaySoundEffectVariation("Hairdryer walking final", .97f, 1.3f);
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
}
