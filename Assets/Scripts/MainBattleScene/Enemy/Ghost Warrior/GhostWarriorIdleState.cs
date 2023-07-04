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

        //DeathBringerêÌäJén
        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
        {
            enemy.bossFightBegun = true;

        }

        if (stateTimer < 0 && enemy.bossFightBegun)
        {
            stateMachine.ChangeState(enemy.battleState);
        }

    }
}
