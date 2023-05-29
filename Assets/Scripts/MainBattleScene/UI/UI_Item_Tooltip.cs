using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Item_Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private int defaultFontSize = 32;

    public void ShowToolTip(ItemData_Equipment item)
    {
        if (!item)
        {
            return;
        }

        string equipmentTypeName = "";

        //装備タイプが英語表記のため、日本語化
        switch (item.equipmentType)
        {
            case EquipmentType.Weapon:
                equipmentTypeName = "武器";
                break;
            case EquipmentType.Armor:
                equipmentTypeName = "防具";
                break;
            case EquipmentType.Amulet:
                equipmentTypeName = "お守り";
                break;
            case EquipmentType.Flask:
                equipmentTypeName = "アイテム";
                break;
        }

        itemNameText.text = item.itemName;
        itemTypeText.text = equipmentTypeName;
        itemDescription.text = item.GetDescription();

        //フォントサイズの調整
        if (itemNameText.text.Length > 8)
        {
            itemNameText.fontSize = itemNameText.fontSize * 0.8f;
        }
        else
        {
            itemNameText.fontSize = defaultFontSize;
        }

        gameObject.SetActive(true);
    }

    public void HideTooplTips()
    {
        gameObject.SetActive(false);
        itemNameText.fontSize = defaultFontSize;
    }
}
