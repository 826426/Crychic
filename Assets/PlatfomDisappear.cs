using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappear : MonoBehaviour
{
    [Header("延迟")]
    public float disappearDelay = 0.5f;
    public float recoveryDelay = 0.5f;
    [Header("透明度")]
    public float disappearAlpha = 0.2f;
    public float recoveryAlpha = 1f;
    [Header("抖动参数")]
    public float shakeAmount = 0.1f;
    public float shakeFrequency = 10f;//用正弦模拟抖动先
    
    private SpriteRenderer platformSpriteRenderer;
    private BoxCollider2D platformCollider2D;
    
    private bool playerOnPlatform = false;
    private bool isDisappearing = false;
    
    private Vector2 disappearPos;
    
    private MovingPlatform m_MovingPlatform;
    
    private void Start()
    {
        platformSpriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider2D = GetComponent<BoxCollider2D>();
        m_MovingPlatform = GetComponent<MovingPlatform>();
    }
    
    private void Update()
    {
        if (playerOnPlatform && !isDisappearing)
        {
            StartCoroutine(DisappearAndRecover());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    private IEnumerator DisappearAndRecover()
    {
        isDisappearing = true;

        if (m_MovingPlatform != null)
        {
            m_MovingPlatform.enabled = false;
        }
        
        //抖动
        disappearPos = transform.position;
        float shakeDuration = disappearDelay;
        while (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            float offsetX = Mathf.Sin(Time.time * shakeFrequency) * shakeAmount;
            transform.position = disappearPos + new Vector2(offsetX, 0);
            yield return null;
        }
        transform.position = disappearPos;
        
        //透明度降低，禁用碰撞
        yield return new WaitForSeconds(disappearDelay);
        
        platformSpriteRenderer.color = new Color(platformSpriteRenderer.color.r, platformSpriteRenderer.color.g, platformSpriteRenderer.color.b, disappearAlpha);
        platformCollider2D.enabled = false;
        
        //过段时间恢复
        yield return new WaitForSeconds(recoveryDelay);
        
        platformSpriteRenderer.color = new Color(platformSpriteRenderer.color.r, platformSpriteRenderer.color.g, platformSpriteRenderer.color.b, recoveryAlpha);
        platformCollider2D.enabled = true;

        if (m_MovingPlatform != null)
        {
            m_MovingPlatform.enabled = true;
        }
        
        isDisappearing = false;
    }
}
