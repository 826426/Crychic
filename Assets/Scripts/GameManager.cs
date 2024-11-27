using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    private Checkpoint _currentCheckPoint;

    private float deadDuringTime = 1f;

    public Checkpoint currentCheckPoint
    {
        get { return _currentCheckPoint; }
        set { _currentCheckPoint = value; }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);

        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        } 
    }

    public void PlayerBeAttacked()
    {
        Debug.Log("Player is dead");
        PlayDeadAnimation();
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        StartCoroutine(IE_ResetPlayer(deadDuringTime));
    }

    IEnumerator IE_ResetPlayer(float second)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(second);
        Time.timeScale = 1;
        player.transform.position = currentCheckPoint.transform.position;
    }

    public void PlayDeadAnimation()
    {

    }

    public void Finish()
    {
        Debug.Log("finish");
    }
}
