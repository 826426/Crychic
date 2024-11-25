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
        player.velocity.x = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.velocity.y = 0;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        player.velocity.y = -player.slideSpeed;
        if (player.IsOnGround)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}