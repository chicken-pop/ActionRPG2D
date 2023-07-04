using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWarriorSpellCastState : EnemyState
{
    Enemy_GhostWarrior enemy;

    private int amountOfSpells;
    private float spellTimer;

    private float defultGravity;

    public GhostWarriorSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_GhostWarrior _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        rb = enemy.GetComponent<Rigidbody2D>();
        defultGravity = rb.gravityScale;
        rb.gravityScale = 0;

        amountOfSpells = enemy.amountOfSpells;
        spellTimer = 2f;
        stateTimer = 0.4f;

        //Vector3 movePosition = new Vector3(player.transform.position.x, player.transform.position.y + 3);
        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, movePosition, 2f * Time.deltaTime);
        //rb.isKinematic = true;
    }

    public override void Update()
    {
        base.Update();

        spellTimer -= Time.deltaTime;

        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 25);
        }
        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -0.1f);
        }

        if (CanCast())
        {
            enemy.CastSpell();
        }

        if (amountOfSpells <= 0 && spellTimer <= -1)
        {
            stateMachine.ChangeState(enemy.idelState);
        }


    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeCast = Time.time;
        rb.gravityScale = defultGravity;

    }

    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCooldown;
            return true;
        }

        return false;
    }
}
