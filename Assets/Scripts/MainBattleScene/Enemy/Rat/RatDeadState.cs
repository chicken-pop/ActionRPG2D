using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDeadState : EnemyState
{
    private Enemy_Rat enemy;

    public RatDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Rat _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
