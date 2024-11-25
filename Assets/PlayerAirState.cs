using UnityEngine;

public class PlayerAirState : State
{
    private float introDir;
    private float moveH;
    public PlayerAirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.velocity.y -= 150f * Time.fixedDeltaTime;
        player.velocity.y = Mathf.Clamp(player.velocity.y, -25, player.velocity.y);
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
        if (player.IsOnGround)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}