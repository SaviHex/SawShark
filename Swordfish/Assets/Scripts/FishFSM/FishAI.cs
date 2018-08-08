using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public GameObject shark;

    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float acceleration;
    public bool inDanger;

    private Animator anim;
    private ConstantForce2D cf2d;

    void Start()
    {
        anim = GetComponent<Animator>();
        cf2d = GetComponent<ConstantForce2D>();
        shark = FindShark();
        inDanger = false;
        speed = minSpeed;
    }

    void Update()
    {
        anim.SetFloat("Distance", Vector2.Distance(transform.position, shark.transform.position));
        cf2d.relativeForce = new Vector2(0f, -1 * speed);
    }

    public GameObject FindShark()
    {
        return GameObject.FindGameObjectWithTag("Shark");
    }

    public void StartSpeedUp()
    {
        inDanger = true;
        StartCoroutine(SpeedUp());
    }

    public void StartSlowDown()
    {
        inDanger = false;
        StartCoroutine(SlowDown());
    }

    IEnumerator SpeedUp()
    {
        while (inDanger && speed < maxSpeed)
        {
            speed = Mathf.Lerp(speed, maxSpeed, acceleration * Time.deltaTime);

            yield return new WaitForSeconds(0f);
        }
    }

    IEnumerator SlowDown()
    {
        while (!inDanger && speed > minSpeed)
        {
            speed = Mathf.Lerp(speed, minSpeed, acceleration * Time.deltaTime);

            yield return new WaitForSeconds(0f);
        }
    }
}
