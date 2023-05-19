using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;

    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    [TextArea]
    [SerializeField] private string statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat -" + statName;

        if (statNameText != null)
        {
            statNameText.text = statName;
        }
    }

    private void Start()
    {
        UpdateStatValue();

        ui = GetComponentInParent<UI>();
    }

    public void UpdateStatValue()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        //agilityやvitalityなど、パラメーター増加を無視した値
        if (playerStats != null)
        {
            statValueText.text = playerStats.GetStatType(statType).GetValue().ToString();
        }

        //上記を考慮した値
        switch (statType)
        {
            case StatType.health:
                statValueText.text = playerStats.GetMaxHealthValue().ToString();
                break;
            case StatType.damage:
                statValueText.text = (playerStats.damage.GetValue() + playerStats.strength.GetValue()).ToString();
                break;
            case StatType.critPower:
                statValueText.text = (playerStats.critPower.GetValue() + playerStats.strength.GetValue()).ToString();
                break;
            case StatType.critChance:
                statValueText.text = (playerStats.critPower.GetValue() + playerStats.agility.GetValue()).ToString();
                break;
            case StatType.evasion:
                statValueText.text = (playerStats.evasion.GetValue() + playerStats.agility.GetValue()).ToString();
                break;
            case StatType.magicResistance:
                statValueText.text = (playerStats.magicResistance.GetValue() + playerStats.intelegence.GetValue() * 3).ToString();
                break;

        }
     
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowStatToolTip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideStatToolTip();
    }
}
