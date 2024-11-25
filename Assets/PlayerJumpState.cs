using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpState : State
{
    private float introDir;
    private float moveH;
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.isJumping = true;
        player.Jump();
    }

    public override void Exit()
    {
        base.Exit();
        //player.isJumping = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.velocity.x > 0 && player.input.moveDir < 0 || (player.velocity.x < 0 && player.input.moveDir > 0) || (player.input.moveDir == 0
            || (player.IsOnGround && player.input.v < 0) || Mathf.Abs(player.velocity.x) > player.maxMoveSpeed))
        {
            introDir = player.velocity.x > 0 ? 1 : -1;
            moveH = Mathf.Abs(player.velocity.x);
            moveH -= player.maxMoveSpeed / 6;
            if (moveH < 0.01f)
            {
                moveH = 0;
            }
            player.velocity.x = moveH * introDir;
        }
        //加速状态
        if (player.input.moveDir > 0)
        {
            player.velocity.x += player.maxMoveSpeed / 15f;
            if (player.velocity.x > player.maxMoveSpeed)
            {
                player.velocity.x = player.maxMoveSpeed;
            }
        }
        else if (player.input.moveDir < 0)
        {

            player.velocity.x += -player.maxMoveSpeed / 12f;
            if (player.velocity.x < -player.maxMoveSpeed)
            {
                player.velocity.x = -player.maxMoveSpeed;
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }
}