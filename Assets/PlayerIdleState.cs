public class PlayerIdleState : State
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelYZero();
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
        if(player.input.h != 0 && player.IsOnGround)
        {
            stateMachine.ChangeState(player.moveState);
        }
        if(player.input.moveDir == 0)
        {
            player.velocity.x = 0;
        }
    }
}