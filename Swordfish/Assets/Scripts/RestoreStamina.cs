using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RestoreStamina : MonoBehaviour
{
    public int restoreAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shark"))
        {            
            collision.GetComponentInParent<Player>().RestoreStamina(restoreAmount);
            Destroy(gameObject);
        }
    }    

}
