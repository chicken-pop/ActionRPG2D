using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;

    private void SetupVisuals()
    {
        if (itemData == null)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object -" + itemData.itemName;
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisuals();
    }

    public void PickupItem()
    {
        //装備スロットが全て埋まっている状態で、装備を拾った場合return
        if(!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            //アイテムを少し浮かせる
            rb.velocity = new Vector2(0, 7);
            PlayerManager.instance.player.fx.CreatePopUpText("持ち物がいっぱいです");
            return;
        }

        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
