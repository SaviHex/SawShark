using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitManagerAI manager;
    public Transform goal;
    public bool inDanger;
    public float minSpeed;
    public float maxSpeed;

    private ConstantForce2D cf2d;
    public float acceleration = 10f;
    private float speed = 1f;

    void Start()
    {
        cf2d = GetComponent<ConstantForce2D>();
        speed = minSpeed;
        goal = manager.GetGoal();
    }

    void Update()
    {
        transform.up = Vector3.Slerp(transform.up, transform.position - goal.position, 4 * Time.deltaTime);
       
        cf2d.relativeForce = new Vector2(0f, -speed);
    }

    public void SpeedUp()
    {
        inDanger = true;
        StartCoroutine(SpeedUpCoroutine());
    }

    public void SlowDown()
    {
        inDanger = false;
        StartCoroutine(SlowDownCoroutine());
    }

    private IEnumerator SpeedUpCoroutine()
    {
        while (inDanger && speed < maxSpeed)
        {
            speed = Mathf.Lerp(speed, maxSpeed, acceleration * Time.deltaTime);

            yield return new WaitForSeconds(0f);
        }
    }

    private IEnumerator SlowDownCoroutine()
    {
        while (!inDanger && speed > minSpeed)
        {
            speed = Mathf.Lerp(speed, minSpeed, acceleration * Time.deltaTime);

            yield return new WaitForSeconds(0f);
        }
    }
}
