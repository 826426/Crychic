using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test: MonoBehaviour
{
    public float maxMoveSpeed;
    public Vector3 velocity;
    public float h;
    public float v;
    public Rigidbody2D rb;
    public float moveH;
    public float introDir;
    public int jumpFrame;
    public Vector2 moveSpeed;
    public float jumpForce;
    public float climbSpeed;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public Vector2 dashDir;
    public float vertivDir;
    public float jumpSpeed;
    public bool isCanFall;
    public bool isCanControl;
    public float sp = 120;
    public bool IsClimb
    {
        get
        {
            return isCanClimb && Input.GetKey(KeyCode.LeftControl);
        }
    }

    public bool JumpKeyDown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }
            else if (jumpFrame > 0)
            {
                return true;
            }
            return false;
        }
    }
    public bool IsOnGround
    {
        get
        {
            return downBox.collider != null ? true : false;
        }
    }
    public bool isCanClimb;
    public int playerLayer;
    public RaycastHit2D downBox;
    public RaycastHit2D[] leftBox;
    public RaycastHit2D[] rightBox;
    public RaycastHit2D[] HorizontalBox
    {
        get
        {
            if(leftBox.Length > 0)
            {
                return leftBox;
            }
            else if(rightBox.Length > 0)
            {
                return rightBox;
            }
            return null;
        }
    }
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        playerLayer = ~playerLayer;
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (IsOnGround && v < 0)
        {
            vertivDir = 0;
        }
        else
        {
            vertivDir = v;
        }
        dashDir = new Vector2(h, vertivDir).normalized;
        if(dashDir == Vector2.zero)
        {
            dashDir = Vector2.zero;//后面更换为角色面朝方向
        }
        downBox = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.8f), 0, Vector2.down, 0.1f, playerLayer);
        leftBox = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.8f), 0, Vector2.left, 0.3f, playerLayer);
        rightBox= Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.8f), 0, Vector2.right, 0.3f, playerLayer);
        if (!IsClimb)
        {
            sp = 120;
        }
        if (HorizontalBox != null && HorizontalBox.Length > 0)
        {
            Debug.DrawLine(transform.position, HorizontalBox[0].point, Color.red);
        }
        if (leftBox.Length > 0 || rightBox.Length > 0)
        {
            isCanClimb = true;
        }
        else
        {
            isCanClimb = false;
        }
        if (IsClimb)
        {
            velocity.x = 0;
            if(v > 0 && transform.position.y - HorizontalBox[0].point.y > 0.9f)
            {
                StartCoroutine("ClambAutoJump");
                return;
            }
            if(v <= 0 && transform.position.y - HorizontalBox[0].point.y > 0.7f || !Input.GetKey(KeyCode.LeftControl) || sp <= 0)
            {
                velocity.y -= climbSpeed * Time.deltaTime;
            }
            else if(transform.position.y - HorizontalBox[0].point.y <= 0.7f || Input.GetKey(KeyCode.LeftControl))
            {
                velocity.y = v * climbSpeed;
            }
            if (JumpKeyDown)
            {
                velocity.y += jumpForce;
                jumpFrame = 2;
                sp -= 30f;
            }
        }
        if (IsOnGround)
        {
            velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.Space) && !IsOnGround)
        {
            jumpFrame = 2;
        }
        if (JumpKeyDown && IsOnGround)
        {
            velocity.y += jumpForce;
            jumpFrame = 2;
        }
        if(JumpKeyDown && isCanClimb)
        {
            //空中靠近墙壁，会有个小蹬墙跳
        }
        if (Input.GetKeyDown(KeyCode.A) && h >= 0)
        {
            h = -1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                h = 1;
            }
            else
            {
                h = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && h <= 0)
        {
            h = 1;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if(jumpFrame > 0)
        {
            jumpFrame--;
        }
        //冲刺状态

        //爬墙状态
        if (IsClimb)
        {
            sp -= 1f;
        }
        //下落状态
        if (!IsOnGround && !IsClimb)
        {
            velocity.y -= 150f * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -25, velocity.y);
        }
        //减速状态
        if (h == 0 || (h > 0 && velocity.x < 0) || (h < 0 && velocity.x > 0))
        {
            introDir = velocity.x > 0 ? 1 : -1;
            moveH = Mathf.Abs(velocity.x);
            if (IsOnGround)
            {
                moveH -= maxMoveSpeed / 3;
            }
            else
            {
                moveH -= maxMoveSpeed / 6;
            }
            if(moveH < 0.01f)
            {
                moveH = 0;
            }
            velocity.x = moveH * introDir;
        }
        //加速状态
        if (h > 0)
        {
            if (IsOnGround)
            {
                velocity.x += maxMoveSpeed / 6f;
            }
            else
            {
                velocity.x += maxMoveSpeed / 15f;
            }
            if (velocity.x > maxMoveSpeed)
            {
                velocity.x = maxMoveSpeed;
            }
        }
        else if (h < 0)
        {
            if (IsOnGround)
            {
                velocity.x += -maxMoveSpeed / 6f;
            }
            else
            {
                velocity.x += -maxMoveSpeed / 12f;
            }
            if (velocity.x < -maxMoveSpeed)
            {
                velocity.x = -maxMoveSpeed;
            }
        }
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
    IEnumerator ClambAutoJump()
    {
        var posY = Mathf.Ceil(transform.position.y);
        velocity = Vector2.zero;
        while(posY + 1f > transform.position.y)
        {
            velocity.y = jumpSpeed;
            velocity.x = h * 5f;
            yield return null;
        }
        velocity = Vector2.zero;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(target1.transform.position, new Vector2(0.5f, 0.8f));
        Gizmos.DrawWireCube(target2.transform.position, new Vector2(0.5f, 0.8f));
        Gizmos.DrawWireCube(target3.transform.position, new Vector2(0.5f, 0.8f));
    }
}