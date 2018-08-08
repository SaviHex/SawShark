using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float delay = 0.1f;
    
    private bool makeGhosts;
    //private GameObject ghost;

    private Transform t;
    //private SpriteRenderer sr;

    void Start()
    {
        makeGhosts = false;
        t = GetComponent<Transform>();
        //sr = GetComponent<SpriteRenderer>();
    }
    
    public void StartGhostTrail()
    {
        if (!makeGhosts)
        {
            makeGhosts = true;
            StartCoroutine(GhostTrail());
        }
        else
        {
            Debug.LogError("StartGhostTrail was called while the coroutine was running.", this);
        }
    }

    public void StopGhostTrail()
    {
        if (makeGhosts)
        {
            makeGhosts = false;
        }
        else
        {
            Debug.LogError("StopGhostTrail was called but it has already stopped!", this);
        }
    }

    private IEnumerator GhostTrail()
    {
        while (makeGhosts)
        {
            //ghost = Instantiate(ghostPrefab, t.position, t.rotation);
            //ghost.GetComponent<SpriteRenderer>().sprite = sr.sprite;

            // Optimized for this game.
            Instantiate(ghostPrefab, t.position, t.rotation);

            yield return new WaitForSeconds(delay);
        }
    }
}
