using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabContainer : MonoBehaviour
{  
    private void Start()
    {
        // Unparent all direct children.
        foreach (Transform t in gameObject.transform)
        {
            t.parent = transform.parent;
        }

        // Destroy this container.
        Destroy(gameObject);
    }    
}
