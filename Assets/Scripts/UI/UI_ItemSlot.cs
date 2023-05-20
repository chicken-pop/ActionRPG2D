using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour , IPointerDownHandler ,IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI ui;
    public InventoryItem item;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            //アイコンの更新
            itemImage.sprite = item.data.icon;

            if (item.stackSize > 1)
            {
                //所持数
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null)
        {
            return;
        }

        //装備品の削除
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }
        
        //装備
        if(item.data.itemType == ItemType.Equipment)
        {
            Inventory.instance.EquipItem(item.data);
        }

        ui.itemTooltip.HideTooplTips();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item == null)
        {
            return;
        }

        float xOffset = -250;
        float yOffset = 300;

        Vector2 pos = gameObject.transform.position;

        //ポジションによって表示場所を変える
        ui.itemTooltip.transform.position = new Vector2(pos.x + xOffset, pos.y + yOffset);

        ui.itemTooltip.ShowToolTip(item.data as ItemData_Equipment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(item == null)
        {
            return;
        }

        ui.itemTooltip.HideTooplTips();
    }
}
