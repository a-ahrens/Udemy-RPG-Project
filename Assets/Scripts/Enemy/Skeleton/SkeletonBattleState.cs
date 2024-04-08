using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{

    private Transform player;
    private EnemySkeleton enemy;
    private int moveDirection;

    public SkeletonBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, EnemySkeleton enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if(enemy.IsPlayerDetected().distance < enemy.attackDistance && CanAttack()) 
            {
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        if(player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDirection = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * 2f * moveDirection, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
