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

        //プレイヤーが持ってる武器,素材をリストに追加
        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        List<InventoryItem> currentStash = inventory.GetStashList();

        //失う武器、素材を格納するリスト
        List<InventoryItem> itemsToUnmequip = new List<InventoryItem>();
        List<InventoryItem> materialsToLoose = new List<InventoryItem>();

        //確率で装備を落として、装備を解除
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

        //素材に関して同様に行っている
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
