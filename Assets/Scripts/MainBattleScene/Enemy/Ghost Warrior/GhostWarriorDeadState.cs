using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWarriorDeadState : EnemyState
{
    private Enemy_GhostWarrior enemy;

    public GhostWarriorDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_GhostWarrior _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
