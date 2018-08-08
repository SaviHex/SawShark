using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRun : FishBaseFSM { 

    void StartSpeedingUp()
    {

    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        fish.GetComponent<FishAI>().StartSpeedUp();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var dir = (shark.transform.position - fish.transform.position);
        fish.transform.up = Vector3.Slerp(fish.transform.up, (fish.transform.position + dir) - fish.transform.position, 2 * rotationSpeed * Time.deltaTime);
        Debug.DrawLine(fish.transform.position, shark.transform.position, Color.red); // Debug.
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fish.GetComponent<FishAI>().StartSlowDown();
    }
}
