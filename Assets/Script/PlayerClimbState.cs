﻿using Unity.VisualScripting;
using UnityEngine;

public class PlayerClimbState : State
{
    public PlayerClimbState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.curState = CurState.Climb;
        player.SetVelZero();
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
        if (Input.GetKeyDown(player.input.jump))
        {
            if(player.input.moveDir == 0 || player.CheckIsSlide())
            {
                player.velocity.y = 0;
                player.Jump();
            }
            else if(!player.CheckIsSlide())
            {
                player.velocity.y = 0;
                player.Jump(new Vector2(8 * player.faceDir, 0), new Vector2(32, 0));
            }
            return;
        }
        if(player.HorizontalBox != null)
        {
            if (player.input.v > 0 && player.transform.position.y - player.HorizontalBox[0].point.y > 0.4f)
            {
                player.AuotoJumpClimb();
                return;
            }
            if(player.input.v <= 0 && player.transform.position.y - player.HorizontalBox[0].point.y > 0.3f)
            {
                player.velocity.y = -player.climbSpeed;
            }
            else if(player.transform.position.y - player.HorizontalBox[0].point.y <= 0.3f)
            {
                player.velocity.y = player.input.v * player.climbSpeed;
            }
        }
        if (player.input.ClimbKeyUp)
        {
            if (player.IsOnGround)
            {
                stateMachine.ChangeState(player.normalState);
            }
            else if (!player.IsOnGround)
            {
                stateMachine.ChangeState(player.fallState);
            }
            //return;
        }
    }
}