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

            // A Hazard can be a squid ink so I need to check the tag.
            if (tag == "Torpedo")
            {
                LastPointStats.Instance.Torpedo += 1; // Torpedo Counter
                Debug.Log("Torpedo++");
            }
            
            player.DrainStamina(damage);
        }
        else
        {
            if(collision.tag != "Torpedo")
            {
                var playerTrans = GameObject.FindGameObjectWithTag("Shark").GetComponent<Transform>();

                // If the torpedo kills something on screen or at least near the player.
                // Far away and unintentional kills won't count.
                if (Vector3.Distance(transform.position, playerTrans.position) < 160f)
                {
                    LastPointStats.Instance.TorpedoKill += 1;
                    Debug.Log("TorpedoKill++");
                }
            }
        }
    }
}
