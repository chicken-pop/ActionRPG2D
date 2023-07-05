using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomKnightStunneState : EnemyState
{
    Enemy_PhantomKnight enemy;

    public PhantomKnightStunneState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_PhantomKnight _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);

        stateTimer = enemy.stunDuration;

        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idelState);
        }


    }
}
