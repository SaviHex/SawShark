using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spawner : MonoBehaviour
{
    public Collider2D garbageDelimiter;
    public Transform container;
    public GameObject[] prefabs;
    public int minItems = 1;
    public int maxItems = 1;
    public float distance = 10f;
    public float spacing = 1f;
    public float interval = 1f;
    public List<string> ignoreTags;

    private void Start()
    {
        garbageDelimiter = GetComponent<Collider2D>();

        StartCoroutine(SpawnTimer());
    }

    private void Spawn()
    {
        //int objectCount = Random.Range(minItems, maxItems + 1);
        int index = Random.Range(0, prefabs.Length);
        Vector3 spawnPoint = (transform.position) + (Vector3)RandomPointInCircunference(distance);

        Instantiate(prefabs[index], spawnPoint, Quaternion.identity, container);

        Debug.Log("Spawn: Obj: " + prefabs[index].name + " At: " + spawnPoint);
    }

    private Vector2 RandomPointInCircunference(float radius)
    {
        float x, y;

        y = Random.Range(-1f, 1f);

        x = Mathf.Sqrt(1 - (y * y));

        x = (Random.Range(-1f, 1f) < 0) ? -1 * x : x;

        return (new Vector2(x, y) * radius);
    }

    IEnumerator SpawnTimer()
    {
        while (enabled)
        {
            Spawn();
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!ignoreTags.Contains(collision.tag))
        {
            Debug.Log("Clean Up: " + collision.name);
            Destroy(collision.gameObject);
        }        
    }

}
