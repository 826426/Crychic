using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Label("旋转速度(度/秒)")]
    public float rotateSpeed = 50f;
    [Label("是否顺时针")]
    public bool isClockWise = true;
    private int rotateDirection => isClockWise ? 1 : -1;

    private void Update()
    {
        this.transform.Rotate(0,0,rotateDirection * rotateSpeed * Time.deltaTime);
    }
}
