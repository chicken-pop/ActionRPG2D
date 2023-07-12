using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    AudioManager.SE moveSound;

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        //AudioManager.Instance.PlaySE(AudioManager.SE.sword, null);

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

    private void MoveSound()
    {
        var index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                moveSound = AudioManager.SE.move0;
                break;
            case 1:
                moveSound = AudioManager.SE.move1;
                break;
            case 2:
                moveSound = AudioManager.SE.move2;
                break;
            case 3:
                moveSound = AudioManager.SE.move3;
                break;
        }

        AudioManager.Instance.PlaySE(moveSound, null);
    }
}
