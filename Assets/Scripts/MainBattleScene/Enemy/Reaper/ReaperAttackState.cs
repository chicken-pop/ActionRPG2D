using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAttackState : EnemyState
{
    Enemy_Reaper enemy;

    public ReaperAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Reaper _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
