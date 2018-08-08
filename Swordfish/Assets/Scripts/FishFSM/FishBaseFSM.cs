using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBaseFSM : StateMachineBehaviour
{
    public GameObject fish;
    public GameObject shark;
    public float speed = 30f;
    public float rotationSpeed = 4f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fish = animator.gameObject;
        shark = fish.GetComponent<FishAI>().FindShark();
    }

}
