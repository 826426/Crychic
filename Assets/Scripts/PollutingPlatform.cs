using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 污染的地板
/// 当Player踏上之后开始抖动并被污染
/// </summary>
public class PollutingPlatform : MonoBehaviour
{
    [Header("抖动")]
    [Label("抖动时间")]
    public float shakeSeconds = 0.5f;
    [Label("抖动横向幅度")]
    public float shakeAmountX = 0.5f;
    [Label("抖动纵向幅度")]
    public float shakeAmountY = 0.5f;
    [Label("抖动频率")]
    public float shakeFrequency = 0.5f;
    
    [Header("污染源")]
    [Label("污染物体")]
    public GameObject pollutionObject;
    [Label("污染上升高度")] 
    public float pollutionRiseHeight = 1f;
    [Label("污染时间")]
    public float pollutionRiseSeconds = 1f;
    
    private bool isPlayerOnPlatform = false;
    private bool isShaking;

    private void OnEnable()
    {
        //确保一开始污染源是Inactive
        pollutionObject.SetActive(false);
        //确保一开始isShaking是false，否则重新启用时会出错
        isShaking = false;
    }

    private void Update()
    {
        if (isPlayerOnPlatform && !isShaking)
        {
            StartCoroutine(ShakeAndPollute());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }

    private IEnumerator ShakeAndPollute()
    {
        isShaking = true;
        Vector2 shakePosition = transform.position;
        float shakeDuration = shakeSeconds;
        while (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            transform.position = shakePosition + new Vector2(Mathf.Sin(Time.time * shakeFrequency) * shakeAmountX, Mathf.Cos(Time.time * shakeFrequency) * shakeAmountY);
            yield return null;
        }
        transform.position = shakePosition;
        
        pollutionObject.SetActive(true);
        float pollutionRiseDuration = pollutionRiseSeconds;
        while (pollutionRiseDuration > 0)
        {
            pollutionRiseDuration -= Time.deltaTime;
            pollutionObject.transform.position = new Vector2(pollutionObject.transform.position.x, pollutionObject.transform.position.y +
                (pollutionRiseHeight / pollutionRiseSeconds) * Time.deltaTime);
            yield return null;
        }
        
        //这个操作只进行一次，污染源爆发之后就禁用此脚本
        this.enabled = false;
    }

}
