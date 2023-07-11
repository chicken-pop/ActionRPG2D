using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Reaper : Enemy
{
    #region States
    public ReaperIdleState idelState { get; private set; }
    public ReaperMoveState moveState { get; private set; }
    public ReaperBattleState battleState { get; private set; }
    public ReaperAttackState attackState { get; private set; }
    public ReaperStunnedState stunnedState { get; private set; }
    public ReaperDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new ReaperIdleState(this, stateMachine, "Idle", this);
        moveState = new ReaperMoveState(this, stateMachine, "Move", this);
        battleState = new ReaperBattleState(this, stateMachine, "Move", this);
        attackState = new ReaperAttackState(this, stateMachine, "Attack", this);
        stunnedState = new ReaperStunnedState(this, stateMachine, "Stunned", this);
        deadState = new ReaperDeadState(this, stateMachine, "Dead", this);
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
