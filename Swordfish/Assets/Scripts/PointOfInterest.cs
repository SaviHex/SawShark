using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public RadarController radar;
    public List<Transform> children;
    private Transform[] beginningChildrenState;
    public Transform target;
    private Vector3 childrenSum;
    public GameObject targetPrefab;

    private void Start()
    {
        radar = GameObject.FindGameObjectWithTag("Radar").GetComponent<RadarController>();

        // Generates the target object and puts it into the radar.
        target = Instantiate(targetPrefab).GetComponent<Transform>();
        radar.AddPointOfInterest(target);

        // Create a list of child objects.
        foreach (Transform t in gameObject.transform)
        {
            children.Add(t);
        }

        beginningChildrenState = children.ToArray();
    }

    private void Update()
    {
        // If there's few objects (less than two) in this point it becomes uninteresting.
        if (children.Count >= 2)
        {
            UpdatePoint();
        }
        else
        {
            TerminatePoint();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(childrenSum, 80f);
    }

    private void UpdatePoint()
    {
        try
        {
            // Calculate the mean product of the children's positions.
            childrenSum = Vector3.zero;
            foreach (Transform t in children)
            {
                childrenSum += t.position;
            }
            childrenSum /= (children.Count > 0) ? children.Count : 1; // Thee shall not divide by zero.
                                                                      // Set the target position to be the mean product.
            target.position = childrenSum;
        } catch (Exception e)
        {
            Debug.Log(gameObject.name + "");

            foreach (Transform t in children)
            {
                if(t != null)
                    Debug.Log(t.name);
            }

            Debug.Log(e.Message);
        }
    }

    private void TerminatePoint()
    {
        // Remove point from radar.
        radar.RemovePointOfInterest(target);
        // Clean up the target.
        Destroy(target.gameObject);
        // Unparent all direct children.
        foreach (Transform t in beginningChildrenState)
        {
            //Debug.Log("Unparenting " + t.name + "...\n" + t.parent + " --> " + transform.parent);
            t.parent = transform.parent;
        }
        // Destroy this container.
        Destroy(gameObject);
    }

    public void RemoveChild(Transform child)
    {
        // The target cannot be left alone.
        // It can only remove if it has more then one child.
        if (children.Count > 2)
        {
            //Debug.Log("Removing " + child.name + "...");
            children.Remove(child);
            UpdatePoint();
        }
    }
}
