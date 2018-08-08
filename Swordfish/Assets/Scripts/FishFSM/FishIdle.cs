using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIdle : FishBaseFSM
{
    // Store diretion.
    float x;
    float y;
    // How many cicles will it have to wait in order to change diretion.
    // Decided at random between min and max values.
    int minWaitCycles = 100;
    int maxWaitCycles = 150;
    int waitCycles;

    public void ChangeDiretion()
    {
        waitCycles = Random.Range(minWaitCycles, maxWaitCycles);        
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);        
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        waitCycles = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        // If its been wainting long enough, change diretion.
        if (waitCycles == 0)
        {
            ChangeDiretion();
        }

        // Gradually rotate to the target diretion.
        Vector3 dir = new Vector3(-1 * x,-1 * y);        
        fish.transform.up = Vector3.Slerp(fish.transform.up, (fish.transform.position + dir) - fish.transform.position, rotationSpeed * Time.deltaTime);

        waitCycles--;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
