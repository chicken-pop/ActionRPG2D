using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerIdleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform player;
    private Event eventData;

    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy, Event _evenyData) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
        eventData = _evenyData;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idelTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        //êÌäJén
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
