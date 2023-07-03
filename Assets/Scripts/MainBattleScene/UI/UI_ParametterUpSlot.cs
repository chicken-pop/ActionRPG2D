using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ParametterUpSlot : MonoBehaviour
{
    [SerializeField] private int needSkillPoint;

    private UI_WizardEvent uI_ParametterUp => GetComponentInParent<UI_WizardEvent>();

    [SerializeField] private TextMeshProUGUI descriptionText;

    private enum Type
    {
        Strength,
        Agility,
        Intelegence,
        Vitality
    }

    [SerializeField] private Type type;

    private void Update()
    {
        switch (type)
        {
            case Type.Strength:
                descriptionText.text = "秘力が1上がる\n" + "(消費スキル)" + $"{needSkillPoint + needSkillPoint * uI_ParametterUp.StrengthLevel}";
                break;
            case Type.Agility:
                descriptionText.text = "俊敏性が1上がる\n" + "(消費スキル)" + $"{needSkillPoint + needSkillPoint * uI_ParametterUp.AgilityLevel}";
                break;
            case Type.Intelegence:
                descriptionText.text = "知性が1上がる\n" + "(消費スキル)" + $"{needSkillPoint + needSkillPoint * uI_ParametterUp.IntelegenceLevel}";
                break;
            case Type.Vitality:
                descriptionText.text = "活力が1上がる\n" + "(消費スキル)" + $"{needSkillPoint + needSkillPoint * uI_ParametterUp.VitalityLevel}";
                break;

        }
    }

    public void StrengthUp()
    {
        if (PlayerManager.instance.SkillPoint < needSkillPoint + needSkillPoint * uI_ParametterUp.StrengthLevel)
        {
            Debug.Log("スキルポイントが足りない");
            return;
        }

        //Debug.Log("秘力Up");
        AudioManager.Instance.PlaySE(AudioManager.SE.parameterUp, null);
        uI_ParametterUp.player.stats.strength.AddModifier(1);
        Inventory.instance.UpdateStatsUI();

        uI_ParametterUp.StrengthLevel++;
        PlayerManager.instance.SkillPoint -= needSkillPoint * uI_ParametterUp.StrengthLevel;
    }

    public void AgilityUp()
    {
        if (PlayerManager.instance.SkillPoint < needSkillPoint + needSkillPoint * uI_ParametterUp.AgilityLevel)
        {
            Debug.Log("スキルポイントが足りない");
            return;
        }

        //Debug.Log("俊敏性Up");
        AudioManager.Instance.PlaySE(AudioManager.SE.parameterUp, null);
        uI_ParametterUp.player.stats.agility.AddModifier(1);
        Inventory.instance.UpdateStatsUI();

        uI_ParametterUp.AgilityLevel++;
        PlayerManager.instance.SkillPoint -= needSkillPoint * uI_ParametterUp.AgilityLevel;


    }

    public void IntelegenceUp()
    {
        if (PlayerManager.instance.SkillPoint < needSkillPoint + needSkillPoint * uI_ParametterUp.IntelegenceLevel)
        {
            Debug.Log("スキルポイントが足りない");
            return;
        }

        //Debug.Log("知性Up");
        AudioManager.Instance.PlaySE(AudioManager.SE.parameterUp, null);
        uI_ParametterUp.player.stats.intelegence.AddModifier(1);
        Inventory.instance.UpdateStatsUI();

        uI_ParametterUp.IntelegenceLevel++;
        PlayerManager.instance.SkillPoint -= needSkillPoint * uI_ParametterUp.IntelegenceLevel;
    }

    public void VitalityUp()
    {
        if (PlayerManager.instance.SkillPoint < needSkillPoint + needSkillPoint * uI_ParametterUp.VitalityLevel)
        {
            Debug.Log("スキルポイントが足りない");
            return;
        }

        //Debug.Log("活力Up");
        AudioManager.Instance.PlaySE(AudioManager.SE.parameterUp, null);
        uI_ParametterUp.player.stats.vitality.AddModifier(1);
        Inventory.instance.UpdateStatsUI();

        PlayerManager.instance.SkillPoint -= needSkillPoint * uI_ParametterUp.VitalityLevel;
        uI_ParametterUp.VitalityLevel++;

    }
}
