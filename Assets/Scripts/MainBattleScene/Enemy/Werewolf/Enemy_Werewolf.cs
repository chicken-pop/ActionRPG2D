using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Werewolf : Enemy
{
    #region States
    public WerewolfIdelState idelState { get; private set; }
    public WerewolfMoveState moveState { get; private set; }
    public WerewolfBattleState battleState { get; private set; }
    public WerewolfAttackState attackState { get; private set; }
    public WerewolfStunnedState stunnedState { get; private set; }
    public WerewolfDeadState deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        SetupDefalutFacingDir(-1);

        idelState = new WerewolfIdelState(this, stateMachine, "Idle", this);
        moveState = new WerewolfMoveState(this, stateMachine, "Move", this);
        battleState = new WerewolfBattleState(this, stateMachine, "Move", this);
        attackState = new WerewolfAttackState(this, stateMachine, "Attack", this);
        stunnedState = new WerewolfStunnedState(this, stateMachine, "Stunned", this);
        deadState = new WerewolfDeadState(this, stateMachine, "Dead", this);
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
