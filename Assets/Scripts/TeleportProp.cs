using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProp : MonoBehaviour
{
    [Label("传送位置")] 
    public Vector2 teleportPosition;
    [Label("传送延时")]
    public float teleportDelaySeconds = 0.5f;
    private float timeNeeds2Teleport;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            timeNeeds2Teleport = teleportDelaySeconds;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(timeNeeds2Teleport >= 0)
            {
                timeNeeds2Teleport -= Time.deltaTime;
            }
            else
            {
                player.transform.position = teleportPosition;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            timeNeeds2Teleport = teleportDelaySeconds;
        }
    }
}
