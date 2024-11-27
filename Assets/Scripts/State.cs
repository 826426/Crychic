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
        player.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        player.anim.SetFloat("Hor", Mathf.Abs(player.velocity.x));
        player.anim.SetFloat("Vel", player.velocity.y);
        player.anim.SetFloat("ClVel", Mathf.Abs(player.velocity.y));
        if (player.input.DashKeyDown && player.dashCount > 0)
        {
            stateMachine.ChangeState(player.dashState);
            return;
        }
        if (player.input.JumpKeyDown && player.IsOnGround && player.curState != CurState.Jump && player.curState != CurState.Dash)
        {
            player.Jump();
        }
        if(player.curState != CurState.Jump && !player.IsOnGround && player.CheckIsSlide() && player.curState != CurState.Climb && player.curState != CurState.Slide && player.curState != CurState.Dash)
        {
            stateMachine.ChangeState(player.slideState);
        }
        if(player.input.ClimbKey && player.IsCanClimb && player.curState != CurState.Jump && player.curState != CurState.Dash && player.curState != CurState.Climb)
        {
            stateMachine.ChangeState(player.climbState);
        }
    }
    public virtual void FixedUpdate()
    {

    }
 
}