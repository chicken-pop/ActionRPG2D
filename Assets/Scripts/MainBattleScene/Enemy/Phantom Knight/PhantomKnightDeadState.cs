using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomKnightDeadState : EnemyState
{
    private Enemy_PhantomKnight enemy;

    public PhantomKnightDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_PhantomKnight _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
