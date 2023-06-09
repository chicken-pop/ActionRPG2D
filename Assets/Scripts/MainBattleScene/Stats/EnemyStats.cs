using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSystem;
    public Stat skillpointDropAmount;

    [Header("Level details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = 0.2f;

    protected override void Start()
    {
        //skillpointDropAmount.SetDefaultValue(100);
        ApplyLevelModifiers();

        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifiers()
    {
        //Modify(strength);
        //Modify(agility);
        Modify(intelegence);
        //Modify(vitality);

        Modify(damage);
        //Modify(critChance);
        //Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);

        Modify(skillpointDropAmount);
    }

    private void Modify(Stat _stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stat.GetValue() * percentageModifier;

            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        AudioManager.Instance.PlaySE(AudioManager.SE.enemyDamage, null);
    }

    protected override void Die()
    {
        base.Die();

        enemy.Die();
        myDropSystem.GenerateDrop();
        PlayerManager.instance.SkillPoint += skillpointDropAmount.GetValue();

        isInvincible = true;

        Destroy(GetComponentInChildren<UI_HealthBar>().gameObject, 0.5f);
        Destroy(gameObject, 8f);
    }
}
