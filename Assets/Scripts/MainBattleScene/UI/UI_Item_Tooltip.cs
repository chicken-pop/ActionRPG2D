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

        //�����^�C�v���p��\�L�̂��߁A���{�ꉻ
        switch (item.equipmentType)
        {
            case EquipmentType.Weapon:
                equipmentTypeName = "����";
                break;
            case EquipmentType.Armor:
                equipmentTypeName = "�h��";
                break;
            case EquipmentType.Amulet:
                equipmentTypeName = "�����";
                break;
            case EquipmentType.Flask:
                equipmentTypeName = "�A�C�e��";
                break;
        }

        itemNameText.text = item.itemName;
        itemTypeText.text = equipmentTypeName;
        itemDescription.text = item.GetDescription();

        //�t�H���g�T�C�Y�̒���
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
