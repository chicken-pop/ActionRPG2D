using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        //player.DamageImpact();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        if(_damage > GetMaxHealthValue()* 0.3f)
        {
            player.SetupKnockbackPower(new Vector2(6, 8));
        }

        //何も装備していない場合nullがかえってくる
        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

        if (currentArmor != null)
        {
            //鎧に付属している効果を実行
            currentArmor.Effect(player.transform);
        }
    }

    public override void OnEvasion()
    {
        player.skill.dodge.CreateMirageOnDodge();
    }

    public void CloneDoDamage(CharacterStats _targetStats, float _multipliar)
    {
        if (TargetCanAvoidAttack(_targetStats))
        {
            return;
        }

        int totalDamage = damage.GetValue() + strength.GetValue();

        //クローンの攻撃の場合、ダメージが下がる
        if(_multipliar > 0)
        {
            totalDamage = Mathf.RoundToInt(totalDamage * _multipliar);
        }

        if (CanCrit())
        {
            totalDamage = CalcurateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);

        DoMagicDamage(_targetStats); //魔法ダメージを与えたくなかったら、消す
    }
}
