using UnityEngine;
using UnityEngine.EventSystems;

public class Test: MonoBehaviour
{
    public float maxMoveSpeed;
    public Vector3 velocity;
    public float horizontal;
    public Rigidbody2D rb;
    public float moveH;
    public float introDir;
    public int jumpFrame;
    public Vector2 moveSpeed;
    public float jumpForce;
    public bool IsOnGround
    {
        get
        {
            return downBox.collider != null ? true : false;
        }
    }
    public bool IsCanClimb
    {
        get
        {
            return leftBox.Length > 0 || rightBox.Length > 0;
        }
    }
    public int playerLayer;
    public RaycastHit2D downBox;
    public RaycastHit2D[] leftBox;
    public RaycastHit2D[] rightBox;
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        playerLayer = ~playerLayer;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        downBox = Physics2D.BoxCast(transform.position - new Vector3(0,0.4f,0), new Vector2(1, 0.25f), 0, Vector2.down, 0.05f, playerLayer);
        leftBox = Physics2D.BoxCastAll(transform.position - new Vector3(0, 0.4f, 0), new Vector2(1, 1.2f), 0, Vector2.left, 0.05f, playerLayer);
        rightBox= Physics2D.BoxCastAll(transform.position - new Vector3(0, 0.4f, 0), new Vector2(1, 1.2f), 0, Vector2.right, 0.05f, playerLayer);
        if (IsOnGround)
        {
            velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.Space) && !IsOnGround)
        {
            jumpFrame = 2;
        }
        if (JumpKeyDown() && IsOnGround)
        {
            velocity.y += jumpForce;
            jumpFrame = 2;
        }
        if (Input.GetKeyDown(KeyCode.A) && horizontal >= 0)
        {
            horizontal = -1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
            }
            else
            {
                horizontal = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && horizontal <= 0)
        {
            horizontal = 1;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if(jumpFrame > 0)
        {
            jumpFrame--;
        }
        //下落状态
        if (!IsOnGround)
        {
            velocity.y -= 150f * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -25, velocity.y);
        }
        //减速状态
        if (horizontal == 0 || (horizontal > 0 && velocity.x < 0) || (horizontal < 0 && velocity.x > 0))
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
        if (horizontal > 0)
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
        else if (horizontal < 0)
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
    public bool JumpKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        else if(jumpFrame > 0)
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - new Vector3(0, 0.4f, 0), new Vector2(1, 0.25f));
    }
}