using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("参数")]
    [Label("左端点")]
    public GameObject leftPoint;
    private float leftX;
    [Label("右端点")]
    public GameObject rightPoint;
    private float rightX;
    [Label("速度")] 
    public float moveSpeed = 2f;
    [Label("是否停滞")] 
    public bool canWait;
    [Label("停滞时间")] 
    public float waitTime = 1f;

    private float targetX;
    private bool isWaiting;
    
    private void Start()
    {
        leftX = leftPoint.transform.position.x;
        rightX = rightPoint.transform.position.x;
        leftPoint.GetComponent<SpriteRenderer>().enabled = false;
        rightPoint.GetComponent<SpriteRenderer>().enabled = false;
        targetX = leftX;
    }
    
    private void Update()
    {
        if(isWaiting)
            return;
        
        float newPosX = Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * Time.deltaTime);
        transform.position = new Vector2(newPosX, transform.position.y);

        if (math.abs(transform.position.x - targetX) < moveSpeed * Time.deltaTime)
        {
            StartCoroutine(SwitchTargetAfterWait());
        }
    }

    private System.Collections.IEnumerator SwitchTargetAfterWait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        
        targetX = targetX == leftX ? rightX : leftX;
        isWaiting = false;
        
    }
}
