using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Strike_Counroller : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Thunderのダメージを与える
        if (collision.GetComponent<Enemy>() != null)
        {
            PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            EnemyStats enemyTarget = collision.GetComponent<EnemyStats>();
            playerStats.DoMagicDamage(enemyTarget);
            AudioManager.Instance.PlaySE(AudioManager.SE.thunder, null);
        }
    }
}
