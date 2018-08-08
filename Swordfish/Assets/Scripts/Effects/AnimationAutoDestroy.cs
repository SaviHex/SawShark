using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationAutoDestroy : MonoBehaviour
{
    public float delay = 0f;  

    public void DestroyMe()
    {        
        Destroy(gameObject, delay);
    }
}