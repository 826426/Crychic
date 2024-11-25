using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player: MonoBehaviour
{
    public int FaceDir//角色面朝方向
    {
        get
        {
            return input.moveDir > 0 ? 1 : -1;
        }
    }
    public float jumpMax;//最大跳跃高度
    public float jumpMin;//最小跳跃高度
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public float slideSpeed;//下滑速度同时也是攀爬速度
    public float jumpSpeed;
    public bool isJumping;
    public bool isClimbing;
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
            if(leftBox.Length > 0 || rightBox.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
    public Vector3 velocity;//当前移动速度
    public float maxMoveSpeed;
    public InputManager input;
    public StateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerAirState airState;
    public PlayerSlideState slideState;
    public PlayerClimbState climbState;
    public Rigidbody2D rb;
    public float startJumpPos;
    public int playerLayer;
    public RaycastHit2D downBox;
    public RaycastHit2D[] leftBox;
    public RaycastHit2D[] rightBox;
    public RaycastHit2D[] HorizontalBox
    {
        get
        {
            if (leftBox.Length > 0)
            {
                return leftBox;
            }
            else if (rightBox.Length > 0)
            {
                return rightBox;
            }
            return null;
        }
    }
    private void Awake()
    {
        stateMachine = new StateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        slideState = new PlayerSlideState(this, stateMachine, "Climb");
        climbState = new PlayerClimbState(this, stateMachine, "Climb");
    }
    private void Start()
    {
        stateMachine.InitializeState(idleState);
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        playerLayer = ~playerLayer;
        input = InputManager.Instance;
    }
    private void Update()
    {
        stateMachine.currentState.Update();
        downBox = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.8f), 0, Vector2.down, 0.1f, playerLayer);
        leftBox = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.8f), 0, Vector2.left, 0.3f, playerLayer);
        rightBox = Physics2D.BoxCastAll(transform.position, new Vector2(0.5f, 0.8f), 0, Vector2.right, 0.3f, playerLayer);
        if (HorizontalBox != null && HorizontalBox.Length > 0)
        {
            Debug.DrawLine(transform.position, HorizontalBox[0].point, Color.red);
        }
    }
    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
    public void Jump()
    {
        startJumpPos = transform.position.y;
        isJumping = true;
        StartCoroutine(StartJump(Vector2.zero, Vector2.zero));
    }
    public void Jump(Vector2 vel, Vector2 maxVel)
    {
        startJumpPos = transform.position.y;
        isJumping = true;
        StartCoroutine(StartJump(vel, maxVel));
    }
    public void SetVelYZero()
    {
        velocity = new Vector2(velocity.x, 0);
    }
    IEnumerator StartJump(Vector2 vel, Vector2 maxVel)
    {
        float dis = 0;
        float curJumpMax = jumpMax * (vel.y + jumpSpeed) / jumpSpeed;
        float curJumpMin = jumpMin * (vel.y + jumpSpeed) / jumpSpeed;
        float curJumpSpeed = jumpSpeed + vel.y;
        while (dis <= curJumpMin && velocity.y <= curJumpSpeed)
        {
            dis = transform.position.y - startJumpPos;
            if (vel.y <= 0)
            {
                velocity.y += 900 * Time.fixedDeltaTime;
            }
            Debug.Log(dis);
            yield return new WaitForFixedUpdate();
        }
        velocity.y = curJumpSpeed;
        while (dis < curJumpMax && input.JumpKey)
        {
            dis = transform.position.y - startJumpPos;
            velocity.y = curJumpSpeed;
            yield return new WaitForFixedUpdate();
        }
        while (velocity.y > 0)
        {
            if (dis > jumpMax)
            {
                velocity.y -= 100 * Time.fixedDeltaTime;
            }
            else
            {
                velocity.y -= 200 * Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        velocity.y = 0;
        yield return .1f;
        isJumping = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(target1.transform.position, new Vector2(0.5f, 0.8f));
        Gizmos.DrawWireCube(target2.transform.position, new Vector2(0.5f, 0.8f));
        Gizmos.DrawWireCube(target3.transform.position, new Vector2(0.5f, 0.8f));
    }
}