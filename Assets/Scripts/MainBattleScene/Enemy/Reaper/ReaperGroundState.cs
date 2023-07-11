using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperGroundState : EnemyState
{
    protected Enemy_Reaper enemy;
    protected Transform player;

    public ReaperGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Reaper _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
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

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.agroDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
