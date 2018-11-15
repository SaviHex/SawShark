using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOfInterestManager : MonoBehaviour
{    
    public RadarController radar;
    public float recalculationTimeOut = 1.5f;

    public List<Transform> pointsOfInterest;
    public Transform nearestPoint;

    public float minDist = float.MaxValue;
    public float dist = 0;

    void Start()
    {
        if(radar != null)
        {
            StartCoroutine(FindNearestPoint());
        }
        else
        {
            Debug.LogError("PointsOfInterestManager: ´radar´ variable was not set.");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            pointsOfInterest.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            pointsOfInterest.Remove(collision.transform);
        }
    }

    private void OnDrawGizmos()
    {
        if(nearestPoint != null)
        {
            Gizmos.DrawLine(transform.position, nearestPoint.position);
        }        
    }  

    private IEnumerator FindNearestPoint()
    {               
        while (enabled)
        {
            minDist = float.MaxValue;

            for (int i = 0; i < pointsOfInterest.Count; i++)
            {
                if(pointsOfInterest[i] == null)
                {                    
                    pointsOfInterest.Remove(pointsOfInterest[i]);
                }
                else
                {
                    dist = Vector3.Distance(transform.position, pointsOfInterest[i].position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestPoint = pointsOfInterest[i];
                    }
                }
                
                yield return new WaitForEndOfFrame();
            }

            radar.SetNearestPoint(nearestPoint);
            yield return new WaitForSeconds(recalculationTimeOut);
        }
    }
}
