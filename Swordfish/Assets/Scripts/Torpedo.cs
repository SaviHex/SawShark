using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public Transform shark;
    private Rigidbody2D rb2d;
    //public float maxSpeed = 70f;
    //public float minSpeed = 50f;
    public float speed;
    public Drops explosion;    

    void Start()
    {
        shark = GameObject.FindGameObjectWithTag("Shark").GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        explosion = GetComponent<Drops>();        
    }

    void Update()
    {
        var dir = shark.position - transform.position;
        rb2d.velocity = Vector3.Normalize(dir) * speed;
        transform.up = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Spawner") && !collision.CompareTag("Particle"))
        {            
            explosion.DropLoot();            
            Destroy(gameObject);
        }
    }
}
