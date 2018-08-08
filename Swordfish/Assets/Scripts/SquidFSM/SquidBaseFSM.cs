using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBaseFSM : StateMachineBehaviour
{
    public GameObject squid;
    public GameObject shark;
    public SquidAI controller;

    public float rotationSpeed = 1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        squid = animator.gameObject;
        controller = squid.GetComponent<SquidAI>();
        shark = controller.FindShark();        
    }
}
