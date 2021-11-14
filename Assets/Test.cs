using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : StateMachineBehaviour
{

    AnimationOffset animationOffset;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animationOffset = animator.gameObject.transform.GetChild(1).GetComponent<AnimationOffset>();
        animationOffset.UpdateAnimator = true;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.length > 2.5)
        {
            animationOffset.UpdateAnimator = false;
        }

        if (stateInfo.length < 0.1f)
        {
            animationOffset.UpdateAnimator = true;
        }
    }

// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animationOffset.UpdateAnimator = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
