using UnityEngine;

public class GoldfishMovement : MonoBehaviour
{
    public float speed = 2f;          // 金鱼移动速度
    public GameObject leftPosGO;
    public GameObject rightPosGO;

    private Vector3 leftPos;           // 左侧终点位置
    private Vector3 rightPos;          // 右侧终点位置

    private bool movingRight = true;  // 当前是否朝右移动

    private void Start()
    {
        leftPosGO.GetComponent<SpriteRenderer>().enabled = false;
        rightPosGO.GetComponent<SpriteRenderer>().enabled = false;
        leftPos = leftPosGO.transform.position;
        rightPos = rightPosGO.transform.position;

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // 根据移动方向决定金鱼的位置
        Vector3 targetPos = movingRight ? rightPos : leftPos;

        // 移动到目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // 如果金鱼到达目标位置，就改变方向
        if (transform.position == targetPos)
        {
            movingRight = !movingRight;
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        // 改变金鱼的朝向
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
