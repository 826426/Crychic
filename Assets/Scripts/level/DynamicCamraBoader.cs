using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraBoarder : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // 绑定的虚拟相机
    private CinemachineConfiner confiner;          // Confiner 组件引用

    private void Start()
    {
        // 获取虚拟相机上的 Confiner 组件
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
        if (confiner == null)
        {
            Debug.LogError("Cinemachine Confiner not found on the virtual camera!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 触发器必须设置为 "CameraBoundary"
        {
            // 动态更改 Confiner 的边界
            PolygonCollider2D newBounds = this.GetComponent<PolygonCollider2D>();
            if (newBounds != null)
            {
                confiner.m_BoundingShape2D = newBounds;
                confiner.InvalidatePathCache(); // 刷新边界缓存
            }
        }
    }
}
