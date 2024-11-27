using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerBeAttacked();
        }
    }
}
