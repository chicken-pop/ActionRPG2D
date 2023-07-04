using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GhostWarrior : Enemy
{
    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private BoxCollider2D spellArea;
    [SerializeField] private float yOffset;

    #region States
    public GhostWarriorIdleState idelState { get; private set; }
    public GhostWarriorBattleState battleState { get; private set; }
    public GhostWarriorAttackState attackState { get; private set; }
    public GhostWarriorSpellCastState spellCastState { get; private set; }
    public GhostWarriorDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idelState = new GhostWarriorIdleState(this, stateMachine, "Idle", this);
        battleState = new GhostWarriorBattleState(this, stateMachine, "Move", this);
        attackState = new GhostWarriorAttackState(this, stateMachine, "Attack", this);
        spellCastState = new GhostWarriorSpellCastState(this, stateMachine, "SpellCast", this);
        deadState = new GhostWarriorDeadState(this, stateMachine, "Dead", this);
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

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float x = Random.Range(spellArea.bounds.min.x, spellArea.bounds.max.x);

        Vector3 spellPosition = new Vector3(x, player.transform.position.y + yOffset);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<GhostWarriorSpell_Controller>().SetupSpell(stats);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
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
