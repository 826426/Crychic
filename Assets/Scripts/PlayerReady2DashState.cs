using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerReady2DashState : State
{
    public Vector2 playerStayPos;
    public PlayerReady2DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.curState = CurState.Ready2Dash;
        player.dashCount += 1;
        player.transform.position = playerStayPos;
        player.anim.SetFloat("Hor",0);
        player.SetVelZero();
    }

    public override void Update()
    {
        if (player.input.DashKeyDown)
        {
            stateMachine.ChangeState(player.dashState);
        }


        //player用的是rotate转向，导致如果我开始和结束朝向不一样就会反
        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     player.transform.rotation = Quaternion.Euler(0, 0,0);
        // }
        //
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     player.transform.rotation = Quaternion.Euler(0, 180, 0);
        // }
    }
}
