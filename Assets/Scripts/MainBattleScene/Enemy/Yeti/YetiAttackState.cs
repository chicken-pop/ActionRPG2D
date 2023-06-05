using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiAttackState : EnemyState
{
    private Enemy_Yeti enemy;

    private int attackType;

    public YetiAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Yeti _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        attackType = Random.Range(0, 2);

        enemy.anim.SetInteger("AttackType", attackType);

        if (attackType == 1)
        {
            enemy.GetComponent<CharacterStats>().damage.AddModifier(20);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (attackType == 1)
        {
            enemy.GetComponent<CharacterStats>().damage.RemoveModifier(20);
        }

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
