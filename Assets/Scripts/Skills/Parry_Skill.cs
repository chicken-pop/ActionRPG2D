using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("パリィ")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked { get; private set; }

    [Header("パリィ+")]
    [SerializeField] private UI_SkillTreeSlot restoreUnlockButton;
    public bool restoreUnlocked { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthAmount;

    [Header("パリィ++")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked { get; private set; }
    [SerializeField] private float cloneToChance;

    protected override void Start()
    {
        base.Start();
        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);

    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();

    }

    public override void UseSkill()
    {
        base.UseSkill();

        //パリィ+
        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValue() * restoreHealthAmount);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }

    private void UnlockParry()
    {
        if (parryUnlockButton.unlocked)
        {
            parryUnlocked = true;
        }
    }

    private void UnlockParryRestore()
    {
        if (parryUnlockButton.unlocked)
        {
            restoreUnlocked = true;
        }
    }

    private void UnlockParryWithMirage()
    {
        if (parryUnlockButton.unlocked)
        {
            parryWithMirageUnlocked = true;
        }     
    }

    //パリィ++
    public void MakeMirageOnparry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked && Random.Range(0, 100) < cloneToChance)
        {
            SkillManager.instance.clone.CreateCloneWithDelay(_respawnTransform);
        }
    }
}
