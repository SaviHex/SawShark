using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControls : MonoBehaviour
{    
    public bool rightPressed;
    public bool leftPressed;

    private float value;    
    private float target;
    public float velocity = 10;   

    public float Value { get { return value; } }

    private void UpdateAxisValue(float v)
    {
        target = v;
    }

    public void RightDown()
    {
        rightPressed = true;
        UpdateAxisValue(1f);        
    }

    public void RightUp()
    {
        rightPressed = false;
        UpdateAxisValue(0f);
    }

    public void LeftDown()
    {
        leftPressed = true;
        UpdateAxisValue(-1f);
    }

    public void LeftUp()
    {
        leftPressed = false;
        UpdateAxisValue(0f);
    }

    public void FakePressLeft()
    {
        RightUp();
        LeftDown();
    }

    public void FakePressRight()
    {
        LeftUp();
        RightDown();
    }

    private void Update()
    {                
        value = Mathf.Lerp(value, target, velocity * Time.deltaTime);
        value = (Mathf.Abs(value) < 0.1f) ? 0 : value;

        if (leftPressed && rightPressed)        
            value = 0;
    }

}
