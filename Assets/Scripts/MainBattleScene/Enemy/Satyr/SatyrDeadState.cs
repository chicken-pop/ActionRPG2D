using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatyrDeadState : EnemyState
{
    private Enemy_Satyr enemy;

    public SatyrDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Satyr _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("Idle", false);
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

    }
}
