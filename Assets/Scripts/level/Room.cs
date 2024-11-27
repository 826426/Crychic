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
    private Checkpoint checkpoint;

    private void Awake()
    {
        confiner = GetComponentInChildren<CinemachineConfiner>();
        checkpoint = GetComponentInChildren<Checkpoint>();

        confiner.gameObject.SetActive(false);

        OnTriggerEnterEvent.AddListener(() => { confiner.gameObject.SetActive(true); });
        OnTriggerEnterEvent.AddListener(() => { checkpoint.gameObject.SetActive(true); });
        OnTriggerExitEvent.AddListener(() => { confiner.gameObject.SetActive(false); });
        OnTriggerExitEvent.AddListener(() => { checkpoint.gameObject.SetActive(false); });
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
