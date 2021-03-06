﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetPlayer : MonoBehaviour
{    

    [Header("Movement")]
    public float speed = 100; // Swiming speed.
    public float rotationSpeed = 500; // Turning speed.      
    
    [Header("Swing")]
    // The collider that you make contact when attacking.
    public Collider2D sawTipCollider;
    public bool isAttacking;
    // For how long the player has been swinging.
    public float swingTime;
    // After how many seconds swinging will it have enough power to deal damage.
    public float swingAttackThreshold = 0.5f;

    [Header("Input")]
    // Input object for mobile devices.
    public TouchControls inputAxis;
    // How much input does it need to start the swinging effect.
    public float inputThreshold = 0.2f;
    // Left/Right key's input.
    public float inputX;

    [Header("Trail Effect")]
    // After how many seconds will it start erasing the trail.
    public float trailEraseDelay = 0.5f;
    // Color of the swing trail effect when it attacks.     
    public Color attackColor;
    // Color of the swing trail effect when it is not attacking. 
    public Color trailColor;

    private Transform t;
    private ConstantForce2D cf2d;
    private TrailRenderer tr; // Child's component.
    private Animator anim;

    void Start()
    {
        // Components
        t = GetComponent<Transform>();
        cf2d = GetComponent<ConstantForce2D>();
        tr = GetComponentInChildren<TrailRenderer>();
        sawTipCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();        

        // Setup
        cf2d.relativeForce = new Vector2(0f, speed);
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        tr.sortingLayerID = rend.sortingLayerID;
        tr.sortingOrder = rend.sortingOrder;        

        
        inputX = 0;   
    }

    void Update()
    {
        #region Input and Swing
        // Gets player input        
        inputX = inputAxis.Value;        

        // Checks if there is enough input to start the trail/attack effect.
        if (Mathf.Abs(inputX) > inputThreshold)
        {
            swingTime += Time.deltaTime; // Updates the swing counter.            
            tr.time = 0.5f; // Bring the trail effect back. 
        }
        else
        {
            swingTime = 0; // Resets the swing counter.
            StartCoroutine(RemoveTrailPoints()); // Start removing the trail effect.    
        }

        // Changes the color whether it is been swinging for long enough.
        Color newColor;
        isAttacking = swingTime > swingAttackThreshold;
        if (isAttacking)
        {
            newColor = attackColor;
            sawTipCollider.enabled = true;
            //stamina = (stamina > 0) ? stamina - Time.deltaTime / drainSpeed : 0; // Drain stamina until it hits zero.
        }
        else
        {
            newColor = trailColor;
            sawTipCollider.enabled = false;
        }

        newColor.a = 0.75f;
        tr.startColor = newColor;
        newColor.a = 0;
        tr.endColor = newColor;

        #endregion                    
    }

    // For physics...
    void FixedUpdate()
    {
        // Changes player direction based on input.
        // Left  -> Counter Clockwise.
        // Right -> Clockwise.
        t.Rotate(new Vector3(0f, 0f, -1 * inputX * rotationSpeed * Time.fixedDeltaTime));
    }

    // Gradually shortens the trail until it disapears.
    IEnumerator RemoveTrailPoints()
    {
        float v = -1f * (1 / trailEraseDelay);
        while (tr.time > 0 && inputX == 0)
        {
            tr.time = Mathf.SmoothDamp(tr.time, 0, ref v, trailEraseDelay);
            yield return new WaitForSeconds(0);
        }
    }     
}
