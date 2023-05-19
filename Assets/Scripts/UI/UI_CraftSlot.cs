using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Start()
    {
        base.Start();
    }

    public void SetUpCraftSlot(ItemData_Equipment _data)
    {

        if(_data == null)
        {
            return;
        }

        item.data = _data;

        itemImage.sprite = _data.icon;
        itemText.text = _data.itemName;

        /*
        //‘¼‚Ì‘•”õ•i‚ðl—¶‚µ‚ÄŒˆ’è
        if(itemText.text.Length > 5)
        {
            itemText.fontSize = itemText.fontSize * 0.8f;
        }
        else
        {
            itemText.fontSize = 24;
        }
        */
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_Equipment);
    }
}
