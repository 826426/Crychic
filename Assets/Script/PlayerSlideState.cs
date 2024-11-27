using UnityEngine;

public class PlayerSlideState : State
{
    public PlayerSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.curState = CurState.Slide;
        player.velocity.x = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (player.input.JumpKeyDown && player.CheckIsSlide())
        {
            player.SetVelZero();
            player.Jump(new Vector2(6 * -player.GetWallDir(), 0), new Vector2(24, 0));
            return;
        }
        if(!player.CheckIsSlide())
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }
        if (player.IsOnGround)
        {
            stateMachine.ChangeState(player.normalState);
            return;
        }
        player.velocity.y = -player.climbSpeed;
    }
}