using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Yeti : Enemy
{
    #region States
    public YetiIdleState idelState { get; private set; }
    public YetiMoveState moveState { get; private set; }
    public YetiBattleState battleState { get; private set; }
    public YetiAttackState attackState { get; private set; }
    public YetiStunnedState stunnedState { get; private set; }
    public YetiDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new YetiIdleState(this, stateMachine, "Idle", this);
        moveState = new YetiMoveState(this, stateMachine, "Move", this);
        battleState = new YetiBattleState(this, stateMachine, "Move", this);
        attackState = new YetiAttackState(this, stateMachine, "Attack", this);
        stunnedState = new YetiStunnedState(this, stateMachine, "Stunned", this);
        deadState = new YetiDeadState(this, stateMachine, "Dead", this);
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
        Destroy(counterImage,0.5f);

    }
}
