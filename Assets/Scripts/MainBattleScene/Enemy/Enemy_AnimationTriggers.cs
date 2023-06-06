using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    [SerializeField] private Vector2 knockbackPower;

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void NormalAttackTrigger()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in collisions)
        {
            Player player = hit.GetComponent<Player>();

            if (player != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
                //enemy.stats.DoMagicDamage(target);
            }
        }

    }

    private void KnockbackAttackTrigger()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in collisions)
        {
            Player player = hit.GetComponent<Player>();

            if (player != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);

                player.SetupKnockbackDir(transform);
                player.rb.velocity = new Vector2(knockbackPower.x * player.knockbackDir, knockbackPower.y);
            }
        }
    }



    private void SpecialAttackTrigger()
    {
        enemy.AnimationSpecialAttackTrigger();
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
