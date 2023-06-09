using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelegence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicResistance,
    fireDamage,
    iceDamage,
    lightingDamage,
}

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major stats")]
    public Stat strength; //物理ダメージ増加（1ごとに1増加）、クリティカル時のダメージ増加（1ごとに1%上昇）
    public Stat agility;　//回避率の増加（1ごとに1%上昇）、クリティカル時の確率増加（1ごとに1%上昇）
    public Stat intelegence;　//魔法ダメージ増加、魔法ダメージの軽減(1ごとに3軽減)
    public Stat vitality;　//体力の増加

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;　//クリティカル時の倍率

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor; //物理ダメージの軽減（受けるダメージからarmorの値を引く）
    public Stat evasion;　//回避率（Maxが100）
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;

    public bool isIgnited; //魔法による継続的ダメージ
    public bool isChilled; //守備力の軽減
    public bool isShocked; //移動速度の軽減

    [SerializeField] private float ailmentsDuration = 4;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCooldown = 0.3f;
    private float igniteDamageTimer;
    private int igniteDamage;
    [SerializeField] private GameObject shockStrikePrefab;
    private int shockDamage;

    public int currentHealth;

    public System.Action onHealthChanged;　//UpdateHealthUI()によってHPのUIを更新する
    public bool isDead { get; private set; }

    protected bool isInvincible;
    private bool isVulnerable;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();

        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
        {
            isIgnited = false;
        }

        if (chilledTimer < 0)
        {
            isChilled = false;
        }

        if (shockedTimer < 0)
        {
            isShocked = false;
        }

        if (isIgnited)
        {
            ApplyIgniteDamage();
        }
    }

    public void MakeVulnerableFor(float _duration) => StartCoroutine(VulnerableForCoroutine(_duration));

    private IEnumerator VulnerableForCoroutine(float _duration)
    {
        isVulnerable = true;

        yield return new WaitForSeconds(_duration);

        isVulnerable = false;
    }

    public virtual void IncreaseStatBy(int _modifier, float _duration, Stat _statToModify) => StartCoroutine(StatModCoroutine(_modifier, _duration, _statToModify));

    private IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModify.RemoveModifier(_modifier);
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        bool criticalStrile = false;

        if (_targetStats.isInvincible)
        {
            return;
        }

        if (TargetCanAvoidAttack(_targetStats))
        {
            return;
        }

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        //ダメージばらつきのための倍率
        float randomMagnification = Random.Range(0.9f, 1.1f);

        int totalDamage = Mathf.RoundToInt((damage.GetValue() + strength.GetValue()) * randomMagnification); 

        if (CanCrit())
        {
            totalDamage = CalcurateCriticalDamage(totalDamage);
            criticalStrile = true;
        }

        fx.CreateHitFx(_targetStats.transform, criticalStrile);

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);

        DoMagicDamage(_targetStats); //魔法ダメージを与えたくなかったら、消す

    }

    #region Magical damage and ailments

    public virtual void DoMagicDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        //ダメージばらつきのための倍率
        float randomMagnification = Random.Range(0.8f, 1.2f);

        //ダメージ計算
        int totalMagicDamage = Mathf.RoundToInt((_fireDamage + _iceDamage + _lightingDamage + intelegence.GetValue()) * randomMagnification); 
        totalMagicDamage = CheckTargetResistance(_targetStats, totalMagicDamage);

        _targetStats.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
        {
            return;
        }

        AttemptToApplyAilements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);
    }

    private void AttemptToApplyAilements(CharacterStats _targetStats, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < 0.5f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock, _fireDamage, _iceDamage, _lightingDamage);
                return;
            }

            if (Random.value < 0.5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock, _fireDamage, _iceDamage, _lightingDamage);
                return;
            }

            if (Random.value < 0.5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock, _fireDamage, _iceDamage, _lightingDamage);
                return;
            }
        }

        if (canApplyIgnite)
        {
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.15f));
        }

        if (canApplyShock)
        {
            _targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(_lightingDamage * 0.1f));
        }

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock, _fireDamage, _iceDamage, _lightingDamage);
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock, int _fireDamage, int _iceDamage, int _lightingDamage)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;

        //属性の強さによって、効果の確率が変わる
        if (_ignite && canApplyIgnite && Random.Range(0, 100) < _fireDamage)
        {
            isIgnited = _ignite;
            ignitedTimer = ailmentsDuration;

            fx.IgniteFxFor(ailmentsDuration);
        }

        if (_chill && canApplyChill && Random.Range(0, 100) < _iceDamage)
        {
            isChilled = _chill;
            chilledTimer = ailmentsDuration;

            float slowPercentage = 0.2f;
            GetComponent<Entity>().SlowEntityBy(slowPercentage, ailmentsDuration);
            fx.ChillFxFor(ailmentsDuration);
        }

        if (_shock && canApplyShock && Random.Range(0, 100) < _lightingDamage)
        {
            if (!isShocked)
            {
                ApplyShock(_shock);

                if (GetComponent<Player>() != null)
                {
                    return;
                }

                HitNearestTargetResistance();

            }
            else
            {
                /*
                if (GetComponent<Player>() != null)
                {
                    return;
                }

                Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 25);

                float closestDistance = Mathf.Infinity;
                Transform closestEnemy = null;

                foreach (var hit in collisions)
                {
                    if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
                    {
                        float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                        if (distanceToEnemy < closestDistance)
                        {
                            closestDistance = distanceToEnemy;
                            closestEnemy = hit.transform;
                        }
                    }


                    if (closestEnemy == null)
                    {
                        closestEnemy = transform;
                    }

                }

                if (closestEnemy != null)
                {
                    GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);

                    newShockStrike.GetComponent<ShockStrike_Controller>().SetUp(shockDamage, closestEnemy.GetComponent<CharacterStats>());
                }

                */
            }
        }

    }

    public void ApplyShock(bool _shock)
    {
        if (isShocked)
        {
            return;
        }

        isShocked = _shock;
        shockedTimer = ailmentsDuration;

        fx.ShockFxFor(ailmentsDuration);
    }

    private void HitNearestTargetResistance()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in collisions)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);

            newShockStrike.GetComponent<ShockStrike_Controller>().SetUp(shockDamage, closestEnemy.GetComponent<CharacterStats>());
        }
    }

    private void ApplyIgniteDamage()
    {
        if (igniteDamageTimer < 0)
        {
            DecreaseHealthBy(igniteDamage, true);

            if (currentHealth <= 0 && !isDead)
            {
                Die();
            }

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;

    public void SetupShockStrikeDamage(int _damage) => shockDamage = _damage;

    #endregion

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
        {
            return;
        }

        DecreaseHealthBy(_damage, false);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        //回復後のHPが最大HPを超えてしまったとき
        if(currentHealth > GetMaxHealthValue())
        {
            currentHealth = GetMaxHealthValue();
        }

        if (onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    protected virtual void DecreaseHealthBy(int _damage, bool isIgnite)
    {
        //ソードスキルの効果で弱体化が入ったとき
        if (isVulnerable)
        {
            _damage = Mathf.RoundToInt(_damage * 1.2f);
        }

        currentHealth -= _damage;

        //ダメージ表記
        if (isIgnite == true)
        {
            fx.popUpTextPrefab.GetComponent<TextMeshPro>().color = Color.red;
            fx.CreatePopUpText(_damage.ToString());
        }
        else if(_damage > 0)
        {
            fx.popUpTextPrefab.GetComponent<TextMeshPro>().color = Color.white;
            fx.CreatePopUpText(_damage.ToString());

        }

        if (onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    #region Stat calculations

    public virtual void OnEvasion()
    {

    }

    /// <summary>
    /// 回避率の算出（100がMax回避率）
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <returns></returns>
    protected bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
        {
            totalEvasion += 20;
        }

        if (Random.Range(0, 100) < totalEvasion)
        {
            //回避成功時
            _targetStats.OnEvasion();
            return true;
        }

        return false;
    }

    /// <summary>
    /// 物理ダメージの軽減
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <param name="totalDamage"></param>
    /// <returns></returns>
    protected int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
        {
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * 0.8f);
        }
        else
        {
            totalDamage -= _targetStats.armor.GetValue();
        }

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    /// <summary>
    /// 魔法ダメージの軽減
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <param name="totalMagicDamage"></param>
    /// <returns></returns>
    private int CheckTargetResistance(CharacterStats _targetStats, int totalMagicDamage)
    {
        totalMagicDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelegence.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }

    /// <summary>
    /// クリティカルかどうか
    /// </summary>
    /// <returns></returns>
    protected bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// クリティカル時の倍率算出
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    protected int CalcurateCriticalDamage(int _damage)
    {
        float totalCriticalPower = (critPower.GetValue() + strength.GetValue()) * 0.01f;

        float critDamage = _damage * totalCriticalPower;

        return Mathf.RoundToInt(critDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    #endregion

    public void KillEntity()
    {
        if (!isDead)
        {
            Die();
        }
    }

    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;

    public Stat GetStatType(StatType _statType)
    {
        switch (_statType)
        {
            case StatType.strength:
                return strength;
            case StatType.agility:
                return agility;
            case StatType.intelegence:
                return intelegence;
            case StatType.vitality:
                return vitality;
            case StatType.damage:
                return damage;
            case StatType.critChance:
                return critChance;
            case StatType.critPower:
                return critPower;
            case StatType.health:
                return maxHealth;
            case StatType.armor:
                return armor;
            case StatType.evasion:
                return evasion;
            case StatType.magicResistance:
                return magicResistance;
            case StatType.fireDamage:
                return fireDamage;
            case StatType.iceDamage:
                return iceDamage;
            case StatType.lightingDamage:
                return lightingDamage;
        }

        return null;
    }
}
