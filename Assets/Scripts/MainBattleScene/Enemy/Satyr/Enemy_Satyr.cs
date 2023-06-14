using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Satyr : Enemy
{
    #region States
    public SatyrIdleState idelState { get; private set; }
    public SatyrMoveState moveState { get; private set; }
    public SatyrBattleState battleState { get; private set; }
    public SatyrAttackState attackState { get; private set; }
    public SatyrStunnedState stunnedState { get; private set; }
    public SatyrDeadState deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new SatyrIdleState(this, stateMachine, "Idle", this);
        moveState = new SatyrMoveState(this, stateMachine, "Move", this);
        battleState = new SatyrBattleState(this, stateMachine, "Move", this);
        attackState = new SatyrAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SatyrStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SatyrDeadState(this, stateMachine, "Dead", this);
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
