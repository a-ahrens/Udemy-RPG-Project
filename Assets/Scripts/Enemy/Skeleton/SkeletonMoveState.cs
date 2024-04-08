using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, EnemySkeleton enemy) : base(stateMachine, enemyBase, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, enemy.rb.velocity.y);

        if(enemy.IsWallDetected() || !enemy.IsGroundDetected() )
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
