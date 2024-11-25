using UnityEditor;
using UnityEngine;

public class State
{
    protected Player player;
    protected StateMachine stateMachine;
    private string animBoolName;
    public State(Player player,StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {

    }
    public virtual void Exit()
    {

    }
    public virtual void Update()
    {
        if (!player.isCanController)
        {
            return;
        }
        if (player.input.DashKeyDown && player.dashCount > 0)
        {
            stateMachine.ChangeState(player.dashState);
        }
        if (!player.IsOnGround && !player.isJumping && !player.isClimbing && !player.isDashing)
        {
            stateMachine.ChangeState(player.airState);
        }
        if(player.input.JumpKeyDown && player.IsOnGround)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if(!player.IsOnGround)
        {
            if((player.input.moveDir > 0 || player.input.moveDir < 0) && player.IsCanClimb && !player.isClimbing && !player.isJumping)
            {
                if((player.faceDir > 0 && player.leftBox.Length > 0) || (player.faceDir < 0 && player.rightBox.Length > 0))
                {
                    stateMachine.ChangeState(player.airState);
                    return;
                }
                stateMachine.ChangeState(player.slideState);
            }
        }
        if (player.input.ClimbKey && player.IsCanClimb && !player.isClimbing && !player.isJumping)
        {
            stateMachine.ChangeState(player.climbState);
        }
    }
    public virtual void FixedUpdate()
    {

    }
 
}