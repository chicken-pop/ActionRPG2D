using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();
    }
}
