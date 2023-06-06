using System.Collections;
using UnityEngine;


public class ArcherDeadState : EnemyState
{
    private Enemy_Archer enemy;

    public ArcherDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Archer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();
    }
}
