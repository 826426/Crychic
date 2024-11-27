using System.Collections;
using UnityEngine;
public enum CurState
{
    Normal,
    Fall,
    Jump,
    Climb,
    Dash,
    Slide
}
public class Player : MonoBehaviour
{
    public CurState curState;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public Vector2 box1Size;
    public Vector2 box2Size;
    public Vector2 box3Size;
    public Vector2 dashDir;
    public float dashCount;
    public int faceDir;//角色面朝方向
    public float jumpMax;//最大跳跃高度
    public float jumpMin;//最小跳跃高度
    public float climbSpeed;//下滑速度同时也是攀爬速度
    public float jumpSpeed;
    public bool isCanController = true;
    public bool isJumping;
    public bool isClimbing;
    public bool isCanMove;
    public bool isDashing;
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
            if (leftBox.Length > 0 || rightBox.Length > 0)
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
    public PlayerNormalState normalState;
    public PlayerJumpState jumpState;
    public PlayerClimbState climbState;
    public PlayerDashState dashState;
    public PlayerFallState fallState;
    public PlayerSlideState slideState;
    public Animator anim;
    public Rigidbody2D rb;
    public float startJumpPos;
    private int playerLayer;
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
        faceDir = -1;
        stateMachine = new StateMachine();
        normalState = new PlayerNormalState(this, stateMachine, "Normal");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        climbState = new PlayerClimbState(this, stateMachine, "Climb");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        fallState = new PlayerFallState(this, stateMachine, "Jump");
        slideState = new PlayerSlideState(this, stateMachine, "Slide");
    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        if (!IsOnGround)
        {
            stateMachine.InitializeState(fallState);
        }
        else
        {
            stateMachine.InitializeState(normalState);
        }
        SetVelZero();
        isCanMove = true;
        playerLayer = LayerMask.GetMask("Player");
        playerLayer = ~playerLayer;
        input = InputManager.Instance;
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        downBox = Physics2D.BoxCast(transform.position, new Vector2(0.75f, 1.15f), 0, Vector2.down, 0.85f, playerLayer);
        rightBox = Physics2D.BoxCastAll(transform.position, new Vector2(0.6f, 2.8f), 0, Vector2.right, 0.3f, playerLayer);
        leftBox = Physics2D.BoxCastAll(transform.position, new Vector2(0.4f, 2.8f), 0, Vector2.left, 0.3f, playerLayer);
        if (HorizontalBox != null && HorizontalBox.Length > 0)
        {
            Debug.DrawLine(transform.position, HorizontalBox[0].point, Color.red);
        }
        UpdateDashCount();
        UpdateFaceDir();
    }
    public int GetWallDir()
    {
        return rightBox.Length > 0 ? 1 : -1;
    }

    private void UpdateDashCount()
    {
        if (IsOnGround && curState != CurState.Dash)
        {
            dashCount = 1;
        }
    }

    private void UpdateFaceDir()
    {
        if (input.moveDir > 0 && faceDir < 0)
        {
            transform.Rotate(0, 180, 0);
            faceDir = 1;
        }
        else if (input.moveDir < 0 && faceDir > 0)
        {
            transform.Rotate(0, 180, 0);
            faceDir = -1;
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
        rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }
    public void Jump()
    {
        stateMachine.ChangeState(jumpState);
        startJumpPos = transform.position.y;
        StartCoroutine(StartJump(Vector2.zero, Vector2.zero));
    }
    public void Jump(Vector2 vel, Vector2 maxVel)
    {
        stateMachine.ChangeState(jumpState);
        startJumpPos = transform.position.y;
        StartCoroutine(StartJump(vel, maxVel));
    }
    IEnumerator StartJump(Vector2 vel, Vector2 maxVel)
    {
        float dis = 0;
        float curJumpMax = jumpMax * (vel.y + jumpSpeed) / jumpSpeed;
        float curJumpMin = jumpMin * (vel.y + jumpSpeed) / jumpSpeed;
        float curJumpSpeed = jumpSpeed + vel.y;
        while (curState == CurState.Jump && dis <= curJumpMin && velocity.y <= curJumpSpeed)
        {
            if (vel.x != 0 && Mathf.Abs(velocity.x) < maxVel.x)
            {
                isCanMove = false;
                velocity.x += vel.x;
                if (Mathf.Abs(velocity.x) > (maxVel.x))
                {
                    velocity.x = maxVel.x * faceDir;
                }
            }
            dis = transform.position.y - startJumpPos;
            if (vel.y <= 0)
            {
                velocity.y += 300 * Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        velocity.y = curJumpSpeed;
        isCanMove = true;
        while (curState == CurState.Jump && dis < curJumpMax && input.JumpKey)
        {
            if(curState == CurState.Jump && Input.GetKeyDown(input.jump) && IsCanClimb && curState != CurState.Climb)
            {
                SetVelZero();
                Jump(new Vector2(5 * -GetWallDir(), 0), new Vector2(20, 0));
                yield break;
            }
            dis = transform.position.y - startJumpPos;
            velocity.y = curJumpSpeed;
            yield return new WaitForFixedUpdate();
        }
        while (curState == CurState.Jump && velocity.y > 0)
        {
            if (curState == CurState.Jump && Input.GetKeyDown(input.jump) && IsCanClimb && curState != CurState.Climb)
            {
                SetVelZero();
                Jump(new Vector2(5 * -GetWallDir(), 0), new Vector2(20, 0));
                yield break;
            }
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
        stateMachine.ChangeState(fallState);
    }
    public void Dash()
    {
        velocity = Vector3.zero;
        dashCount--;
        StopAllCoroutines();
        StartCoroutine("StartDash");
    }
    public void SetVelZero()
    {
        velocity = Vector3.zero;
    }
    public void AuotoJumpClimb()
    {
        StartCoroutine("AutoJump");
    }
    public bool CheckIsSlide()
    {
        return (input.moveDir > 0 && rightBox.Length > 0) || (input.moveDir < 0 && leftBox.Length > 0);
    }
    public bool CheckCanMove()
    {
        return (isCanMove && curState != CurState.Dash && curState != CurState.Climb && curState != CurState.Slide && isCanController);
    }
    IEnumerator StartDash()
    {
        float verticalDir;
        float horizontalDir;
        if (input.v < 0 && IsOnGround)
        {
            verticalDir = 0;
        }
        else
        {
            verticalDir = input.v;
        }
        if(CheckIsSlide())
        {
            horizontalDir = 0;
        }
        else
        {
            horizontalDir = input.moveDir;
        }
        dashDir = new Vector2(horizontalDir, verticalDir).normalized;
        if (dashDir == Vector2.zero && !(input.v < 0))
        {
            dashDir = new Vector2(faceDir, 0).normalized;
        }
        int i = 0;
        isCanController = false;
        while (i < 9)
        {
            velocity = dashDir * 30f;
            i++;
            yield return new WaitForFixedUpdate();
        }
        isCanController = true;
        if (IsOnGround)
        {
            stateMachine.ChangeState(normalState);
        }
        else if(!IsOnGround)
        {
            stateMachine.ChangeState(fallState);
        }
    }
    IEnumerator AutoJump()
    {
        var posY = Mathf.Ceil(transform.position.y);
        isCanController = false;
        velocity = Vector3.zero;
        while (posY + 1f - transform.position.y > 0)
        {
            velocity.y = jumpSpeed;
            velocity.x = faceDir * 5f;
            yield return null;
        }
        velocity = Vector2.zero;
        isCanController = true;
        stateMachine.ChangeState(fallState);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(target1.transform.position, box1Size);
        Gizmos.DrawWireCube(target2.transform.position, box2Size);
        Gizmos.DrawWireCube(target3.transform.position, box3Size);
    }
}