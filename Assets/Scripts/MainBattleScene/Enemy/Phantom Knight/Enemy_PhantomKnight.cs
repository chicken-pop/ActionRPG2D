using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PhantomKnight : Enemy
{
    #region States
    public PhantomKnightIdleState idelState { get; private set; }
    public PhantomKnightMoveState moveState { get; private set; }
    public PhantomKnightBattleState battleState { get; private set; }
    public PhantomKnightAttackState attackState { get; private set; }
    public PhantomKnightStunneState stunnedState { get; private set; }
    public PhantomKnightDeadState deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new PhantomKnightIdleState(this, stateMachine, "Idle", this);
        moveState = new PhantomKnightMoveState(this, stateMachine, "Move", this);
        battleState = new PhantomKnightBattleState(this, stateMachine, "Move", this);
        attackState = new PhantomKnightAttackState(this, stateMachine, "Attack", this);
        stunnedState = new PhantomKnightStunneState(this, stateMachine, "Stunned", this);
        deadState = new PhantomKnightDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idelState);
    }

    protected override void Update()
    {
        base.Update();


    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
        Destroy(counterImage, 0.5f);

    }
}
