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

        //‰½‚à‘•”õ‚µ‚Ä‚¢‚È‚¢ê‡null‚ª‚©‚¦‚Á‚Ä‚­‚é
        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

        if (currentArmor != null)
        {
            //ŠZ‚É•t‘®‚µ‚Ä‚¢‚éŒø‰Ê‚ğÀs
            currentArmor.Effect(player.transform);
        }
    }
}
