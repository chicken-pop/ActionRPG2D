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
        //�����X���b�g���S�Ė��܂��Ă����ԂŁA�������E�����ꍇreturn
        if(!Inventory.instance.CanAddItem() && itemData.itemType == ItemType.Equipment)
        {
            //�A�C�e����������������
            rb.velocity = new Vector2(0, 7);
            PlayerManager.instance.player.fx.CreatePopUpText("�������������ς��ł�");
            return;
        }

        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
