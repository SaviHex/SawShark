using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Hazard : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        
        if (player != null && collision.CompareTag("Shark"))
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.ShakeStamina();
            CameraShaker.Instance.ShakeOnce(9f, 10f, .1f, .75f);

            // A Hazard can be a squid ink so I need to check the name.
            // (Boom objects only come from Torpedos)
            if (name.Contains("Boom")) // The name will always be "Boom (number)"
            {
                LastPointStats.Instance.Torpedo += 1; // Torpedo Counter
                Debug.Log("Torpedo++");
            }
            
            player.DrainStamina(damage);
        }        
    }
}
