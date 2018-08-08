using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DisposableContainer : MonoBehaviour
{
    private void Awake()
    {
        // Unparent all direct children.
        Transform[] children = transform.Cast<Transform>().ToArray();
        Transform parent = transform.parent;
        foreach(Transform c in children)
        {
            c.parent = parent;
        }

        // Destroy this container.
        Destroy(gameObject);
    }
}
