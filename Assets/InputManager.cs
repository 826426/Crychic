using UnityEditor.Build;
using UnityEngine;

public class InputManager: MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public Player player;
    public KeyCode leftMoveKey;
    public KeyCode rightMoveKey;
    public KeyCode climb;
    public KeyCode jump;
    public KeyCode dash;
    public bool ClimbKey
    {
        get
        {
            return Input.GetKey(climb);
        }
    }
    public bool ClimbKeyDown
    {
        get
        {
            return Input.GetKeyDown(climb);
        }
    }
    public bool ClimbKeyUp
    {
        get
        {
            return Input.GetKeyUp(climb);
        }
    }
    public bool JumpKeyDown
    {
        get
        {
            if (Input.GetKeyDown(jump))
            {
                return true;
            }
            else if(jumpFarme > 0)
            {
                return true;
            }
            return false;
        }
    }
    public bool JumpKeyUp
    {
        get
        {
            return Input.GetKeyUp(jump);
        }
    }
    public bool JumpKey
    {
        get
        {
            return Input.GetKey(jump);
        }
    }
    public bool DashKey
    {
        get
        {
            return Input.GetKey(dash);
        }
    }
    public bool DashKeyDown
    {
        get
        {
            return Input.GetKeyDown(dash);
        }
    }
    public bool DaashKeyUp
    {
        get
        {
            return Input.GetKeyUp(dash);
        }
    }
    public float v = 0;
    public float h = 0;
    public int moveDir;
    int jumpFarme;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        player = GetComponent<Player>();
        KeyInit();
    }
    private void Update()
    {
        CheckHorizontalMove();
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(jump))
        {
            jumpFarme = 3;
        }
    }
    private void FixedUpdate()
    {
        if(jumpFarme > 0)
        {
            jumpFarme--;
        }
    }
    private void CheckHorizontalMove()
    {
        if (Input.GetKeyDown(leftMoveKey) && h >= 0)
        {
            moveDir = -1;
        }
        else if (Input.GetKeyDown(rightMoveKey) && h <= 0)
        {
            moveDir = 1;
        }
        else if (Input.GetKeyUp(leftMoveKey))
        {
            if (Input.GetKey(rightMoveKey))
            {
                moveDir = 1;
            }
            else
            {
                moveDir = 0;
            }
        }
        else if (Input.GetKeyUp(rightMoveKey))
        {
            if (Input.GetKey(leftMoveKey))
            {
                moveDir = -1;
            }
            else
            {
                moveDir = 0;
            }
        }
    }
    private void KeyInit()
    {
        jump = KeyCode.C;
        dash = KeyCode.X;
        climb = KeyCode.Z;
        leftMoveKey = KeyCode.LeftArrow;
        rightMoveKey = KeyCode.RightArrow;
    }
}