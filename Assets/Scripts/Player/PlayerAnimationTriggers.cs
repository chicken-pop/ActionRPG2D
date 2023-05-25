using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        AudioManager.instance.PlaySE(2, null);

        Collider2D[] collisions = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in collisions)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                }

                //‰½‚à‘•”õ‚µ‚Ä‚¢‚È‚¢ê‡null‚ª‚©‚¦‚Á‚Ä‚­‚é
                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);


                if (weaponData != null)
                {
                    //•Ší‚É•t‘®‚µ‚Ä‚¢‚éŒø‰Ê‚ğÀs
                    weaponData.Effect(_target.transform);
                }

            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
