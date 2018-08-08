using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidRun : SquidBaseFSM
{
    int waitCycles;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        squid.transform.up = squid.transform.position - shark.transform.position;
        Debug.DrawLine(squid.transform.position, shark.transform.position);
        controller.Impulse();
        controller.Ink();
        waitCycles = 50;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawLine(squid.transform.position, shark.transform.position);
        squid.transform.up = squid.transform.position - shark.transform.position;
        if (waitCycles <= 0)
        {            
            animator.SetBool("rundone", true);
        }

        waitCycles--;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("rundone", false);        
    }
}
