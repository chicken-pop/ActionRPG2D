using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLooseItems;
    [SerializeField] private float chanceToLooseMaterials;

    public override void GenerateDrop()
    {
    
        Inventory inventory = Inventory.instance;

        //�v���C���[�������Ă镐��,�f�ނ����X�g�ɒǉ�
        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        List<InventoryItem> currentStash = inventory.GetStashList();

        //��������A�f�ނ��i�[���郊�X�g
        List<InventoryItem> itemsToUnmequip = new List<InventoryItem>();
        List<InventoryItem> materialsToLoose = new List<InventoryItem>();

        //�m���ő����𗎂Ƃ��āA����������
        foreach (InventoryItem item in currentEquipment)
        {
            if (Random.Range(0, 100) <= chanceToLooseItems)
            {
                DropItem(item.data);
                itemsToUnmequip.Add(item);
            }
        }

        for (int i = 0; i < itemsToUnmequip.Count; i++)
        {
            inventory.UnequipItem(itemsToUnmequip[i].data as ItemData_Equipment);
        }

        //�f�ނɊւ��ē��l�ɍs���Ă���
        foreach (InventoryItem item in currentStash)
        {
            if (Random.Range(0, 100) <= chanceToLooseMaterials)
            {
                DropItem(item.data);
                materialsToLoose.Add(item);
            }
        }


        for (int i = 0; i < materialsToLoose.Count; i++)
        {
            inventory.RemoveItem(materialsToLoose[i].data);
        }
    }
}
