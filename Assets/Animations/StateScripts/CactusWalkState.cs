using UnityEngine;
using System.Collections;

public class CactusWalkState : StateMachineBehaviour 
{
	float normalizedTime;
	float prevNormalizedTime;

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		normalizedTime = state.normalizedTime;

		if (normalizedTime - (normalizedTime % 1) != prevNormalizedTime - (prevNormalizedTime % 1)) 
		{
			AudioManager.instance.PlaySoundEffectVariation("Cactus Jump final", .97f, 1.3f);
		}

		prevNormalizedTime = normalizedTime;
		Debug.Log (Mathf.Round ((state.normalizedTime % 1) * 10) / 10f);
	}
}
