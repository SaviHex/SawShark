using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadarController : MonoBehaviour
{
    [Header("Positions")]
    public Transform player;
    //public List<Transform> pointsOfInterest;
    public Transform nearestPoint;
    

    [Header("GUI")]
    public GameObject pointerPrefab;
    public RectTransform pointer;
    //public List<RectTransform> pointers;

    

    [Header("Screen")]
    public float margin;
    public float width;
    public float height;  

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Shark").GetComponent<Transform>();
        height = Camera.main.orthographicSize - margin;
        width = Mathf.Ceil((Camera.main.orthographicSize * Screen.width) / Screen.height) - margin;
        pointer = Instantiate(pointerPrefab, transform).GetComponent<RectTransform>();
    }

    private void Update()
    {
        //int len = pointsOfInterest.Count;
        //for(int i = 0; i < len; i++)
        //{
        //    Move(pointers[i], pointsOfInterest[i].position);
        //}

        if (nearestPoint != null)
        {
            Move(pointer, nearestPoint.position);
        }
    }

    private void Move(RectTransform pointer, Vector2 target)
    {
        Vector2 origin = player.position;

        Debug.DrawLine(origin, target); // TODO: Debug.

        Vector2 dir = (target - origin);       

        pointer.up = dir; // Rotation.
        
        // Constrain the pointer inside the screen.
        dir.x = Mathf.Clamp(target.x, (-1 * width) + origin.x, width + origin.x);
        dir.y = Mathf.Clamp(target.y, (-1 * height) + origin.y, height + origin.y);

        // If the target is visible (the constrain had no effect over the position), do not show the pointer.
        pointer.gameObject.SetActive(dir != target);

        // Move the pointer to the desired position.
        pointer.position = dir;
    }

    public void SetNearestPoint(Transform t)
    {
        nearestPoint = t;
    }

    /*
    public void AddPointOfInterest(Transform t)
    {
        // Adds the transform.
        pointsOfInterest.Add(t);
        // Instantiate a new pointer and store it in the list.
        GameObject newPointer = Instantiate(pointerPrefab, transform);
        pointers.Add(newPointer.GetComponent<RectTransform>());
    }
   
    public void RemovePointOfInterest(Transform t)
    {
        int i;
        for(i = 0; i < pointsOfInterest.Count; i++)
        {
            if(t == pointsOfInterest[i])
            {
                // Remove reference in list.
                pointsOfInterest.RemoveAt(i);

                // Destroy and remove reference for the pointer.
                Destroy(pointers[i].gameObject);
                pointers.RemoveAt(i);
            }                
        }
    }
    */
}
