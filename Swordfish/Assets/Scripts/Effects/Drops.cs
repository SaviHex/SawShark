using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drops : MonoBehaviour
{    
    public GameObject[] dropsObjs;    
    public int minDrops = 1;
    public int maxDrops = 2;
    public int dissipation = 0;
    public bool randomRotation;

    public void DropLoot()
    {        
        Vector3 pos;
        Quaternion rot = Quaternion.identity;
        int index;
        int dropCount = Random.Range(minDrops, maxDrops);

        for (int i = 0; i < dropCount; i++)
        {
            index = Random.Range(0, dropsObjs.Length);            
            pos = transform.position + ((Vector3)Random.insideUnitCircle * dissipation);

            if (randomRotation)
            {
                rot = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            }

            Instantiate(dropsObjs[index], pos, rot);
        }
    }
}
