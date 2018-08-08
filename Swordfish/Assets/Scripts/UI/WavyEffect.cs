using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyEffect : MonoBehaviour
{
    public float frequency;
    public float magnitude;
    public float offset;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        float y = Mathf.Sin((offset + Time.time) * frequency) * magnitude;
        transform.localPosition = startPos + new Vector3(0.0f, y, 0.0f);
    }
}
