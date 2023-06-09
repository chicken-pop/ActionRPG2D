using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Unique effect")]
    public float itemCoolDown;
    public ItemEffect[] itemEffects;@//íÌøÊ

    [Header("Major stats")]
    public int strength; //NeBJÌ_[WÁ
    public int agility;@//ñð¦ÌÁ
    public int intelegence;@//@_[WÁÆ@_[WÌy¸
    public int vitality;@//HPÌÁ

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int heath;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;

    private int descriptionLength;

    public void Effect(Transform _enemyPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelegence.AddModifier(intelegence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(heath);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);

    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelegence.RemoveModifier(intelegence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(heath);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(strength, "éÍ");
        AddItemDescription(agility, "rq«");
        AddItemDescription(intelegence, "m«");
        AddItemDescription(vitality, "Í");

        AddItemDescription(damage, "UÍ");
        AddItemDescription(critChance, "ïS¦");
        AddItemDescription(critPower, "ïS");

        AddItemDescription(heath, "ÌÍ");
        AddItemDescription(armor, "çõÍ");
        AddItemDescription(evasion, "ñð");
        AddItemDescription(magicResistance, "@Ï«");

        AddItemDescription(fireDamage, "®«");
        AddItemDescription(iceDamage, "X®«");
        AddItemDescription(lightingDamage, "®«");


        if (descriptionLength > 0)
        {
            /*
            //õiÌøÊª­È¢êAsðâ®·é
            if (descriptionLength < 5)
            {
                for (int i = 0; i < 4 - descriptionLength; i++)
                {
                    sb.AppendLine();
                    sb.Append("");
                }
            }
            */
            sb.AppendLine();
            sb.Append("");
        }

        if (itemEffects != null)
        {
            //õiÌÁêøÊ
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].effectDescription.Length > 0)
                {
                    if (descriptionLength > 0)
                    {
                        sb.AppendLine();
                    }
                    sb.AppendLine(itemEffects[i].effectDescription);
                    descriptionLength++;
                }
            }
        }

        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (_value > 0)
            {
                //Xe[^X¼Æp[^[Ì¶ñðí¹Ä\¦
                sb.Append("+" + _value + " " + _name);
            }

            descriptionLength++;
        }
    }
}
