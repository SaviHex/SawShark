﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Drops))]
public class Killable : MonoBehaviour
{
    private Drops[] drops;
    //private PointOfInterest container;
    public bool possibleRemovalEvaluated;
    public static float distanceLimit = 80f;

    private void Start()
    {
        drops = GetComponents<Drops>();
        //container = GetComponentInParent<PointOfInterest>();
        possibleRemovalEvaluated = false;
    }

    private void Update()
    {
        //if(!possibleRemovalEvaluated && container != null && Vector3.Distance(transform.position, container.transform.position) > distanceLimit)
        //{
        //    possibleRemovalEvaluated = true;
        //    container.RemoveChild(transform);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shark") || collision.CompareTag("Torpedo"))
        {
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Shark") || collision.CompareTag("Torpedo"))
        {
            Die();
        }
    }

    private void Die()
    {
        //if(container != null)
        //{
        //    container.RemoveChild(transform);
        //}

        if (tag == "Fish")
        {
            LastPointStats.Instance.Fish += 1;
            Debug.Log("Fish++");
        }
        else
        {
            LastPointStats.Instance.Squid += 1;
            Debug.Log("Squid++");
        }


        DropAllLoot();
        Destroy(gameObject);
    }

    private void DropAllLoot()
    {
        foreach (Drops d in drops)
        {
            d.DropLoot();
        }
    }
}
