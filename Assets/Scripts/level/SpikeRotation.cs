using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRotation : MonoBehaviour
{
    public float rotationSpeed = 50f; // 每秒旋转的角度

    void Update()
    {
        // 以自身为中心绕 Z 轴旋转
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
