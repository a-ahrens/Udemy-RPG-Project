using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(xInput == player.facingDirection && player.IsWallDetected())
        {
            return;
        }

        if (xInput != 0) 
        {
            stateMachine.ChangeState(player.moveState);
        }
    }   
}
