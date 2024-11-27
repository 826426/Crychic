using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain; // 绑定的虚拟相机
    private Room[] rooms;

    private float blendTime;
    private bool isFirst = true;

    private void Awake()
    {
        rooms = GetComponentsInChildren<Room>();
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].OnTriggerEnterEvent.AddListener(Blend);
        }
        blendTime = cinemachineBrain.m_DefaultBlend.BlendTime;
    }

    public void Blend()
    {
        if (!isFirst)
        {
            StartCoroutine(StopMove());
        } 
        else
        {
            isFirst = false;
        }

    }

    IEnumerator StopMove()
    {
        Time.timeScale = 0;
        //yield return null;
        yield return new WaitForSecondsRealtime(blendTime);
        Time.timeScale = 1;
    }
}
