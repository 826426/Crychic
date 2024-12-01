using UnityEngine;

public class PlayerFallState : State
{
    private float introDir;
    private float moveH;
    public PlayerFallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.fallAudio.Play();
        player.curState = CurState.Fall;
    }

    public override void Exit()
    {
        base.Exit();
        ParticleManager.instance.DustPlay(player.transform);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.velocity.y -= 150f * Time.deltaTime;
        player.velocity.y = Mathf.Clamp(player.velocity.y, -25, player.velocity.y);
        if (player.CheckCanMove())
        {
            if (player.velocity.x > 0 && player.input.moveDir < 0 || (player.velocity.x < 0 && player.input.moveDir > 0) || (player.input.moveDir == 0
        || Mathf.Abs(player.velocity.x) > player.maxMoveSpeed))
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
    }

    public override void Update()
    {
        base.Update();
        if (player.IsOnGround == true)
        {
            stateMachine.ChangeState(player.normalState);
        }
        if (Input.GetKeyDown(player.input.jump) && player.IsCanClimb)
        {
            player.SetVelZero();
            player.Jump(new Vector2(5 * -player.GetWallDir(), 0), new Vector2(20, 0));
        }
    }
}