using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 气泡的 SpriteRenderer
    private float fadeSpeed = 0.3f;         // 透明度变化速度
    private bool isFadingIn = true;      // 是否正在变亮
    private float randomInitNumber;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        randomInitNumber = UnityEngine.Random.Range(0, 255);
        Debug.Log(randomInitNumber);
        var color = spriteRenderer.color;
        color.a = (float)randomInitNumber/255;
        spriteRenderer.color = color;
    }

    void Update()
    {
        // 获取当前颜色
        Color color = spriteRenderer.color;

        // 透明度渐变逻辑
        if (isFadingIn)
        {
            color.a += fadeSpeed * Time.deltaTime; // 增加 Alpha 值
            if (color.a >= 1f)
            {
                color.a = 1f; // 限制到最大值
                isFadingIn = false; // 切换为变暗
            }
        }
        else
        {
            color.a -= fadeSpeed * Time.deltaTime; // 减少 Alpha 值
            if (color.a <= 0f)
            {
                color.a = 0f; // 限制到最小值
                isFadingIn = true; // 切换为变亮
            }
        }

        // 应用新颜色
        spriteRenderer.color = color;
    }
}
