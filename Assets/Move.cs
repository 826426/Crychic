using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float maxSpeed;
    public float moveSpeed;
    public Rigidbody2D rb;
    public float horizontal;
    public bool isOnGround;
    public float jumpForce;
    public int jumpFram;
    public LayerMask whatIsGround;
    public float dashTime;
    public bool isCanControl;
    public bool isClimb;
    public bool isCanClimb;
    public float wallCheckDistance;
    public float climbSpeed;
    public float groundCheckDistance;
    public float sp = 120;
    public bool JumpKeyDown
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }
            else if (jumpFram > 0)
            {
                return true;
            }
            return false;
        }
    }
    private void Awake()
    {
        isCanControl = true;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isCanClimb = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, whatIsGround);
        isOnGround = rb.velocity.y == 0 && !isClimb;
        if (isCanClimb && Input.GetKey(KeyCode.LeftControl))
        {
            isClimb = true;
            if (rb.velocity.y > 5f)
            {

            }
        }
        if (isClimb)
        {
            if (sp <= 0)
            {
                rb.velocity = new Vector2(0, -1 * climbSpeed);
                if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround) && Input.GetKeyUp(KeyCode.LeftControl))
                {
                    isClimb = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * climbSpeed);
                if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround) && Input.GetKeyUp(KeyCode.LeftControl))
                {
                    isClimb = false;
                }
            }
        }
        if (JumpKeyDown && isOnGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpFram = 0;
        }
        if (Input.GetKey(KeyCode.Space) && !isOnGround)
        {
            jumpFram = 5;
        }
        if (!isClimb)
        {
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
    }
    private void FixedUpdate()
    {
        if (jumpFram > 0)
        {
            jumpFram--;
        }
        if (isClimb)
        {
            sp -= 0.5f;
        }
        if (!isClimb)
        {
            if (horizontal != 0)
            {
                if (moveSpeed < maxSpeed)
                {
                    moveSpeed += maxSpeed / 6;
                }
            }
            else if (horizontal == 0)
            {
                if (moveSpeed != 0)
                {
                    moveSpeed -= maxSpeed / 3;
                }
            }
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDistance, transform.position.y));
    }
}