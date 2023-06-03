using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rat : Enemy
{
    #region States
    public RatIdelState idelState { get; private set; }
    public RatMoveState moveState { get; private set; }
    public RatBattleState battleState { get; private set; }
    public RatAttackState attackState { get; private set; }
    public RatStunnedState stunnedState { get; private set; }
    public RatDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new RatIdelState(this, stateMachine, "Idel", this);
        moveState = new RatMoveState(this, stateMachine, "Move", this);
        battleState = new RatBattleState(this, stateMachine, "Move", this);
        attackState = new RatAttackState(this, stateMachine, "Attack", this);
        stunnedState = new RatStunnedState(this, stateMachine, "Stunned", this);
        deadState = new RatDeadState(this, stateMachine, "Idel", this);
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

    }
}

