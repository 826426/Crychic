using System.Diagnostics;
using UnityEngine;

public class PlayerSlideState : State
{
    public PlayerSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isSlide = true;
        player.velocity.x = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.isSlide = false;
        player.velocity.y = 0;
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
            player.velocity.y = 0;
            player.isJumping = false;
            player.faceDir = -player.faceDir;
            player.Jump(new Vector2(4 * player.faceDir, 0), new Vector2(12, 0));
            stateMachine.ChangeState(player.airState);
            return;
        }
        player.velocity.y = -player.slideSpeed;
        if (player.IsOnGround)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}