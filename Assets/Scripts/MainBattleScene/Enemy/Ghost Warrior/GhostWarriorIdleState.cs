using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWarriorIdleState : GhostWarriorGroundState
{
    private Event eventData;

    public GhostWarriorIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_GhostWarrior _enemy, Event _eventData) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
        eventData = _eventData;
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

        //DeathBringerêÌäJén
        if (eventData.BossEvent == false)
        {
            enemy.bossFightBegun = true;

        }

        if (stateTimer < 0 && enemy.bossFightBegun)
        {
            stateMachine.ChangeState(enemy.battleState);
        }

    }
}
