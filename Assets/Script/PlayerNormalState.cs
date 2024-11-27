using UnityEngine;

public class PlayerNormalState : State
{
    private float introDir;
    private float moveH;
    public PlayerNormalState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.curState = CurState.Normal;
        player.velocity.y = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //减速状态
        if (player.velocity.x > 0 && player.input.moveDir < 0 || (player.velocity.x < 0 && player.input.moveDir > 0) || (player.input.moveDir == 0
            || (player.IsOnGround && player.input.v < 0) || Mathf.Abs(player.velocity.x) > player.maxMoveSpeed))
        {
            introDir = player.velocity.x > 0 ? 1 : -1;
            moveH = Mathf.Abs(player.velocity.x);
            moveH -= player.maxMoveSpeed / 3;
            if (moveH < 0.01f)
            {
                moveH = 0;
            }
            player.velocity.x = moveH * introDir;
        }
        else
        {
            if (player.input.v < 0)
            {
                return;
            }
            if (player.input.moveDir > 0)
            {
                player.velocity.x += player.maxMoveSpeed / 6f;
                if (player.velocity.x > player.maxMoveSpeed)
                {
                    player.velocity.x = player.maxMoveSpeed;
                }
            }
            else if (player.input.moveDir < 0)
            {
                player.velocity.x += -player.maxMoveSpeed / 6f;
                if (player.velocity.x < -player.maxMoveSpeed)
                {
                    player.velocity.x = -player.maxMoveSpeed;
                }
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (player.IsOnGround == false)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}