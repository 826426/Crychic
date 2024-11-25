using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    private CinemachineConfiner confiner;
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;

    private void Awake()
    {
        confiner = GetComponentInChildren<CinemachineConfiner>();

        confiner.gameObject.SetActive(false);

        OnTriggerEnterEvent.AddListener(() => { confiner.gameObject.SetActive(true); });
        OnTriggerExitEvent.AddListener(() => { confiner.gameObject.SetActive(false); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnTriggerEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnTriggerExitEvent.Invoke();
        }
    }
}
