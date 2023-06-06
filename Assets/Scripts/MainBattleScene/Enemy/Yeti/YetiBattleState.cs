using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiBattleState : EnemyState
{
    private Transform player;
    private Enemy_Yeti enemy;

    private int moveDir;

    public YetiBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Yeti _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.moveState);
        }

    }

    public override void Update()
    {
        base.Update();

        enemy.anim.SetBool("Move", true);

        //Player‚ğŒŸ’m‚µ‚½ÛA—‚¿‚È‚¢‚æ‚¤‚ÉBattleState‚ğ‚Ê‚¯‚é
        if (enemy.IsPlayerDetected() && !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idelState);
            return;
        }

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                //Player‚Æ‚Ì‹——£‚ª‹ß‚¢‚Æ‚«AIdle‚É
                enemy.anim.SetBool("Move", false);
                enemy.anim.SetBool("Idle", true);

                if (CanAttack())
                {
                    enemy.anim.SetBool("Idle", false);
                    stateMachine.ChangeState(enemy.attackState);
                }

            }
            else
            {
                enemy.anim.SetBool("Idle", false);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 10)
            {
                stateMachine.ChangeState(enemy.idelState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance - 0.1f)
        {
            return;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }


}
