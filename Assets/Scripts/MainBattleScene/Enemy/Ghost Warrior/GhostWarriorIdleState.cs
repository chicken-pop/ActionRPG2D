using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWarriorIdleState : GhostWarriorGroundState
{
    public GhostWarriorIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_GhostWarrior _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idelTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.battleState);
        }

    }
}
