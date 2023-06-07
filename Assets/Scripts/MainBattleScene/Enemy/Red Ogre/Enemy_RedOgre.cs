using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RedOgre : Enemy
{
    #region States
    public RedOgreIdleState idelState { get; private set; }
    public RedOgreMoveState moveState { get; private set; }
    public RedOgreBattleState battleState { get; private set; }
    public RedOgreAttackState attackState { get; private set; }
    public RedOgreStunnedState stunnedState { get; private set; }
    public RedOgreDeadState deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new RedOgreIdleState(this, stateMachine, "Idle", this);
        moveState = new RedOgreMoveState(this, stateMachine, "Move", this);
        battleState = new RedOgreBattleState(this, stateMachine, "Move", this);
        attackState = new RedOgreAttackState(this, stateMachine, "Attack", this);
        stunnedState = new RedOgreStunnedState(this, stateMachine, "Stunned", this);
        deadState = new RedOgreDeadState(this, stateMachine, "Dead", this);
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
