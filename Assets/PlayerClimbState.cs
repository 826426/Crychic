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
        player.velocity.y = player.input.v * player.slideSpeed;
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