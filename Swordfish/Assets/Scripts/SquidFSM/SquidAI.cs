using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SquidAI : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private GameObject shark;
    public float impulseForce;
    public GameObject inkPrefab;

    public Transform inkA;
    public Transform inkB;
    public Transform inkC;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shark = FindShark();
    }

    void Update()
    {
        anim.SetFloat("distance", Vector2.Distance(transform.position, shark.transform.position));
    }

    public GameObject FindShark()
    {
        return GameObject.FindGameObjectWithTag("Shark");
    }

    public void Impulse()
    {
        rb2d.AddRelativeForce(Vector2.up * impulseForce);
    }

    public void Ink()
    {        
        // Instantiate three ink objects.
        Instantiate(inkPrefab, inkA.position, inkA.rotation);
        Instantiate(inkPrefab, inkB.position, inkB.rotation);
        Instantiate(inkPrefab, inkC.position, inkC.rotation);
    }

}
