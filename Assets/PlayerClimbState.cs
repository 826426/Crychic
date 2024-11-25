using Unity.VisualScripting;
using UnityEngine;

public class PlayerClimbState : State
{
    public PlayerClimbState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isClimbing = true;
        player.velocity.x = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.isClimbing = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (player.input.JumpKeyDown)
        {
            if((player.input.moveDir > 0 && player.rightBox != null) || (player.input.moveDir < 0 && player.leftBox != null))
            {
                stateMachine.ChangeState(player.jumpState);
                return;
            }
            stateMachine.ChangeState(player.jumpState);
            return;
        }
        if (player.input.JumpKeyDown && ((player.input.moveDir < 0 && player.rightBox != null) || (player.input.moveDir > 0 && player.leftBox != null)))
        {
            player.velocity.y = 0;
            player.isJumping = false;
            player.Jump(new Vector2(4 * player.input.moveDir, 0), new Vector2(12, 0));
            stateMachine.ChangeState(player.airState);
            return;
        }
        if(player.HorizontalBox != null)
        {
            if(player.transform.position.y - player.HorizontalBox[0].point.y > 0.2f && player.input.v > 0)
            {
                player.AuotoJumpClimb();
                return;
            }
            if (player.transform.position.y - player.HorizontalBox[0].point.y > 0.3f)
            {
                player.velocity.y = -player.slideSpeed;
            }
            else if(player.transform.position.y - player.HorizontalBox[0].point.y <= 0.3f)
            {
                player.velocity.y = player.input.v * player.slideSpeed;
            }
        }
        if (player.IsOnGround && player.input.ClimbKeyUp)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.input.ClimbKeyUp)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}